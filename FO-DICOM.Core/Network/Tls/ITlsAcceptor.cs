// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;

namespace FellowOakDicom.Network.Tls
{
    public interface ITlsAcceptor
    {
        Stream AcceptTls(Stream encryptedStream, string remoteAddress, int localPort);
    }
}
