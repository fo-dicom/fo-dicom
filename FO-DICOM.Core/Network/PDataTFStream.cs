using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    internal class PDataTFStream : Stream
    {
        #region Private Members

        private readonly DicomService _service;

        private bool _command;

        private readonly uint _pduMax;

        private uint _max;

        private readonly byte _pcid;

        private readonly DicomMessage _dicomMessage;

        private PDataTF _pdu;

        private byte[] _bytes;

        private int _length;

        #endregion

        #region Public Constructors

        public PDataTFStream(DicomService service, byte pcid, uint max, DicomMessage dicomMessage)
        {
            _service = service;
            _command = true;
            _pcid = pcid;
            _dicomMessage = dicomMessage;
            _pduMax = Math.Min(max, int.MaxValue);
            _max = _pduMax == 0
                ? _service.Options.MaxCommandBuffer
                : Math.Min(_pduMax, _service.Options.MaxCommandBuffer);

            _pdu = new PDataTF();

            // Max PDU Size - Current Size - Size of PDV header
            _bytes = new byte[_max - CurrentPduSize() - 6];
        }

        #endregion

        #region Public Properties

        public async Task SetIsCommandAsync(bool value)
        {
            // recalculate maximum PDU buffer size
            if (_command != value)
            {
                _max = _pduMax == 0
                    ? _service.Options.MaxCommandBuffer
                    : Math.Min(
                        _pduMax,
                        value ? _service.Options.MaxCommandBuffer : _service.Options.MaxDataBuffer);

                await CreatePDVAsync(true).ConfigureAwait(false);
                _command = value;
            }
        }

        #endregion

        #region Private Members

        private uint CurrentPduSize()
        {
            // PDU header + PDV header + PDV data
            return 6 + _pdu.GetLengthOfPDVs();
        }

        private async Task CreatePDVAsync(bool last)
        {
            try
            {
                if (_bytes == null)
                {
                    _bytes = new byte[0];
                }

                if (_length < _bytes.Length)
                {
                    Array.Resize(ref _bytes, _length);
                }

                var pdv = new PDV(_pcid, _bytes, _command, last);
                _pdu.PDVs.Add(pdv);

                // reset length in case we recurse into WritePDU()
                _length = 0;
                // is the current PDU at its maximum size or do we have room for another PDV?
                if (_service.Options.MaxPDVsPerPDU != 0 && _pdu.PDVs.Count >= _service.Options.MaxPDVsPerPDU
                    || CurrentPduSize() + 6 >= _max || !_command && last)
                {
                    await WritePDUAsync(last).ConfigureAwait(false);
                }

                // Max PDU Size - Current Size - Size of PDV header
                uint max = _max - CurrentPduSize() - 6;
                _bytes = last ? null : new byte[max];
            }
            catch (Exception e)
            {
                _service.Logger.Error("Exception creating PDV: {@error}", e);
                throw;
            }
        }

        private async Task WritePDUAsync(bool last)
        {
            // Immediately stop sending PDUs if the message is no longer pending (e.g. because it timed out)
            if (_dicomMessage is DicomRequest req && !_service.IsStillPending(req))
            {
                _pdu.Dispose();
                _pdu = new PDataTF();
                return;
            }

            if (_length > 0)
            {
                await CreatePDVAsync(last).ConfigureAwait(false);
            }

            if (_pdu.PDVs.Count > 0)
            {
                if (last)
                {
                    _pdu.PDVs[_pdu.PDVs.Count - 1].IsLastFragment = true;
                }

                await _service.SendPDUAsync(_pdu).ConfigureAwait(false);

                _dicomMessage.LastPDUSent = DateTime.Now;

                _pdu.Dispose();
                _pdu = new PDataTF();
            }
        }

        #endregion

        #region Stream Members

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override void Flush() { }

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                WriteAsync(buffer, offset, count, (CancellationToken)CancellationToken.None).Wait();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count,
            CancellationToken cancellationToken)
        {
            try
            {
                if (_bytes == null || _bytes.Length == 0)
                {
                    // Max PDU Size - Current Size - Size of PDV header
                    uint max = _max - CurrentPduSize() - 6;
                    _bytes = new byte[max];
                }

                while (count >= _bytes.Length - _length)
                {
                    var c = Math.Min(count, _bytes.Length - _length);

                    Array.Copy(buffer, offset, _bytes, _length, c);

                    _length += c;
                    offset += c;
                    count -= c;

                    await CreatePDVAsync(false).ConfigureAwait(false);
                }

                if (count > 0)
                {
                    Array.Copy(buffer, offset, _bytes, _length, count);
                    _length += count;

                    if (_bytes.Length == _length)
                    {
                        await CreatePDVAsync(false).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception e)
            {
                _service.Logger.Error("Exception writing data to PDV: {@error}", e);
                throw;
            }
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            await CreatePDVAsync(true).ConfigureAwait(false);
            await WritePDUAsync(true).ConfigureAwait(false);
        }

        #endregion
    }
}