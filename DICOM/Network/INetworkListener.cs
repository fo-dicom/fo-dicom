// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public interface INetworkListener
    {
        void Start();

        void Stop();

        INetworkStream AcceptNetworkStream(int port, string certificateName, bool noDelay);
    }
}
