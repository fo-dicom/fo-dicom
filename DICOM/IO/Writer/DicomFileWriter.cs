// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Writer
{
    /// <summary>
    /// Writer for DICOM Part 10 objects.
    /// </summary>
    public class DicomFileWriter
    {
        private EventAsyncResult _async;

        private Exception _exception;

        private IByteTarget _target;

        private DicomFileMetaInformation _fileMetaInfo;

        private DicomDataset _dataset;

        private readonly DicomWriteOptions _options;

        /// <summary>
        /// Initializes an instance of a <see cref="DicomFileWriter"/>.
        /// </summary>
        /// <param name="options">Writer options.</param>
        public DicomFileWriter(DicomWriteOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Gets the byte target written to.
        /// </summary>
        public IByteTarget Target
        {
            get
            {
                return _target;
            }
        }

        /// <summary>
        /// Write DICOM Part 10 object to <paramref name="target"/>.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="fileMetaInfo">File meta information-</param>
        /// <param name="dataset">Dataset.</param>
        public void Write(IByteTarget target, DicomFileMetaInformation fileMetaInfo, DicomDataset dataset)
        {
            EndWrite(BeginWrite(target, fileMetaInfo, dataset, null, null));
        }

        public IAsyncResult BeginWrite(
            IByteTarget target,
            DicomFileMetaInformation fileMetaInfo,
            DicomDataset dataset,
            AsyncCallback callback,
            object state)
        {
            _target = target;
            _fileMetaInfo = fileMetaInfo;
            _dataset = dataset;

            _async = new EventAsyncResult(callback, state);

            byte[] preamble = new byte[132];
            preamble[128] = (byte)'D';
            preamble[129] = (byte)'I';
            preamble[130] = (byte)'C';
            preamble[131] = (byte)'M';

            _target.Write(preamble, 0, 132, OnCompletePreamble, null);

            return _async;
        }

        public void EndWrite(IAsyncResult result)
        {
            _async.AsyncWaitHandle.WaitOne();
            if (_exception != null) throw _exception;
        }

        /// <summary>
        /// Continuation when preamble is written.
        /// </summary>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="state">Object state (ignored.)</param>
        private void OnCompletePreamble(IByteTarget target, object state)
        {
            // recalculate FMI group length as required by standard
            _fileMetaInfo.RecalculateGroupLengths();

            DicomWriter writer = new DicomWriter(DicomTransferSyntax.ExplicitVRLittleEndian, _options, _target);
            DicomDatasetWalker walker = new DicomDatasetWalker(_fileMetaInfo);
            walker.BeginWalk(writer, OnCompleteFileMetaInfo, walker);
        }

        /// <summary>
        /// Continuation when file meta information has been written.
        /// </summary>
        /// <param name="result">Dataset walker.</param>
        private void OnCompleteFileMetaInfo(IAsyncResult result)
        {
            try
            {
                DicomDatasetWalker walker;

                if (result != null)
                {
                    walker = result.AsyncState as DicomDatasetWalker;
                    walker.EndWalk(result);
                }

                DicomTransferSyntax syntax = _fileMetaInfo.TransferSyntax;

                if (_options.KeepGroupLengths)
                {
                    // update transfer syntax and recalculate existing group lengths
                    _dataset.InternalTransferSyntax = syntax;
                    _dataset.RecalculateGroupLengths(false);
                }
                else
                {
                    // remove group lengths as suggested in PS 3.5 7.2
                    //
                    //	2. It is recommended that Group Length elements be removed during storage or transfer 
                    //	   in order to avoid the risk of inconsistencies arising during coercion of data 
                    //	   element values and changes in transfer syntax.
                    _dataset.RemoveGroupLengths();
                }

                DicomWriter writer = new DicomWriter(syntax, _options, _target);
                walker = new DicomDatasetWalker(_dataset);
                walker.BeginWalk(writer, OnCompleteDataset, walker);
            }
            catch (Exception e)
            {
                _exception = e;
                _async.Set();
            }
        }

        /// <summary>
        /// Continuation when dataset has been written.
        /// </summary>
        /// <param name="result">Dataset walker.</param>
        private void OnCompleteDataset(IAsyncResult result)
        {
            try
            {
                DicomDatasetWalker walker = result.AsyncState as DicomDatasetWalker;
                walker.EndWalk(result);
            }
            catch (Exception e)
            {
                _exception = e;
            }
            finally
            {
                _async.Set();
            }
        }
    }
}
