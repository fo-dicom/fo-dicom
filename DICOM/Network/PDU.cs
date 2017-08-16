﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Dicom.IO;

namespace Dicom.Network
{
    #region Raw PDU

    /// <summary>Encapsulates PDU data for reading or writing</summary>
    public class RawPDU : IDisposable
    {
        #region Private members

        private readonly byte _type;

        private MemoryStream _ms;

        private BinaryReader _br;

        private readonly BinaryWriter _bw;

        private readonly Stack<long> _m16;

        private readonly Stack<long> _m32;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes new PDU for writing
        /// </summary>
        /// <param name="type">Type of PDU</param>
        public RawPDU(byte type)
        {
            _type = type;
            _ms = new MemoryStream();
            _ms.Seek(0, SeekOrigin.Begin);
            _bw = EndianBinaryWriter.Create(_ms, DicomEncoding.Default, Endian.Big);
            _m16 = new Stack<long>();
            _m32 = new Stack<long>();
        }

        /// <summary>
        /// Initializes new PDU reader from buffer
        /// </summary>
        /// <param name="buffer">Buffer</param>
        public RawPDU(byte[] buffer)
        {
            _ms = new MemoryStream(buffer);
            _br = EndianBinaryReader.Create(_ms, Endian.Big);
            _type = _br.ReadByte();
            _ms.Seek(6, SeekOrigin.Begin);
        }

        #endregion

        #region Public Properties

        /// <summary>PDU type</summary>
        public byte Type => _type;

        /// <summary>PDU length</summary>
        public uint Length => (uint)_ms.Length;

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes PDU to stream
        /// </summary>
        /// <param name="s">Output stream</param>
        public void WritePDU(Stream s)
        {
            var buffer = new byte[6];

            unchecked
            {
                buffer[0] = _type;

                var length = (uint)_ms.Length;
                buffer[2] = (byte)((length & 0xff000000U) >> 24);
                buffer[3] = (byte)((length & 0x00ff0000U) >> 16);
                buffer[4] = (byte)((length & 0x0000ff00U) >> 8);
                buffer[5] = (byte)(length & 0x000000ffU);
            }

            s.Write(buffer, 0, 6);
            _ms.WriteTo(s);
            s.Flush();
        }

        /// <summary>
        /// Saves PDU to file
        /// </summary>
        /// <param name="file">Filename</param>
        public void Save(string file)
        {
            var f = IOManager.CreateFileReference(file);
            var d = f.Directory;

            if (!d.Exists)
            {
                d.Create();
            }
            using (var fs = f.OpenWrite())
            {
                WritePDU(fs);
            }
        }

        /// <summary>
        /// Gets string describing this PDU
        /// </summary>
        /// <returns>PDU description</returns>
        public override string ToString()
        {
            return $"Pdu[type={Type:X2}, length={Length}]";
        }

        /// <summary>
        /// Reset PDU read stream
        /// </summary>
        public void Reset()
        {
            _ms.Seek(0, SeekOrigin.Begin);
        }

        #region Read Methods

        private void CheckOffset(int bytes, string name)
        {
            if (_ms.Position + bytes > _ms.Length)
            {
                var msg =
                    $"{ToString()} (offset={_ms.Position}, bytes={bytes}, field=\"{name}\") Requested offset out of range!";
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
        /// <param name="count">Length of string</param>
        /// <returns>Field value</returns>
        public string ReadString(string name, int count)
        {
            CheckOffset(count, name);
            var c = _br.ReadChars(count);
            return new string(c).Trim(_trimChars);
        }

        /// <summary>
        /// Skips ahead in PDU
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <param name="count">Number of bytes to skip</param>
        public void SkipBytes(string name, int count)
        {
            CheckOffset(count, name);
            _ms.Seek(count, SeekOrigin.Current);
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
            for (var i = 0; i < count; i++) _bw.Write(value);
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
            _m16.Push(_ms.Position);
            _bw.Write((ushort)0);
        }

        /// <summary>
        /// Writes 16-bit length to top length marker
        /// </summary>
        public void WriteLength16()
        {
            var p1 = _m16.Pop();
            var p2 = _ms.Position;
            _ms.Position = p1;
            _bw.Write((ushort)(p2 - p1 - 2));
            _ms.Position = p2;
        }

        /// <summary>
        /// Marks position to write 32-bit length value
        /// </summary>
        /// <param name="name">Field name</param>
        public void MarkLength32(string name)
        {
            _m32.Push(_ms.Position);
            _bw.Write((uint)0);
        }

        /// <summary>
        /// Writes 32-bit length to top length marker
        /// </summary>
        public void WriteLength32()
        {
            var p1 = _m32.Pop();
            var p2 = _ms.Position;
            _ms.Position = p1;
            _bw.Write((uint)(p2 - p1 - 4));
            _ms.Position = p2;
        }

        private static char[] ToCharArray(string s, int l, char p)
        {
            var c = new char[l];
            for (var i = 0; i < l; i++)
            {
                if (i < s.Length) c[i] = s[i];
                else c[i] = p;
            }
            return c;
        }

        #endregion

        #endregion

        public void Dispose()
        {
            _ms = null;
            _br = null;
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

        public override string ToString()
        {
            return "A-ASSOCIATE-RQ";
        }

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

            pdu.Write("Version", (ushort)0x0001);
            pdu.Write("Reserved", 0x00, 2);
            pdu.Write("Called AE", _assoc.CalledAE, 16, ' ');
            pdu.Write("Calling AE", _assoc.CallingAE, 16, ' ');
            pdu.Write("Reserved", 0x00, 32);

            // Application Context
            pdu.Write("Item-Type", 0x10);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Application Context Name", DicomUID.DICOMApplicationContextName.UID);
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
            pdu.Write("Max PDU Length", _assoc.MaximumPDULength);

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

            foreach (var exNeg in _assoc.ExtendedNegotiations)
            {
                exNeg.Write(pdu);
            }

            pdu.WriteLength16();

            return pdu;
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
                            _assoc.ExtendedNegotiations.Add(DicomExtendedNegotiation.Create(raw, ul));
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
        private DicomAssociation _assoc;

        /// <summary>
        /// Initializes new A-ASSOCIATE-AC
        /// </summary>
        /// <param name="assoc">Association parameters</param>
        public AAssociateAC(DicomAssociation assoc)
        {
            _assoc = assoc;
        }

        public override string ToString()
        {
            return "A-ASSOCIATE-AC";
        }

        #region Write

        /// <summary>
        /// Writes A-ASSOCIATE-AC to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x02);

            pdu.Write("Version", (ushort)0x0001);
            pdu.Write("Reserved", 0x00, 2);
            pdu.Write("Called AE", _assoc.CalledAE, 16, ' ');
            pdu.Write("Calling AE", _assoc.CallingAE, 16, ' ');
            pdu.Write("Reserved", 0x00, 32);

            // Application Context
            pdu.Write("Item-Type", 0x10);
            pdu.Write("Reserved", 0x00);
            pdu.MarkLength16("Item-Length");
            pdu.Write("Application Context Name", DicomUID.DICOMApplicationContextName.UID);
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
            pdu.Write("Max PDU Length", _assoc.MaximumPDULength);

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

            foreach (var exNeg in _assoc.ExtendedNegotiations)
            {
                exNeg.Write(pdu);
            }

            pdu.WriteLength16();

            return pdu;
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
                            _assoc.ExtendedNegotiations.Add(DicomExtendedNegotiation.Create(raw, ul));
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
        private DicomRejectResult _rt = DicomRejectResult.Permanent;

        private DicomRejectSource _so = DicomRejectSource.ServiceUser;

        private DicomRejectReason _rn = DicomRejectReason.NoReasonGiven;

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
            _rt = rt;
            _so = so;
            _rn = rn;
        }

        /// <summary>Rejection result</summary>
        public DicomRejectResult Result
        {
            get
            {
                return _rt;
            }
        }

        /// <summary>Rejection source</summary>
        public DicomRejectSource Source
        {
            get
            {
                return _so;
            }
        }

        /// <summary>Rejection reason</summary>
        public DicomRejectReason Reason
        {
            get
            {
                return _rn;
            }
        }

        public override string ToString()
        {
            return "A-ASSOCIATE-RJ";
        }

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
            pdu.Write("Reserved", 0x00);
            pdu.Write("Result", (byte)_rt);
            pdu.Write("Source", (byte)_so);
            pdu.Write("Reason", (byte)((byte)_rn & 0xf));
            return pdu;
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
            _rt = (DicomRejectResult)raw.ReadByte("Result");
            _so = (DicomRejectSource)raw.ReadByte("Source");
            _rn = (DicomRejectReason)((byte)_so << 4 | raw.ReadByte("Reason"));
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
            pdu.Write("Reserved", (uint)0x00000000);
            return pdu;
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
            pdu.Write("Reserved", (uint)0x00000000);
            return pdu;
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
        /// <summary>Unknown</summary>
        Unknown = 0,

        /// <summary>Service user</summary>
        ServiceUser = 1,

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
        private DicomAbortSource _s;

        private DicomAbortReason _r;

        /// <summary>Abort source</summary>
        public DicomAbortSource Source
        {
            get
            {
                return _s;
            }
        }

        /// <summary>Abort reason</summary>
        public DicomAbortReason Reason
        {
            get
            {
                return _r;
            }
        }

        /// <summary>
        /// Initializes new A-ABORT
        /// </summary>
        public AAbort()
        {
            _s = DicomAbortSource.ServiceUser;
            _r = DicomAbortReason.NotSpecified;
        }

        /// <summary>
        /// Initializes new A-ABORT
        /// </summary>
        /// <param name="source">Abort source</param>
        /// <param name="reason">Abort reason</param>
        public AAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            _s = source;
            _r = reason;
        }

        public override string ToString()
        {
            return "A-ABORT";
        }

        #region Write

        /// <summary>
        /// Writes A-ABORT to PDU buffer
        /// </summary>
        /// <returns>PDU buffer</returns>
        public RawPDU Write()
        {
            var pdu = new RawPDU(0x07);
            pdu.Write("Reserved", 0x00);
            pdu.Write("Reserved", 0x00);
            pdu.Write("Source", (byte)_s);
            pdu.Write("Reason", (byte)_r);
            return pdu;
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
            _s = (DicomAbortSource)raw.ReadByte("Source");
            _r = (DicomAbortReason)raw.ReadByte("Reason");
        }

        #endregion
    }

    #endregion

    #region P-Data-TF

    /// <summary>P-DATA-TF</summary>
    public class PDataTF : PDU
    {
        private List<PDV> _pdvs = new List<PDV>();

        /// <summary>
        /// Initializes new P-DATA-TF
        /// </summary>
        public PDataTF()
        {
        }

        /// <summary>PDVs in this P-DATA-TF</summary>
        public List<PDV> PDVs
        {
            get
            {
                return _pdvs;
            }
        }

        /// <summary>Calculates the total length of the PDVs in this P-DATA-TF</summary>
        /// <returns>Length of PDVs</returns>
        public uint GetLengthOfPDVs()
        {
            uint len = 0;
            foreach (var pdv in _pdvs)
            {
                len += pdv.PDVLength;
            }
            return len;
        }

        public override string ToString()
        {
            var value = $"P-DATA-TF [Length: {6 + GetLengthOfPDVs()}]";
            foreach (var pdv in PDVs) value += "\n\t" + pdv.ToString();
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
            foreach (var pdv in _pdvs)
            {
                pdv.Write(pdu);
            }
            return pdu;
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
                _pdvs.Add(pdv);
            }
        }

        #endregion
    }

    #endregion

    #region PDV

    /// <summary>PDV</summary>
    public class PDV
    {
        private byte _pcid;

        private byte[] _value = new byte[0];

        private bool _command = false;

        private bool _last = false;

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

            _pcid = pcid;
            _value = value;
            _command = command;
            _last = last;
        }

        /// <summary>
        /// Initializes new PDV
        /// </summary>
        public PDV()
        {
        }

        /// <summary>Presentation context ID</summary>
        public byte PCID
        {
            get
            {
                return _pcid;
            }
            set
            {
                _pcid = value;
            }
        }

        /// <summary>PDV data</summary>
        public byte[] Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>PDV is command</summary>
        public bool IsCommand
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
            }
        }

        /// <summary>PDV is last fragment of command or data</summary>
        public bool IsLastFragment
        {
            get
            {
                return _last;
            }
            set
            {
                _last = value;
            }
        }

        /// <summary>Length of this PDV</summary>
        public uint PDVLength
        {
            get
            {
                return (uint)_value.Length + 6;
            }
        }

        public override string ToString()
        {
            return $"PDV [PCID: {PCID}; Length: {Value.Length}; Command: {IsCommand}; Last: {IsLastFragment}]";
        }

        #region Write

        /// <summary>
        /// Writes PDV to PDU buffer
        /// </summary>
        /// <param name="pdu">PDU buffer</param>
        public void Write(RawPDU pdu)
        {
            var mch = (byte)((_last ? 2 : 0) + (_command ? 1 : 0));
            pdu.MarkLength32("PDV-Length");
            pdu.Write("Presentation Context ID", _pcid);
            pdu.Write("Message Control Header", mch);
            pdu.Write("PDV Value", _value);
            pdu.WriteLength32();
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
            _pcid = raw.ReadByte("Presentation Context ID");
            var mch = raw.ReadByte("Message Control Header");
            _value = raw.ReadBytes("PDV Value", (int)len - 2);
            _command = (mch & 0x01) != 0;
            _last = (mch & 0x02) != 0;
            return len + 4;
        }

        #endregion
    }

    #endregion
}
