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
		}

		public DicomFile(DicomDataset dataset) {
			Dataset = dataset;
			FileMetaInfo = new DicomFileMetaInformation(Dataset);
		}

		public FileReference File {
			get;
			private set;
		}

		public DicomFileMetaInformation FileMetaInfo {
			get;
			private set;
		}

		public DicomDataset Dataset {
			get;
			private set;
		}

		public void Save(string fileName) {
			File = new FileReference(fileName);
			File.Delete();

			FileByteTarget target = new FileByteTarget(File);

			DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
			writer.Write(target, FileMetaInfo, Dataset);

			target.Close();
		}

		public void BeginSave(string fileName, AsyncCallback callback, object state) {
			File = new FileReference(fileName);
			File.Delete();

			FileByteTarget target = new FileByteTarget(File);

			EventAsyncResult result = new EventAsyncResult(callback, state);

			DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
			writer.BeginWrite(target, FileMetaInfo, Dataset, OnWriteComplete, new Tuple<DicomFileWriter, EventAsyncResult>(writer, result));
		}
		private static void OnWriteComplete(IAsyncResult result) {
			var state = result.AsyncState as Tuple<DicomFileWriter, EventAsyncResult>;

			try {
				state.Item1.EndWrite(result);
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

		public static DicomFile Open(string fileName) {
			DicomFile df = new DicomFile();

			try {
				df.File = new FileReference(fileName);
				FileByteSource source = new FileByteSource(df.File);

				DicomFileReader reader = new DicomFileReader();
				reader.Read(source,
					new DicomDatasetReaderObserver(df.FileMetaInfo),
					new DicomDatasetReaderObserver(df.Dataset));

				df.Dataset.InternalTransferSyntax = df.FileMetaInfo.TransferSyntax;

				return df;
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

				df.Dataset.InternalTransferSyntax = df.FileMetaInfo.TransferSyntax;

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
				state.Item2.Dataset.InternalTransferSyntax = state.Item2.FileMetaInfo.TransferSyntax;
			} catch (Exception ex) {
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
	}
}
