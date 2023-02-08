// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;

namespace FellowOakDicom.Network.Tls
{
    public interface ITlsInitiator
    {
        Stream InitiateTls(Stream plainStream, string remoteAddress, int remotePort);
    }
}
