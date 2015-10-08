// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Globalization;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    public class DesktopNetworkManager : NetworkManager
    {
        #region FIELDS

        public static readonly NetworkManager Instance;

        #endregion

        #region CONSTRUCTORS

        static DesktopNetworkManager()
        {
            Instance = new DesktopNetworkManager();
        }

        private DesktopNetworkManager()
        {
        }

        #endregion

        #region METHODS

        protected override INetworkListener CreateNetworkListenerImpl(int port)
        {
            return new DesktopNetworkListener(port);
        }

        protected override INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            return new DesktopNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);
        }

        protected override bool IsSocketExceptionImpl(Exception exception, out int errorCode, out string errorDescriptor)
        {
            var socketEx = exception as SocketException;
            if (socketEx != null)
            {
                errorCode = socketEx.ErrorCode;
                errorDescriptor = socketEx.SocketErrorCode.ToString();
                return true;
            }

            errorCode = -1;
            errorDescriptor = null;
            return false;
        }

        protected override bool TryGetNetworkIdentifierImpl(out DicomUID identifier)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            for (var i = 0; i < interfaces.Length; i++)
            {
                if (NetworkInterface.LoopbackInterfaceIndex == i
                    || interfaces[i].OperationalStatus != OperationalStatus.Up) continue;

                var hex = interfaces[i].GetPhysicalAddress().ToString();
                if (string.IsNullOrEmpty(hex)) continue;

                try
                {
                    var mac = long.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    {
                        identifier = DicomUID.Append(DicomImplementation.ClassUID, mac);
                        return true;
                    }
                }
                catch
                {
                }
            }

            identifier = null;
            return false;
        }

        #endregion
    }
}
