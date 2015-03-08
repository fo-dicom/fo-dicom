using System;
using System.Collections.Generic;
using System.Drawing.Printing;
//using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using Dicom;
using Dicom.Imaging;
using Dicom.Printing;


namespace Dicom.Printing
{
    public class StatusUpdateEventArgs : EventArgs
    {
        public ushort EventTypeId { get; private set; }
        public string ExecutionStatusInfo { get; private set; }
        public string FilmSessionLabel { get; private set; }
        public string PrinterName { get; private set; }

        public StatusUpdateEventArgs(ushort eventTypeId, string executionStatusInfo, string filmSessionLabel, string printerName)
        {
            EventTypeId = eventTypeId;
            ExecutionStatusInfo = executionStatusInfo;
            FilmSessionLabel = filmSessionLabel;
            PrinterName = printerName;
        }
    }
    public enum PrintJobStatus : ushort
    {
        Pending = 1,
        Printing = 2,
        Done = 3,
        Failure = 4
    }

    public class PrintJob : DicomDataset
    {
        #region Properties and Attributes

        public bool SendNEventReport { get; set; }

        private readonly object _synchRoot = new object();

        public Guid PrintJobGuid { get; private set; }

        public IList<string> FilmBoxFolderList { get; private set; }

        public Printer Printer { get; private set; }

        public PrintJobStatus Status { get; private set; }

        public string PrintJobFolder { get; private set; }

        public string FullPrintJobFolder { get; private set; }

        public Exception Error { get; private set; }

        public string FilmSessionLabel { get; private set; }

        private int _currentPage;
        private FilmBox _currentFilmBox;
        /// <summary>
        /// Print job SOP class UID
        /// </summary>
        public readonly DicomUID SOPClassUID = DicomUID.PrintJobSOPClass;

        /// <summary>
        /// Print job SOP instance UID
        /// </summary>
        public DicomUID SOPInstanceUID { get; private set; }

        /// <summary>
        /// Execution status of print job.
        /// </summary>
        /// <remarks>
        /// Enumerated Values:
        /// <list type="bullet">
        /// <item><description>PENDING</description></item>
        /// <item><description>PRINTING</description></item>
        /// <item><description>DONE</description></item>
        /// <item><description>FAILURE</description></item>
        /// </list>
        /// </remarks> 
        public string ExecutionStatus
        {
            get { return Get(DicomTag.ExecutionStatus, string.Empty); }
            set { Add(DicomTag.ExecutionStatus, value); }
        }

        /// <summary>
        /// Additional information about Execution Status (2100,0020).
        /// </summary>
        public string ExecutionStatusInfo
        {
            get { return Get(DicomTag.ExecutionStatusInfo, string.Empty); }
            set { Add(DicomTag.ExecutionStatusInfo, value); }
        }

        /// <summary>
        /// Specifies the priority of the print job.
        /// </summary>
        /// <remarks>
        /// Enumerated values:
        /// <list type="bullet">
        ///     <item><description>HIGH</description></item>
        ///     <item><description>MED</description></item>
        ///     <item><description>LOW</description></item>
        /// </list>
        /// </remarks>
        public string PrintPriority
        {
            get { return Get(DicomTag.PrintPriority, "MED"); }
            set { Add(DicomTag.PrintPriority, value); }
        }

        /// <summary>
        /// Date/Time of print job creation.
        /// </summary>
        public DateTime CreationDateTime
        {
            get { return this.GetDateTime(DicomTag.CreationDate, DicomTag.CreationTime); }
            set
            {
                Add(DicomTag.CreationDate, value);
                Add(DicomTag.CreationTime, value);
            }
        }

        /// <summary>
        /// User defined name identifying the printer.
        /// </summary>
        public string PrinterName
        {
            get { return Get(DicomTag.PrinterName, string.Empty); }
            set { Add(DicomTag.PrinterName, value); }
        }

        /// <summary>
        /// DICOM Application Entity Title that issued the print operation.
        /// </summary>
        public string Originator
        {
            get { return Get(DicomTag.Originator, string.Empty); }
            set { Add(DicomTag.Originator, value); }
        }

        public Dicom.Log.Logger Log { get; private set; }


        public event EventHandler<StatusUpdateEventArgs> StatusUpdate;
        #endregion

        #region Constructors

        /// <summary>
        /// Construct new print job using specified SOP instance UID. If passed SOP instance UID is missing, new UID will
        /// be generated
        /// </summary>
        /// <param name="sopInstance">New print job SOP instance uID</param>
        public PrintJob(DicomUID sopInstance, Printer printer, string originator, Dicom.Log.Logger log)
            : base()
        {
            if (printer == null)
            {
                throw new ArgumentNullException("printer");
            }

            Log = log;

            if (sopInstance == null || sopInstance.UID == string.Empty)
            {
                SOPInstanceUID = DicomUID.Generate();
            }
            else
            {
                SOPInstanceUID = sopInstance;
            }

            this.Add(DicomTag.SOPClassUID, SOPClassUID);
            this.Add(DicomTag.SOPInstanceUID, SOPInstanceUID);

            Printer = printer;

            Status = PrintJobStatus.Pending;

            PrinterName = Printer.PrinterAet;

            Originator = originator;

            if (CreationDateTime == DateTime.MinValue)
            {
                CreationDateTime = DateTime.Now;
            }

            PrintJobFolder = SOPInstanceUID.UID;

            var receivingFolder = Environment.CurrentDirectory + @"\PrintJobs";

            FullPrintJobFolder = string.Format(@"{0}\{1}", receivingFolder.TrimEnd('\\'), PrintJobFolder);

            FilmBoxFolderList = new List<string>();
        }

        #endregion

        #region Printing Methods

        public void Print(IList<FilmBox> filmBoxList)
        {
            try
            {
                Status = PrintJobStatus.Pending;

                OnStatusUpdate("Preparing films for printing");

                var printJobDir = new System.IO.DirectoryInfo(FullPrintJobFolder);
                if (!printJobDir.Exists)
                {
                    printJobDir.Create();
                }

                DicomFile file;
                int filmsCount = FilmBoxFolderList.Count;
                for (int i = 0; i < filmBoxList.Count; i++)
                {
                    var filmBox = filmBoxList[i];
                    var filmBoxDir = printJobDir.CreateSubdirectory(string.Format("F{0:000000}", i + 1 + filmsCount));

                    file = new DicomFile(filmBox.FilmSession);
                    file.Save(string.Format(@"{0}\FilmSession.dcm", filmBoxDir.FullName));

                    FilmBoxFolderList.Add(filmBoxDir.Name);
                    filmBox.Save(filmBoxDir.FullName);
                }

                FilmSessionLabel = filmBoxList.First().FilmSession.FilmSessionLabel;

                var thread = new Thread(new ThreadStart(DoPrint));
                thread.Name = string.Format("PrintJob {0}", SOPInstanceUID.UID);
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                Error = ex;
                Status = PrintJobStatus.Failure;
                OnStatusUpdate("Print failed");
                DeletePrintFolder();
            }
        }

        private void DoPrint()
        {
            PrintDocument printDocument = null;
            try
            {
                Status = PrintJobStatus.Printing;
                OnStatusUpdate("Printing Started");

                var printerSettings = new PrinterSettings
                {
                    PrinterName = "Microsoft XPS Document Writer",
                    PrintToFile = true,
                    PrintFileName = string.Format("{0}\\{1}.xps", FullPrintJobFolder, SOPInstanceUID.UID)
                };

                printDocument = new PrintDocument
                {
                    PrinterSettings = printerSettings,
                    DocumentName = Thread.CurrentThread.Name,
                    PrintController = new StandardPrintController()
                };

                printDocument.QueryPageSettings += OnQueryPageSettings;
                printDocument.PrintPage += OnPrintPage;

                printDocument.Print();

                Status = PrintJobStatus.Done;

                OnStatusUpdate("Printing Done");
            }
            catch
            {
                Status = PrintJobStatus.Failure;
                OnStatusUpdate("Printing failed");
            }
            finally
            {
                if (printDocument != null)
                {
                    //dispose the print document and unregister events handlers to avoid memory leaks
                    printDocument.QueryPageSettings -= OnQueryPageSettings;
                    printDocument.PrintPage -= OnPrintPage;
                    printDocument.Dispose();
                }
            }
        }

        void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            _currentFilmBox.Print(e.Graphics, e.MarginBounds, 100);

            _currentFilmBox = null;
            _currentPage++;

            e.HasMorePages = _currentPage < FilmBoxFolderList.Count;
        }

        void OnQueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            OnStatusUpdate(string.Format("Printing film {0} of {1}", _currentPage + 1, FilmBoxFolderList.Count));
            var filmBoxFolder = string.Format("{0}\\{1}", FullPrintJobFolder, FilmBoxFolderList[_currentPage]);
            var filmSession = FilmSession.Load(string.Format("{0}\\FilmSession.dcm", filmBoxFolder));
            _currentFilmBox = FilmBox.Load(filmSession, filmBoxFolder);

            e.PageSettings.Margins.Left = 25;
            e.PageSettings.Margins.Right = 25;
            e.PageSettings.Margins.Top = 25;
            e.PageSettings.Margins.Bottom = 25;

            e.PageSettings.Landscape = _currentFilmBox.FilmOrienation == "LANDSCAPE";
        }

        private void DeletePrintFolder()
        {
            var folderInfo = new System.IO.DirectoryInfo(FullPrintJobFolder);
            if (folderInfo.Exists)
            {
                folderInfo.Delete(true);
            }
        }

        #endregion

        #region Notification Methods

        protected virtual void OnStatusUpdate(string info)
        {
            ExecutionStatus = Status.ToString();
            ExecutionStatusInfo = info;

            if (Status != PrintJobStatus.Failure)
            {
                Log.Info("Print Job {0} Status {1}: {2}", SOPInstanceUID.UID.Split('.').Last(), Status, info);
            }
            else
            {
                Log.Error("Print Job {0} Status {1}: {2}", SOPInstanceUID.UID.Split('.').Last(), Status, info);
            }
            if (StatusUpdate != null)
            {
                var args = new StatusUpdateEventArgs((ushort)Status, info, FilmSessionLabel, PrinterName);
                StatusUpdate(this, args);
            }
        }

        #endregion

    }
}
