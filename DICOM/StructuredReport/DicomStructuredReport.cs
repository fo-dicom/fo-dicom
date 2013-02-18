using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;
using Dicom.IO.Reader;
using Dicom.IO.Writer;

namespace Dicom.StructuredReport {
	public class DicomStructuredReport : DicomFile {
		protected override void OnSave() {
		}

		public new static DicomStructuredReport Open(string fileName) {
			var df = new DicomStructuredReport();

			// reset datasets
			df.FileMetaInfo.Clear();
			df.Dataset.Clear();

			try {
				df.File = new FileReference(fileName);

				using (var source = new FileByteSource(df.File)) {
					DicomFileReader reader = new DicomFileReader();

					var datasetObserver = new DicomDatasetReaderObserver(df.Dataset);
					
					reader.Read(source,
						new DicomDatasetReaderObserver(df.FileMetaInfo),
						datasetObserver);

					df.Format = reader.FileFormat;

					df.Dataset.InternalTransferSyntax = reader.Syntax;

					return df;
				}
			} catch (Exception e) {
				throw new DicomFileException(df, e.Message, e);
			}
		}

		public new static IAsyncResult BeginOpen(string fileName, AsyncCallback callback, object state) {
			var df = new DicomStructuredReport();

			// reset datasets
			df.FileMetaInfo.Clear();
			df.Dataset.Clear();

			df.File = new FileReference(fileName);

			FileByteSource source = new FileByteSource(df.File);

			EventAsyncResult result = new EventAsyncResult(callback, state);

			DicomFileReader reader = new DicomFileReader();

			var datasetObserver = new DicomDatasetReaderObserver(df.Dataset);

			reader.BeginRead(source,
				new DicomDatasetReaderObserver(df.FileMetaInfo),
				datasetObserver,
				OnReadComplete, new Tuple<DicomFileReader, DicomStructuredReport, EventAsyncResult>(reader, df, result));

			return result;
		}
		private static void OnReadComplete(IAsyncResult result) {
			var state = result.AsyncState as Tuple<DicomFileReader, DicomStructuredReport, EventAsyncResult>;

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

			state.Item3.InternalState = new Tuple<DicomStructuredReport, Exception>(state.Item2, e);
			state.Item3.Set();
		}

		public new static DicomStructuredReport EndOpen(IAsyncResult result) {
			result.AsyncWaitHandle.WaitOne();

			EventAsyncResult eventResult = result as EventAsyncResult;
			var state = eventResult.InternalState as Tuple<DicomStructuredReport, Exception>;

			if (state.Item2 != null)
				throw new DicomFileException(state.Item1, state.Item2.Message, state.Item2);

			return state.Item1;
		}
	}
}
