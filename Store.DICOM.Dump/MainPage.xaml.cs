using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Dicom;
using Dicom.Network;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.DICOM.Dump
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
	{
		#region INNER CLASSES

	    public class ModalityGroup
		{
			public string Modality { get; set; }
			public ICollection<DicomFileImage> FileImages { get; set; }
		}

		#endregion

		#region FIELDS

	    private readonly static ObservableCollection<ModalityGroup> _modalityGroups =
		    new ObservableCollection<ModalityGroup>();
 
		private DicomServer<CEchoScp> _server;
	
        #endregion

		#region CONSTRUCTORS

		public MainPage()
		{
			InitializeComponent();
		}
		
		#endregion

        #region PROPERTIES

	    public ObservableCollection<ModalityGroup> ModalityGroups
	    {
			get { return _modalityGroups; }
	    }

        #endregion

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void OpenFileButtonOnTapped(object sender, TappedRoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.DocumentsLibrary };
            filePicker.FileTypeFilter.Add("*");

	        var files = await filePicker.PickMultipleFilesAsync();
			if (files == null || files.Count == 0) return;

			foreach (var file in files)
	        {
		        try
		        {
					var storeStream = await file.OpenAsync(FileAccessMode.Read);
					var dicomFile = DicomFile.Open(storeStream.AsStream());
			        var fileImage = new DicomFileImage(Path.GetFileNameWithoutExtension(file.Path), file, dicomFile);

			        var modality = fileImage.Modality;
			        var modalityGroup = _modalityGroups.FirstOrDefault(m => m.Modality.Equals(modality));
					if (modalityGroup == null)
					{
						_modalityGroups.Add(new ModalityGroup { Modality = modality, FileImages = new ObservableCollection<DicomFileImage> { fileImage }});
					}
					else
					{
						modalityGroup.FileImages.Add(fileImage);
					}
		        }
		        catch
		        {
		        }
	        }

		}

		private void DicomEchoButtonOnTapped(object sender, TappedRoutedEventArgs e)
	    {
		    var client = new DicomClient();
			var request = new DicomCEchoRequest
				              {
					              OnResponseReceived =
						              async (req, res) =>
						              await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						                                        () => EchoStatus.Text = res.Status.Description)
				              };
		    client.AddRequest(request);
			client.Send("server", 104, false, "cureos", "cureos");
	    }

	    private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
	    {
			_server = new DicomServer<CEchoScp>(104);
	    }

	    private void DicomFilesViewOnItemClick(object sender, ItemClickEventArgs e)
	    {
			Frame.Navigate(typeof(ViewDicomFileImagePage), e.ClickedItem);
		}

	    private void RemoveButtonOnTapped(object sender, TappedRoutedEventArgs e)
	    {
			_modalityGroups.Clear();
	    }
	}
}
