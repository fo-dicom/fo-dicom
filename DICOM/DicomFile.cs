using System;
using System.IO;
using Dicom.IO;

using Dicom.IO.Reader;
using Dicom.IO.Writer;

namespace Dicom {
	public class DicomFile {
		public DicomFile() {
			FileMetaInfo = new DicomFileMetaInformation();
			Dataset = new DicomDataset();
			Format = DicomFileFormat.DICOM3;
		}

		public DicomFile(DicomDataset dataset) {
			Dataset = dataset;
			FileMetaInfo = new DicomFileMetaInformation(Dataset);
			Format = DicomFileFormat.DICOM3;
		}

		public FileReference File {
			get;
			protected set;
		}

		public DicomFileFormat Format {
			get;
			protected set;
		}

		public DicomFileMetaInformation FileMetaInfo {
			get;
			protected set;
		}

		public DicomDataset Dataset {
			get;
			protected set;
		}

		protected virtual void OnSave() {
		}

		public void Save(string fileName) {
			if (Format == DicomFileFormat.ACRNEMA1 || Format == DicomFileFormat.ACRNEMA2)
				throw new DicomFileException(this, "Unable to save ACR-NEMA file");

			if (Format == DicomFileFormat.DICOM3NoFileMetaInfo) {
				// create file meta information from dataset
				FileMetaInfo = new DicomFileMetaInformation(Dataset);
			}

			File = new FileReference(fileName);
			File.Delete();

			OnSave();

			using (var target = new FileByteTarget(File)) {
				DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
				writer.Write(target, FileMetaInfo, Dataset);
			}
		}

		public void Save(Stream stream) {
			if (Format == DicomFileFormat.ACRNEMA1 || Format == DicomFileFormat.ACRNEMA2)
				throw new DicomFileException(this, "Unable to save ACR-NEMA file");

			if (Format == DicomFileFormat.DICOM3NoFileMetaInfo) {
				// create file meta information from dataset
				FileMetaInfo = new DicomFileMetaInformation(Dataset);
			}

			OnSave();

			using (var target = new StreamByteTarget(stream)) {
				DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
				writer.Write(target, FileMetaInfo, Dataset);
			}
		}

		public void BeginSave(string fileName, AsyncCallback callback, object state) {
			if (Format == DicomFileFormat.ACRNEMA1 || Format == DicomFileFormat.ACRNEMA2)
				throw new DicomFileException(this, "Unable to save ACR-NEMA file");

			if (Format == DicomFileFormat.DICOM3NoFileMetaInfo) {
				// create file meta information from dataset
				FileMetaInfo = new DicomFileMetaInformation(Dataset);
			}

			File = new FileReference(fileName);
			File.Delete();

			OnSave();

			FileByteTarget target = new FileByteTarget(File);

			EventAsyncResult result = new EventAsyncResult(callback, state);

			DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
			writer.BeginWrite(target, FileMetaInfo, Dataset, OnWriteComplete, new Tuple<DicomFileWriter, EventAsyncResult>(writer, result));
		}
		private static void OnWriteComplete(IAsyncResult result) {
			var state = result.AsyncState as Tuple<DicomFileWriter, EventAsyncResult>;

			try {
				state.Item1.EndWrite(result);

				// ensure that file handles are closed
				var target = (FileByteTarget)state.Item1.Target;
				target.Dispose();
			} catch (Exception ex) {
				state.Item2.InternalState = ex;
			}

			state.Item2.Set();
		}

		public void EndSave(IAsyncResult result) {
			EventAsyncResult eventResult = result as EventAsyncResult;

			result.AsyncWaitHandle.WaitOne();

			if (eventResult.InternalState != null)
				throw eventResult.InternalState as Exception;
		}

        /// <summary>
        /// Reads the specified filename and returns a DicomFile object.  Note that the values for large
        /// DICOM elements (e.g. PixelData) are read in "on demand" to conserve memory.  Large DICOM elements
        /// are determined by their size in bytes - see the default value for this in the FileByteSource._largeObjectSize
        /// </summary>
        /// <param name="fileName">The filename of the DICOM file</param>
        /// <returns>DicomFile instance</returns>
		public static DicomFile Open(string fileName) {
			DicomFile df = new DicomFile();

			try {
				df.File = new FileReference(fileName);

				using (var source = new FileByteSource(df.File)) {
					DicomFileReader reader = new DicomFileReader();
					reader.Read(source,
						new DicomDatasetReaderObserver(df.FileMetaInfo),
						new DicomDatasetReaderObserver(df.Dataset));

					df.Format = reader.FileFormat;

					df.Dataset.InternalTransferSyntax = reader.Syntax;

					return df;
				}
			} catch (Exception e) {
				throw new DicomFileException(df, e.Message, e);
			}
		}

        public static DicomFile Open(Stream stream)
        {
            var df = new DicomFile();

			try {
				var source = new StreamByteSource(stream);

				var reader = new DicomFileReader();
				reader.Read(source,
					new DicomDatasetReaderObserver(df.FileMetaInfo),
					new DicomDatasetReaderObserver(df.Dataset));

				df.Format = reader.FileFormat;

				df.Dataset.InternalTransferSyntax = reader.Syntax;

				return df;
			} catch (Exception e) {
				throw new DicomFileException(df, e.Message, e);
			}
        }

        public static IAsyncResult BeginOpen(string fileName, AsyncCallback callback, object state)
        {
			DicomFile df = new DicomFile();
			df.File = new FileReference(fileName);

			FileByteSource source = new FileByteSource(df.File);

			EventAsyncResult result = new EventAsyncResult(callback, state);

			DicomFileReader reader = new DicomFileReader();
			reader.BeginRead(source,
				new DicomDatasetReaderObserver(df.FileMetaInfo),
				new DicomDatasetReaderObserver(df.Dataset),
				OnReadComplete, new Tuple<DicomFileReader, DicomFile, EventAsyncResult>(reader, df, result));

			return result;
		}
		private static void OnReadComplete(IAsyncResult result) {
			var state = result.AsyncState as Tuple<DicomFileReader, DicomFile, EventAsyncResult>;

			Exception e = null;
			try {
				state.Item1.EndRead(result);

				// ensure that file handles are closed
				var source = (FileByteSource)state.Item1.Source;
				source.Dispose();

				state.Item2.Format = state.Item1.FileFormat;
				state.Item2.Dataset.InternalTransferSyntax = state.Item1.Syntax;
			} catch (Exception ex) {
				state.Item2.Format = state.Item1.FileFormat;
				e = ex;
			}

			state.Item3.InternalState = new Tuple<DicomFile, Exception>(state.Item2, e);
			state.Item3.Set();
		}

		public static DicomFile EndOpen(IAsyncResult result) {
			result.AsyncWaitHandle.WaitOne();

			EventAsyncResult eventResult = result as EventAsyncResult;
			var state = eventResult.InternalState as Tuple<DicomFile, Exception>;

			if (state.Item2 != null)
				throw new DicomFileException(state.Item1, state.Item2.Message, state.Item2);

			return state.Item1;
		}

		public override string ToString() {
			return String.Format("DICOM File [{0}]", Format);
		}

		/// <summary>
		/// Test if file has a valid preamble and DICOM 3.0 header.
		/// </summary>
		/// <param name="path">Path to file</param>
		/// <returns>True if valid DICOM 3.0 file header is detected.</returns>
		public static bool HasValidHeader(string path) {
			try {
				using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
					fs.Seek(128, SeekOrigin.Begin);

					bool magic = false;
					if (fs.ReadByte() == 'D' &&
						fs.ReadByte() == 'I' &&
						fs.ReadByte() == 'C' &&
						fs.ReadByte() == 'M')
						magic = true;

					fs.Close();

					return magic;
				}
			} catch {
				return false;
			}
		}
	}
}
