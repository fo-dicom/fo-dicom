﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.IO;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Threading;

namespace FellowOakDicom.Network
{
    #region Raw PDU

    /// <summary>Encapsulates PDU data for reading or writing</summary>
    public class RawPDU : IDisposable, IAsyncDisposable
    {
        #region Private members

        private readonly Encoding _encoding;

        private readonly Stream _stream;
        
        private readonly bool _leaveOpen;

        private readonly BinaryReader _br;

        private readonly BinaryWriter _bw;

        private readonly Stack<long> _m16;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes new PDU for writing
        /// </summary>
        /// <param name="type">Type of PDU</param>
        /// <param name="encoding">The encoding to use for encoding text</param>
        public RawPDU(byte type, Encoding encoding = null)
        {
            Type = type;
            _encoding = encoding ?? DicomEncoding.Default;
            _stream = new MemoryStream();
            _leaveOpen = true;
            _stream.Seek(0, SeekOrigin.Begin);
            _bw = EndianBinaryWriter.Create(_stream, _encoding, Endian.Big, _leaveOpen);
            _m16 = new Stack<long>();
        }

        /// <summary>
        /// Initializes new PDU for writing to a stream
        /// </summary>
        /// <param name="type">Type of PDU</param>
        /// <param name="encoding">The encoding to use for encoding text</param>
        /// <param name="stream">The stream to write to</param>
        /// <param name="leaveOpen">Whether the stream should be disposed or not when this RawPDU is disposed</param>
        public RawPDU(byte type, Encoding encoding, Stream stream, bool leaveOpen)
        {
            Type = type;
            _encoding = encoding ?? DicomEncoding.Default;
            _stream = stream;
            _leaveOpen = leaveOpen;
            _bw = EndianBinaryWriter.Create(_stream, _encoding, Endian.Big, _leaveOpen);
        }

        /// <summary>
        /// Initializes new PDU reader from buffer
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="encoding">The encoding to use for decoding text</param>
        public RawPDU(byte[] buffer, Encoding encoding = null)
        {
            _stream = new MemoryStream(buffer);
            _leaveOpen = true;
            _encoding = encoding ?? DicomEncoding.Default;
            _br = EndianBinaryReader.Create(_stream, _encoding, Endian.Big, _leaveOpen);
            Type = _br.ReadByte();
            _stream.Seek(6, SeekOrigin.Begin);
        }

        /// <summary>
        /// Initializes new PDU reader from a stream
        /// </summary>
        /// <remarks>The created object takes ownership of the stream</remarks>
        /// <param name="stream">Stream</param>
        /// <param name="encoding">The encoding to use for decoding text</param>
        public RawPDU(MemoryStream stream, Encoding encoding = null)
        {
            _encoding = encoding ?? DicomEncoding.Default;
            _stream = stream;
            _leaveOpen = true;
            _stream.Seek(0, SeekOrigin.Begin);
            _br = EndianBinaryReader.Create(_stream, Endian.Big, _leaveOpen);
            Type = _br.ReadByte();
            _stream.Seek(6, SeekOrigin.Begin);
        }

        #endregion

        #region Public Properties

        /// <summary>PDU type</summary>
        public byte Type { get; }

        /// <summary>PDU length</summary>
        public uint Length => (uint)_stream.Length;

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes PDU to stream
        /// </summary>
        /// <param name="s">Output stream</param>
        public void WritePDU(Stream s)
        {
            byte[] preamble = ArrayPool<byte>.Shared.Rent(6);
            try
            {
                GetCommonFields(preamble);
                s.Write(preamble, 0, 6);
                _stream.Seek(0, SeekOrigin.Begin);
                _stream.CopyTo(s);
                s.Flush();
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(preamble);
            }
        }

        /// <summary>
        /// Writes PDU to stream
        /// </summary>
        /// <param name="s">Output stream</param>
        public async Task WritePDUAsync(Stream s, CancellationToken cancellationToken)
        {
            byte[] preamble = ArrayPool<byte>.Shared.Rent(6);
            try
            {
                GetCommonFields(preamble);
                await s.WriteAsync(preamble, 0, 6, cancellationToken).ConfigureAwait(false);
                _stream.Seek(0, SeekOrigin.Begin);
                await _stream.CopyToAsync(s, 81920, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(preamble);
            }
        }

        /// <summary>
        /// Saves PDU to file
        /// </summary>
        /// <param name="file">file</param>
        public void Save(IFileReference file)
        {
            var d = file.Directory;

            if (!d.Exists)
            {
                d.Create();
            }
            using var fs = file.OpenWrite();
            WritePDU(fs);
        }

        /// <summary>
        /// Gets string describing this PDU
        /// </summary>
        /// <returns>PDU description</returns>
        public override string ToString() => $"Pdu[type={Type:X2}, length={Length}]";

        /// <summary>
        /// Reset PDU read stream
        /// </summary>
        public void Reset()
        {
            _stream.Seek(0, SeekOrigin.Begin);
        }

        #region Read Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckOffset(int bytes, string name)
        {
            if (_stream.Position + bytes > _stream.Length)
            {
                var msg = $"{ToString()} (offset={_stream.Position}, bytes={bytes}, field=\"{name}\") Requested offset out of range!";
                throw new DicomNetworkException(msg);
            }
        }

        /// <summary>
        /// Read byte from PDU
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <returns>Field value</returns>
        public byte ReadByte(string name)
        {
            CheckOffset(1, name);
            return _br.ReadByte();
        }

        /// <summary>
        /// Read bytes from PDU
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <param name="count">Number of bytes to read</param>
        /// <returns>Field value</returns>
        public byte[] ReadBytes(string name, int count)
        {
            CheckOffset(count, name);
            return _br.ReadBytes(count);
        }

        /// <summary>
        /// Read ushort from PDU
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <returns>Field value</returns>
        public ushort ReadUInt16(string name)
        {
            CheckOffset(2, name);
            return _br.ReadUInt16();
        }

        /// <summary>
        /// Reads uint from PDU
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <returns>Field value</returns>
        public uint ReadUInt32(string name)
        {
            CheckOffset(4, name);
            return _br.ReadUInt32();
        }

        private readonly char[] _trimChars = { ' ', '\0' };

        /// <summary>
        /// Reads string from PDU
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <param name="numberOfBytes">Number of bytes to read</param>
        /// <returns>Field value</returns>
        public string ReadString(string name, int numberOfBytes)
        {
            var bytes = ReadBytes(name, numberOfBytes);

            return _encoding.GetString(bytes).Trim(_trimChars);
        }

        /// <summary>
        /// Skips ahead in PDU
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <param name="count">Number of bytes to skip</param>
        public void SkipBytes(string name, int count)
        {
            CheckOffset(count, name);
            _stream.Seek(count, SeekOrigin.Current);
        }

        #endregion

        #region Write Methods

        /// <summary>
        /// Writes byte to PDU
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        public void Write(string name, byte value)
        {
            _bw.Write(value);
        }

        /// <summary>
        /// Writes byte to PDU multiple times
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="count">Number of times to write PDU value</param>
        public void Write(string name, byte value, int count)
        {
            for (var i = 0; i < count; i++)
            {
                _bw.Write(value);
            }
        }

        /// <summary>
        /// Writes byte[] to PDU
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        public void Write(string name, byte[] value)
        {
            _bw.Write(value);
        }

        /// <summary>
        /// Writes ushort to PDU
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        public void Write(string name, ushort value)
        {
            _bw.Write(value);
        }

        /// <summary>
        /// Writes uint to PDU
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        public void Write(string name, uint value)
        {
            _bw.Write(value);
        }

        /// <summary>
        /// Writes string to PDU
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        public void Write(string name, string value)
        {
            _bw.Write(value.ToCharArray());
        }

        /// <summary>
        /// Writes string to PDU
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <param name="count">Number of characters to write</param>
        /// <param name="pad">Padding character</param>
        public void Write(string name, string value, int count, char pad)
        {
            _bw.Write(ToCharArray(value, count, pad));
        }

        /// <summary>
        /// Marks position to write 16-bit length value
        /// </summary>
        /// <param name="name">Field name</param>
        public void MarkLength16(string name)
        {
            _m16.Push(_stream.Position);
            _bw.Write((ushort)0);
        }

        /// <summary>
        /// Writes 16-bit length to top length marker
        /// </summary>
        public void WriteLength16()
        {
            var p1 = _m16.Pop();
            var p2 = _stream.Position;
            _stream.Position = p1;
            _bw.Write((ushort)(p2 - p1 - 2));
            _stream.Position = p2;
        }

        private static char[] ToCharArray(string s, int l, char p)
        {
            var c = new char[l];
            for (var i = 0; i < l; i++)
            {
                c[i] = i < s.Length ? s[i] : p;
            }
            return c;
        }

        #endregion

        #endregion

        public void Dispose()
        {
            _br?.Dispose();
            _bw?.Dispose();
            if (!_leaveOpen)
            {
                _stream.Dispose();
            }
        }

        public ValueTask DisposeAsync()
        {
            _br?.Dispose();
            _bw?.Dispose();
            if(!_leaveOpen)
            {
                _stream.Dispose();
            }

            return default;
        }

        /// <summary>
        /// Gets the first fields common to all PDUs (Type, Reserved, PDU-length)
        /// </summary>
        private void GetCommonFields(byte[] buffer)
        {
            unchecked
            {
                var length = (uint)_stream.Length;
                buffer[0] = Type;
                buffer[1] = 0;
                buffer[2] = (byte)((length & 0xff000000U) >> 24);
                buffer[3] = (byte)((length & 0x00ff0000U) >> 16);
                buffer[4] = (byte)((length & 0x0000ff00U) >> 8);
                buffer[5] = (byte)(length & 0x000000ffU);
            }
        }

    }

    #endregion

    #region PDU Interface

    /// <summary>
    /// Interface for PDU
    /// </summary>
    public interface PDU
    {
        /// <summary>
        /// Writes PDU to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        RawPDU Write();
        
        /// <summary>
        /// Writes PDU to stream
        /// </summary>
        Task WriteAsync(Stream stream, CancellationToken cancellationToken);

        /// <summary>
        /// Reads PDU from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        void Read(RawPDU raw);
    }

    #endregion

    #region A-Associate-RQ

    /// <summary>A-ASSOCIATE-RQ</summary>
    public class AAssociateRQ : PDU
    {
        private readonly DicomAssociation _assoc;

        /// <summary>
        /// Initializes new A-ASSOCIATE-RQ
        /// </summary>
        /// <param name="assoc">Association parameters</param>
        public AAssociateRQ(DicomAssociation assoc)
        {
            _assoc = assoc;
        }

        public override string ToString() => "A-ASSOCIATE-RQ";

        #region EVENTS

        /// <summary>
        /// Event to handle unsupported PDU bytes.
        /// </summary>
        public event PDUBytesHandler HandlePDUBytes;

        #endregion

        #region Write

        /// <summary>
        /// Writes A-ASSOCIATE-RQ to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x01);

            Write(pdu);

            return pdu;
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            await using var rawPdu = new RawPDU(0x01);

            Write(rawPdu);

            await rawPdu.WritePDUAsync(stream, cancellationToken).ConfigureAwait(false);
        }

        private void Write(RawPDU pdu)
        {
            pdu.Write("Version", (ushort)0x0001);
            pdu.Write("Reserved", 0x00, 2);
            pdu.Write("Called AE", _assoc.CalledAE, 16, ' ');
            pdu.Write("Calling AE", _assoc.CallingAE, 16, ' ');
            pdu.Write("Reserved", 0x00, 32);

            // Application Context
            pdu.Write("Item-Type", 0x10);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Application Context Name", DicomUID.DICOMApplicationContext.UID);
            pdu.WriteLength16();

            foreach (var pc in _assoc.PresentationContexts)
            {
                // Presentation Context
                pdu.Write("Item-Type", 0x20);
                pdu.Write("Reserved", 0x00);
                pdu.MarkLength16("Item-Length");
                pdu.Write("Presentation Context ID", pc.ID);
                pdu.Write("Reserved", 0x00, 3);

                // Abstract Syntax
                pdu.Write("Item-Type", 0x30);
                pdu.Write("Reserved", 0x00);
                pdu.MarkLength16("Item-Length");
                pdu.Write("Abstract Syntax UID", pc.AbstractSyntax.UID);
                pdu.WriteLength16();

                // Transfer Syntax
                foreach (var ts in pc.GetTransferSyntaxes())
                {
                    pdu.Write("Item-Type", 0x40);
                    pdu.Write("Reserved", 0x00);
                    pdu.MarkLength16("Item-Length");
                    pdu.Write("Transfer Syntax UID", ts.UID.UID);
                    pdu.WriteLength16();
                }

                pdu.WriteLength16();
            }

            // User Data Fields
            pdu.Write("Item-Type", 0x50);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");

            // Maximum PDU
            pdu.Write("Item-Type", 0x51);
            pdu.Write("Reserved", 0x00);
            pdu.Write("Item-Length", (ushort)0x0004);
            pdu.Write("Max PDU Length", _assoc.Options?.MaxPDULength ?? new DicomServiceOptions().MaxPDULength);

            // Implementation Class UID
            pdu.Write("Item-Type", 0x52);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Implementation Class UID", DicomImplementation.ClassUID.UID);
            pdu.WriteLength16();

            // Asynchronous Operations Negotiation
            if (_assoc.MaxAsyncOpsInvoked != 1 || _assoc.MaxAsyncOpsPerformed != 1)
            {
                pdu.Write("Item-Type", 0x53);
                pdu.Write("Reserved", 0x00);
                pdu.Write("Item-Length", (ushort)0x0004);
                pdu.Write("Asynchronous Operations Invoked", (ushort)_assoc.MaxAsyncOpsInvoked);
                pdu.Write("Asynchronous Operations Performed", (ushort)_assoc.MaxAsyncOpsPerformed);
            }

            // SCP-SCU Role Selection Negotiation
            foreach (var pc in _assoc.PresentationContexts)
            {
                if (pc.UserRole.HasValue || pc.ProviderRole.HasValue)
                {
                    pdu.Write("Item-Type", 0x54);
                    pdu.Write("Reserved", 0x00);
                    pdu.MarkLength16("Item-Length");
                    pdu.MarkLength16("UID-Length");
                    pdu.Write("Abstract Syntax UID", pc.AbstractSyntax.UID);
                    pdu.WriteLength16();
                    pdu.Write("SCU Role", pc.UserRole.GetValueOrDefault() ? (byte)1 : (byte)0);
                    pdu.Write("SCP Role", pc.ProviderRole.GetValueOrDefault() ? (byte)1 : (byte)0);
                    pdu.WriteLength16();
                }
            }

            // Implementation Version
            pdu.Write("Item-Type", 0x55);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Implementation Version", DicomImplementation.Version);
            pdu.WriteLength16();

            // SOP Class Extended Negotiation
            foreach (var ex in _assoc.ExtendedNegotiations.Where(e => e.RequestedApplicationInfo?.Contains(1) == true))
            {
                pdu.Write("Item-Type", 0x56);
                pdu.Write("Reserved", 0x00);
                pdu.MarkLength16("Item-Length");
                pdu.MarkLength16("SOP Class UID-Length");
                pdu.Write("SOP Class UID", ex.SopClassUid.UID);
                pdu.WriteLength16();
                pdu.Write("Service Class Application Information", ex.RequestedApplicationInfo.GetValues());
                pdu.WriteLength16();
            }

            // SOP Class Common Extended Negotiation (only in A-ASSOCIATE-RQ)
            foreach (var commonExNeg in _assoc.ExtendedNegotiations.Where(e => e.ServiceClassUid != null))
            {
                pdu.Write("Item-Type", 0x57);
                pdu.Write("Sub-item-version", 0);
                pdu.MarkLength16("Item-Length");
                pdu.MarkLength16("SOP Class UID-Length");
                pdu.Write("SOP Class UID", commonExNeg.SopClassUid.UID);
                pdu.WriteLength16();
                pdu.MarkLength16("Service Class UID-Length");
                pdu.Write("Service Class UID", commonExNeg.ServiceClassUid.UID);
                pdu.WriteLength16();
                pdu.MarkLength16("Related SOP Class Identification-Length");
                foreach (var relatedSopClass in commonExNeg.RelatedGeneralSopClasses)
                {
                    pdu.MarkLength16("Related SOP Class UID-Length");
                    pdu.Write("Related SOP Class UID", relatedSopClass.UID);
                    pdu.WriteLength16();
                }
                pdu.WriteLength16();
                pdu.WriteLength16();
            }

            pdu.WriteLength16();
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads A-ASSOCIATE-RQ from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        public void Read(RawPDU raw)
        {
            var l = raw.Length - 6;

            raw.ReadUInt16("Version");
            raw.SkipBytes("Reserved", 2);
            _assoc.CalledAE = raw.ReadString("Called AE", 16);
            _assoc.CallingAE = raw.ReadString("Calling AE", 16);
            raw.SkipBytes("Reserved", 32);
            l -= 2 + 2 + 16 + 16 + 32;

            while (l > 0)
            {
                var type = raw.ReadByte("Item-Type");
                raw.SkipBytes("Reserved", 1);
                var il = raw.ReadUInt16("Item-Length");

                l -= 4 + (uint)il;

                if (type == 0x10)
                {
                    // Application Context
                    raw.SkipBytes("Application Context", il);
                }
                else if (type == 0x20)
                {
                    // Presentation Context
                    var id = raw.ReadByte("Presentation Context ID");
                    raw.SkipBytes("Reserved", 3);
                    il -= 4;

                    while (il > 0)
                    {
                        var pt = raw.ReadByte("Presentation Context Item-Type");
                        raw.SkipBytes("Reserved", 1);
                        var pl = raw.ReadUInt16("Presentation Context Item-Length");
                        var sx = raw.ReadString("Presentation Context Syntax UID", pl);
                        if (pt == 0x30)
                        {
                            var pc = new DicomPresentationContext(id, DicomUID.Parse(sx));
                            _assoc.PresentationContexts.Add(pc);
                        }
                        else if (pt == 0x40)
                        {
                            var pc = _assoc.PresentationContexts[id];
                            pc.AddTransferSyntax(DicomTransferSyntax.Parse(sx));
                        }
                        il -= (ushort)(4 + pl);
                    }
                }
                else if (type == 0x50)
                {
                    // User Information
                    while (il > 0)
                    {
                        var ut = raw.ReadByte("User Information Item-Type");
                        raw.SkipBytes("Reserved", 1);
                        var ul = raw.ReadUInt16("User Information Item-Length");
                        il -= (ushort)(4 + ul);
                        if (ut == 0x51)
                        {
                            _assoc.MaximumPDULength = raw.ReadUInt32("Max PDU Length");
                        }
                        else if (ut == 0x52)
                        {
                            _assoc.RemoteImplementationClassUID =
                                new DicomUID(
                                    raw.ReadString("Implementation Class UID", ul),
                                    "Implementation Class UID",
                                    DicomUidType.Unknown);
                        }
                        else if (ut == 0x55)
                        {
                            _assoc.RemoteImplementationVersion = raw.ReadString("Implementation Version", ul);
                        }
                        else if (ut == 0x53)
                        {
                            _assoc.MaxAsyncOpsInvoked = raw.ReadUInt16("Asynchronous Operations Invoked");
                            _assoc.MaxAsyncOpsPerformed = raw.ReadUInt16("Asynchronous Operations Performed");
                        }
                        else if (ut == 0x54)
                        {
                            var asul = raw.ReadUInt16("Abstract Syntax Item-Length");
                            var syntax = raw.ReadString("Abstract Syntax UID", asul);
                            var userRole = raw.ReadByte("SCU role");
                            var providerRole = raw.ReadByte("SCP role");
                            var pc =
                                _assoc.PresentationContexts.FirstOrDefault(
                                    context => context.AbstractSyntax.UID.Equals(syntax));
                            if (pc != null)
                            {
                                pc.UserRole = userRole == 0x01;
                                pc.ProviderRole = providerRole == 0x01;
                            }
                        }
                        else if (ut == 0x56)
                        {
                            var uidLen = raw.ReadUInt16("SOP Class UID Length");
                            var uid = DicomUID.Parse(raw.ReadString("SOP Class UID", uidLen));
                            var infoLen = ul - uidLen - 2;
                            var info = raw.ReadBytes("Service Class Application Information", infoLen);
                            var appInfo = DicomServiceApplicationInfo.Create(uid, info);
                            _assoc.ExtendedNegotiations.AddOrUpdate(uid, appInfo);
                        }
                        else if (ut == 0x57)
                        {
                            var uidLen = raw.ReadUInt16("SOP Class UID Length");
                            var uid = DicomUID.Parse(raw.ReadString("SOP Class UID", uidLen));
                            var serviceUidLen = raw.ReadUInt16("Service Class UID Length");
                            var serviceUid = DicomUID.Parse(raw.ReadString("Service Class UID", serviceUidLen));
                            var relLen = raw.ReadUInt16("Related General SOP Class Identification Length");
                            var relatedUids = new List<DicomUID>();
                            var rl = relLen;
                            while (rl > 1)
                            {
                                var relUidLen = raw.ReadUInt16("Related General SOP Class UID Length");
                                relatedUids.Add(DicomUID.Parse(raw.ReadString("Related General SOP Class UID", relUidLen)));
                                rl -= (ushort)(relUidLen + 2);
                            }
                            _assoc.ExtendedNegotiations.AddOrUpdate(uid, serviceUid, relatedUids.ToArray());

                            var remaining = ul - 2 - uidLen - 2 - serviceUidLen - 2 - relLen;
                            if (remaining > 0)
                            {
                                raw.SkipBytes("User Item Value", remaining);
                            }
                        }
                        else
                        {
                            if (HandlePDUBytes != null)
                            {
                                HandlePDUBytes(raw.ReadBytes("Unhandled User Item", ul));
                            }
                            else
                            {
                                raw.SkipBytes("Unhandled User Item", ul);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }

    #endregion

    #region A-Associate-AC

    /// <summary>A-ASSOCIATE-AC</summary>
    public class AAssociateAC : PDU
    {
        private readonly DicomAssociation _assoc;

        /// <summary>
        /// Initializes new A-ASSOCIATE-AC
        /// </summary>
        /// <param name="assoc">Association parameters</param>
        public AAssociateAC(DicomAssociation assoc)
        {
            _assoc = assoc;
        }

        public override string ToString() => "A-ASSOCIATE-AC";

        #region Write

        /// <summary>
        /// Writes A-ASSOCIATE-AC to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x02);

            Write(pdu);

            return pdu;
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            await using var rawPdu = new RawPDU(0x02);

            Write(rawPdu);

            await rawPdu.WritePDUAsync(stream, cancellationToken).ConfigureAwait(false);
        }

        private void Write(RawPDU pdu)
        {
            pdu.Write("Version", (ushort)0x0001);
            pdu.Write("Reserved", 0x00, 2);
            pdu.Write("Called AE", _assoc.CalledAE, 16, ' ');
            pdu.Write("Calling AE", _assoc.CallingAE, 16, ' ');
            pdu.Write("Reserved", 0x00, 32);

            // Application Context
            pdu.Write("Item-Type", 0x10);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Application Context Name", DicomUID.DICOMApplicationContext.UID);
            pdu.WriteLength16();

            foreach (var pc in _assoc.PresentationContexts)
            {
                // Presentation Context
                pdu.Write("Item-Type", 0x21);
                pdu.Write("Reserved", 0x00);
                pdu.MarkLength16("Item-Length");
                pdu.Write("Presentation Context ID", pc.ID);
                pdu.Write("Reserved", 0x00);
                pdu.Write("Result", (byte)pc.Result);
                pdu.Write("Reserved", 0x00);

                // Transfer Syntax (set to Implicit VR Little Endian if no accepted transfer syntax is defined)
                pdu.Write("Item-Type", 0x40);
                pdu.Write("Reserved", 0x00);
                pdu.MarkLength16("Item-Length");
                pdu.Write("Transfer Syntax UID",
                    pc.AcceptedTransferSyntax?.UID.UID ?? DicomUID.ImplicitVRLittleEndian.UID);
                pdu.WriteLength16();

                pdu.WriteLength16();
            }

            // User Data Fields
            pdu.Write("Item-Type", 0x50);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");

            // Maximum PDU
            pdu.Write("Item-Type", 0x51);
            pdu.Write("Reserved", 0x00);
            pdu.Write("Item-Length", (ushort)0x0004);
            pdu.Write("Max PDU Length", _assoc.Options?.MaxPDULength ?? new DicomServiceOptions().MaxPDULength);

            // Implementation Class UID
            pdu.Write("Item-Type", 0x52);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Implementation Class UID", DicomImplementation.ClassUID.UID);
            pdu.WriteLength16();

            // Asynchronous Operations Negotiation
            if (_assoc.MaxAsyncOpsInvoked != 1 || _assoc.MaxAsyncOpsPerformed != 1)
            {
                pdu.Write("Item-Type", 0x53);
                pdu.Write("Reserved", 0x00);
                pdu.Write("Item-Length", (ushort)0x0004);
                pdu.Write("Asynchronous Operations Invoked", (ushort)_assoc.MaxAsyncOpsInvoked);
                pdu.Write("Asynchronous Operations Performed", (ushort)_assoc.MaxAsyncOpsPerformed);
            }

            // SCP-SCU Role Selection Negotiation
            foreach (var pc in _assoc.PresentationContexts)
            {
                if (pc.UserRole.HasValue || pc.ProviderRole.HasValue)
                {
                    pdu.Write("Item-Type", 0x54);
                    pdu.Write("Reserved", 0x00);
                    pdu.MarkLength16("Item-Length");
                    pdu.MarkLength16("UID-Length");
                    pdu.Write("Abstract Syntax UID", pc.AbstractSyntax.UID);
                    pdu.WriteLength16();
                    pdu.Write("SCU Role", pc.UserRole.GetValueOrDefault() ? (byte)1 : (byte)0);
                    pdu.Write("SCP Role", pc.ProviderRole.GetValueOrDefault() ? (byte)1 : (byte)0);
                    pdu.WriteLength16();
                }
            }

            // Implementation Version
            pdu.Write("Item-Type", 0x55);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Implementation Version", DicomImplementation.Version);
            pdu.WriteLength16();

            // SOP Class Extended Negotiation
            foreach (var ex in _assoc.ExtendedNegotiations.Where(e => e.AcceptedApplicationInfo?.Contains(1) == true))
            {
                pdu.Write("Item-Type", 0x56);
                pdu.Write("Reserved", 0x00);
                pdu.MarkLength16("Item-Length");
                pdu.MarkLength16("SOP Class UID-Length");
                pdu.Write("SOP Class UID", ex.SopClassUid.UID);
                pdu.WriteLength16();
                pdu.Write("Service Class Application Information", ex.AcceptedApplicationInfo.GetValues());
                pdu.WriteLength16();
            }

            pdu.WriteLength16();
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads A-ASSOCIATE-AC from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        public void Read(RawPDU raw)
        {
            // reset async ops in case remote end does not negotiate
            _assoc.MaxAsyncOpsInvoked = 1;
            _assoc.MaxAsyncOpsPerformed = 1;

            var l = raw.Length - 6;

            raw.ReadUInt16("Version");
            raw.SkipBytes("Reserved", 2);
            raw.SkipBytes("Reserved", 16);
            raw.SkipBytes("Reserved", 16);
            raw.SkipBytes("Reserved", 32);
            l -= 68;

            while (l > 0)
            {
                var type = raw.ReadByte("Item-Type");
                l -= 1;

                if (type == 0x10)
                {
                    // Application Context
                    raw.SkipBytes("Reserved", 1);
                    var c = raw.ReadUInt16("Item-Length");
                    raw.SkipBytes("Value", c);
                    l -= 3 + (uint)c;
                }
                else if (type == 0x21)
                {
                    // Presentation Context
                    raw.ReadByte("Reserved");
                    var pl = raw.ReadUInt16("Presentation Context Item-Length");
                    var id = raw.ReadByte("Presentation Context ID");
                    raw.ReadByte("Reserved");
                    var res = raw.ReadByte("Presentation Context Result/Reason");
                    raw.ReadByte("Reserved");
                    l -= (uint)pl + 3;
                    pl -= 4;

                    if ((DicomPresentationContextResult)res == DicomPresentationContextResult.Accept)
                    {
                        // Presentation Context Transfer Syntax
                        raw.ReadByte("Presentation Context Item-Type (0x40)");
                        raw.ReadByte("Reserved");
                        var tl = raw.ReadUInt16("Presentation Context Item-Length");
                        var tx = raw.ReadString("Presentation Context Syntax UID", tl);
                        pl -= (ushort)(tl + 4);

                        _assoc.PresentationContexts[id].SetResult(
                            (DicomPresentationContextResult)res,
                            DicomTransferSyntax.Parse(tx));
                    }
                    else
                    {
                        raw.SkipBytes("Rejected Presentation Context", pl);
                        _assoc.PresentationContexts[id].SetResult((DicomPresentationContextResult)res);
                    }
                }
                else if (type == 0x50)
                {
                    // User Information
                    raw.ReadByte("Reserved");
                    var il = raw.ReadUInt16("User Information Item-Length");
                    l -= (uint)(il + 3);
                    while (il > 0)
                    {
                        var ut = raw.ReadByte("User Item-Type");
                        raw.ReadByte("Reserved");
                        var ul = raw.ReadUInt16("User Item-Length");
                        il -= (ushort)(ul + 4);
                        if (ut == 0x51)
                        {
                            _assoc.MaximumPDULength = raw.ReadUInt32("Max PDU Length");
                        }
                        else if (ut == 0x52)
                        {
                            _assoc.RemoteImplementationClassUID =
                                DicomUID.Parse(raw.ReadString("Implementation Class UID", ul));
                        }
                        else if (ut == 0x53)
                        {
                            _assoc.MaxAsyncOpsInvoked = raw.ReadUInt16("Asynchronous Operations Invoked");
                            _assoc.MaxAsyncOpsPerformed = raw.ReadUInt16("Asynchronous Operations Performed");
                        }
                        else if (ut == 0x54)
                        {
                            var asul = raw.ReadUInt16("Abstract Syntax Item-Length");
                            var syntax = raw.ReadString("Abstract Syntax UID", asul);
                            var userRole = raw.ReadByte("SCU role");
                            var providerRole = raw.ReadByte("SCP role");
                            var pc =
                                _assoc.PresentationContexts.FirstOrDefault(
                                    context => context.AbstractSyntax.UID.Equals(syntax));
                            if (pc != null)
                            {
                                pc.UserRole = userRole == 0x01;
                                pc.ProviderRole = providerRole == 0x01;
                            }
                        }
                        else if (ut == 0x55)
                        {
                            _assoc.RemoteImplementationVersion = raw.ReadString("Implementation Version", ul);
                        }
                        else if (ut == 0x56)
                        {
                            var uidLen = raw.ReadUInt16("SOP Class UID Length");
                            var uid = DicomUID.Parse(raw.ReadString("SOP Class UID", uidLen));
                            var infoLen = ul - 2 - uidLen;
                            var info = raw.ReadBytes("Service Class Application Information", infoLen);
                            var appInfo = DicomServiceApplicationInfo.Create(uid, info);
                            _assoc.ExtendedNegotiations.AcceptApplicationInfo(uid, appInfo);
                        }
                        else
                        {
                            raw.SkipBytes("User Item Value", ul);
                        }
                    }
                }
                else
                {
                    raw.SkipBytes("Reserved", 1);
                    var il = raw.ReadUInt16("User Item-Length");
                    raw.SkipBytes("Unknown User Item", il);
                    l -= (uint)(il + 3);
                }
            }
        }

        #endregion
    }

    #endregion

    #region A-Associate-RJ

    /// <summary>Rejection result</summary>
    public enum DicomRejectResult
    {
        /// <summary>Permanent rejection</summary>
        Permanent = 1,

        /// <summary>Transient rejection</summary>
        Transient = 2
    }

    /// <summary>Rejection source</summary>
    public enum DicomRejectSource
    {
        /// <summary>Service user</summary>
        ServiceUser = 1,

        /// <summary>Service provider - ACSE</summary>
        ServiceProviderACSE = 2,

        /// <summary>Service provider - Presentation</summary>
        ServiceProviderPresentation = 3
    }

    /// <summary>Rejection reason</summary>
    /// <remarks>The underlying value is a hexadecimal combination of the <see cref="DicomRejectSource"/> and the rejection
    /// reason code given in Table 9-21 of DICOM Standard PS 3.8.</remarks>
    public enum DicomRejectReason
    {
        /// <summary>No reason given (Service user)</summary>
        NoReasonGiven = 0x11,

        /// <summary>Application context not supported (Service user)</summary>
        ApplicationContextNotSupported = 0x12,

        /// <summary>Calling AE not recognized (Service user)</summary>
        CallingAENotRecognized = 0x13,

        /// <summary>Called AE not recognized (Service user)</summary>
        CalledAENotRecognized = 0x17,

        /// <summary>No reason given (Service provider - ACSE)</summary>
        NoReasonGiven_ = 0x21,

        /// <summary>Protocol version not supported (Service provider - ACSE)</summary>
        ProtocolVersionNotSupported = 0x22,

        /// <summary>Temporary congestion (Service provider - Presentation)</summary>
        TemporaryCongestion = 0x31,

        /// <summary>Local limit exceeded (Service provider - Presentation)</summary>
        LocalLimitExceeded = 0x32
    }

    /// <summary>A-ASSOCIATE-RJ</summary>
    public class AAssociateRJ : PDU
    {

        /// <summary>
        /// Initializes new A-ASSOCIATE-RJ
        /// </summary>
        public AAssociateRJ()
        {
        }

        /// <summary>
        /// Initializes new A-ASSOCIATE-RJ
        /// </summary>
        /// <param name="rt">Rejection result</param>
        /// <param name="so">Rejection source</param>
        /// <param name="rn">Rejection reason</param>
        public AAssociateRJ(DicomRejectResult rt, DicomRejectSource so, DicomRejectReason rn)
        {
            Result = rt;
            Source = so;
            Reason = rn;
        }

        /// <summary>Rejection result</summary>
        public DicomRejectResult Result { get; private set; } = DicomRejectResult.Permanent;

        /// <summary>Rejection source</summary>
        public DicomRejectSource Source { get; private set; } = DicomRejectSource.ServiceUser;

        /// <summary>Rejection reason</summary>
        public DicomRejectReason Reason { get; private set; } = DicomRejectReason.NoReasonGiven;

        public override string ToString() => "A-ASSOCIATE-RJ";

        /// <summary>
        /// Writes A-ASSOCIATE-RJ to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        /// <remarks>When writing the rejection reason to the <see cref="RawPDU"/> object, the <see cref="DicomRejectSource"/>
        /// specification in the underlying value is masked out, to ensure that the reason code matches the codes specified
        /// in Table 9-21 of DICOM Standard PS 3.8.</remarks>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x03);
            
            Write(pdu);
            
            return pdu;
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            await using var rawPdu = new RawPDU(0x03);

            Write(rawPdu);

            await rawPdu.WritePDUAsync(stream, cancellationToken).ConfigureAwait(false);
        }

        private void Write(RawPDU pdu)
        {
            pdu.Write("Reserved", 0x00);
            pdu.Write("Result", (byte)Result);
            pdu.Write("Source", (byte)Source);
            pdu.Write("Reason", (byte)((byte)Reason & 0xf));
        }

        /// <summary>
        /// Reads A-ASSOCIATE-RJ from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        /// <remarks>When reading the rejection reason, the previously read <see cref="DicomRejectSource"/> value is
        /// combined with the numerical reject reason from the <see cref="RawPDU"/> object to yield numerical
        /// values that matches the members of the <see cref="DicomRejectReason"/> enumeration.</remarks>
        public void Read(RawPDU raw)
        {
            raw.ReadByte("Reserved");
            Result = (DicomRejectResult)raw.ReadByte("Result");
            Source = (DicomRejectSource)raw.ReadByte("Source");
            Reason = (DicomRejectReason)((byte)Source << 4 | raw.ReadByte("Reason"));
        }
    }

    #endregion

    #region A-Release-RQ

    /// <summary>A-RELEASE-RQ</summary>
    public class AReleaseRQ : PDU
    {
        public override string ToString()
        {
            return "A-RELEASE-RQ";
        }

        /// <summary>
        /// Writes A-RELEASE-RQ to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x05);
            
            Write(pdu);
            
            return pdu;
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            await using var rawPdu = new RawPDU(0x05);

            Write(rawPdu);

            await rawPdu.WritePDUAsync(stream, cancellationToken).ConfigureAwait(false);
        }

        private void Write(RawPDU pdu)
        {
            pdu.Write("Reserved", (uint)0x00000000);
        }

        /// <summary>
        /// Reads A-RELEASE-RQ from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        public void Read(RawPDU raw)
        {
            raw.ReadUInt32("Reserved");
        }
    }

    #endregion

    #region A-Release-RP

    /// <summary>A-RELEASE-RP</summary>
    public class AReleaseRP : PDU
    {
        public override string ToString()
        {
            return "A-RELEASE-RP";
        }

        /// <summary>
        /// Writes A-RELEASE-RP to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x06);

            Write(pdu);
            
            return pdu;
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            await using var rawPdu = new RawPDU(0x06);

            Write(rawPdu);

            await rawPdu.WritePDUAsync(stream, cancellationToken).ConfigureAwait(false);
        }

        private void Write(RawPDU pdu)
        {
            pdu.Write("Reserved", (uint)0x00000000);
        }

        /// <summary>
        /// Reads A-RELEASE-RP from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        public void Read(RawPDU raw)
        {
            raw.ReadUInt32("Reserved");
        }
    }

    #endregion

    #region A-Abort

    /// <summary>Abort source</summary>
    public enum DicomAbortSource
    {
        /// <summary>Service user</summary>
        ServiceUser = 0,

        /// <summary>Unknown</summary>
        Unknown = 1,

        /// <summary>Service provider</summary>
        ServiceProvider = 2
    }

    /// <summary>Abort reason</summary>
    public enum DicomAbortReason
    {
        /// <summary>Not specified</summary>
        NotSpecified = 0,

        /// <summary>Unrecognized PDU type</summary>
        UnrecognizedPDU = 1,

        /// <summary>Unexpected PDU</summary>
        UnexpectedPDU = 2,

        /// <summary>Unrecognized PDU parameter</summary>
        UnrecognizedPDUParameter = 4,

        /// <summary>Unexpected PDU parameter</summary>
        UnexpectedPDUParameter = 5,

        /// <summary>Invalid PDU parameter</summary>
        InvalidPDUParameter = 6
    }

    /// <summary>A-ABORT</summary>
    public class AAbort : PDU
    {

        /// <summary>Abort source</summary>
        public DicomAbortSource Source { get; private set; }

        /// <summary>Abort reason</summary>
        public DicomAbortReason Reason { get; private set; }

        /// <summary>
        /// Initializes new A-ABORT
        /// </summary>
        public AAbort()
        {
            Source = DicomAbortSource.ServiceUser;
            Reason = DicomAbortReason.NotSpecified;
        }

        /// <summary>
        /// Initializes new A-ABORT
        /// </summary>
        /// <param name="source">Abort source</param>
        /// <param name="reason">Abort reason</param>
        public AAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            Source = source;
            Reason = reason;
        }

        public override string ToString() => "A-ABORT";

        #region Write

        /// <summary>
        /// Writes A-ABORT to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x07);
            
            Write(pdu);
            
            return pdu;
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            await using var rawPdu = new RawPDU(0x07);

            Write(rawPdu);

            await rawPdu.WritePDUAsync(stream, cancellationToken).ConfigureAwait(false);
        }
        
        private void Write(RawPDU pdu)
        {
            pdu.Write("Reserved", 0x00);
            pdu.Write("Reserved", 0x00);
            pdu.Write("Source", (byte)Source);
            pdu.Write("Reason", (byte)Reason);
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads A-ABORT from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        public void Read(RawPDU raw)
        {
            raw.ReadByte("Reserved");
            raw.ReadByte("Reserved");
            Source = (DicomAbortSource)raw.ReadByte("Source");
            Reason = (DicomAbortReason)raw.ReadByte("Reason");
        }

        #endregion
    }

    #endregion

    #region P-Data-TF

    /// <summary>P-DATA-TF</summary>
    public class PDataTF : PDU
    {

        /// <summary>
        /// Initializes new P-DATA-TF
        /// </summary>
        public PDataTF()
        {
        }

        /// <summary>PDVs in this P-DATA-TF</summary>
        public List<PDV> PDVs { get; } = new List<PDV>();

        /// <summary>Calculates the total length of the PDVs in this P-DATA-TF</summary>
        /// <returns>Length of PDVs</returns>
        public uint GetLengthOfPDVs()
        {
            return (uint)PDVs.Sum(pdv => pdv.PDVLength);
        }

        public override string ToString()
        {
            var value = $"P-DATA-TF [Length: {6 + GetLengthOfPDVs()}]";
            foreach (var pdv in PDVs)
            {
                value += "\n\t" + pdv.ToString();
            }

            return value;
        }

        #region Write

        /// <summary>
        /// Writes P-DATA-TF to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x04);

            Write(pdu);
            
            return pdu;
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            await using var pdu = new RawPDU(0x04, DicomEncoding.Default, stream, true);

            // For P-DATA-TF, we manually compose the preamble because we cannot use the length of the memory stream (because there is no memory stream) 
            byte[] preamble = ArrayPool<byte>.Shared.Rent(6);
            var length = GetLengthOfPDVs();
            try
            {
                unchecked
                {
                    preamble[0] = 0x04;
                    preamble[1] = 0;
                    preamble[2] = (byte)((length & 0xff000000U) >> 24);
                    preamble[3] = (byte)((length & 0x00ff0000U) >> 16);
                    preamble[4] = (byte)((length & 0x0000ff00U) >> 8);
                    preamble[5] = (byte)(length & 0x000000ffU);
                }

                await stream.WriteAsync(preamble, 0, 6, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(preamble);
            }
            
            
            Write(pdu);
        }

        private void Write(RawPDU pdu)
        {
            foreach (var pdv in PDVs)
            {
                pdv.Write(pdu);
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads P-DATA-TF from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        public void Read(RawPDU raw)
        {
            var len = raw.Length - 6;
            uint read = 0;
            while (read < len)
            {
                var pdv = new PDV();
                read += pdv.Read(raw);
                PDVs.Add(pdv);
            }
        }

        #endregion
    }

    #endregion

    #region PDV

    /// <summary>PDV</summary>
    public class PDV
    {

        /// <summary>
        /// Initializes new PDV
        /// </summary>
        /// <param name="pcid">Presentation context ID</param>
        /// <param name="value">PDV data</param>
        /// <param name="command">Is command</param>
        /// <param name="last">Is last fragment of command or data</param>
        public PDV(byte pcid, byte[] value, bool command, bool last)
        {
            if (value.Length % 2 == 1)
            {
                Array.Resize(ref value, value.Length + 1);
            }

            PCID = pcid;
            Value = value;
            IsCommand = command;
            IsLastFragment = last;
        }

        /// <summary>
        /// Initializes new PDV
        /// </summary>
        public PDV()
        {
        }

        /// <summary>Presentation context ID</summary>
        public byte PCID { get; set; }

        /// <summary>PDV data</summary>
        public byte[] Value { get; set; } = new byte[0];

        /// <summary>PDV is command</summary>
        public bool IsCommand { get; set; } = false;

        /// <summary>PDV is last fragment of command or data</summary>
        public bool IsLastFragment { get; set; } = false;

        /// <summary>Length of this PDV</summary>
        public uint PDVLength => (uint)Value.Length + 6;

        public override string ToString() => $"PDV [PCID: {PCID}; Length: {Value.Length}; Command: {IsCommand}; Last: {IsLastFragment}]";

        #region Write

        /// <summary>
        /// Writes PDV to PDU buffer
        /// </summary>
        /// <param name="pdu">PDU buffer</param>
        public void Write(RawPDU pdu)
        {
            var mch = (byte)((IsLastFragment ? 2 : 0) + (IsCommand ? 1 : 0));
            pdu.Write("PDV-Length", 2 + (uint) Value.Length);
            pdu.Write("Presentation Context ID", PCID);
            pdu.Write("Message Control Header", mch);
            pdu.Write("PDV Value", Value);
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads PDV from PDU buffer
        /// </summary>
        /// <param name="raw">PDU buffer</param>
        public uint Read(RawPDU raw)
        {
            var len = raw.ReadUInt32("PDV-Length");
            PCID = raw.ReadByte("Presentation Context ID");
            var mch = raw.ReadByte("Message Control Header");
            Value = raw.ReadBytes("PDV Value", (int)len - 2);
            IsCommand = (mch & 0x01) != 0;
            IsLastFragment = (mch & 0x02) != 0;
            return len + 4;
        }

        #endregion
    }

    #endregion
}
