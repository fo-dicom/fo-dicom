using System;

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

			EventAsyncResult async = new EventAsyncResult(callback, state);

			DicomFileWriter writer = new DicomFileWriter(DicomWriteOptions.Default);
			writer.BeginWrite(target, FileMetaInfo, Dataset, OnWriteComplete, new Tuple<DicomFileWriter, EventAsyncResult>(writer, async));
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
			EventAsyncResult async = result as EventAsyncResult;

			result.AsyncWaitHandle.WaitOne();

			if (async.InternalState != null)
				throw async.InternalState as Exception;
		}

		public static DicomFile Open(string fileName) {
			DicomFile df = new DicomFile();
			FileReference file = new FileReference(fileName);
			FileByteSource source = new FileByteSource(file);

			DicomFileReader reader = new DicomFileReader();
			reader.Read(source,
				new DicomDatasetReaderObserver(df.FileMetaInfo),
				new DicomDatasetReaderObserver(df.Dataset));

			df.Dataset.InternalTransferSyntax = df.FileMetaInfo.TransferSyntax;
			df.File = file;

			return df;
		}

		public static IAsyncResult BeginOpen(string fileName, AsyncCallback callback, object state) {
			DicomFile df = new DicomFile();
			df.File = new FileReference(fileName);
			FileByteSource source = new FileByteSource(df.File);

			EventAsyncResult async = new EventAsyncResult(callback, state);

			DicomFileReader reader = new DicomFileReader();
			reader.BeginRead(source,
				new DicomDatasetReaderObserver(df.FileMetaInfo),
				new DicomDatasetReaderObserver(df.Dataset),
				OnReadComplete, new Tuple<DicomFileReader, DicomFile, EventAsyncResult>(reader, df, async));

			return async;
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

			EventAsyncResult async = result as EventAsyncResult;
			var state = async.InternalState as Tuple<DicomFile, Exception>;

			if (state.Item2 != null)
				throw state.Item2;

			return state.Item1;
		}
	}
}
