// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Globalization;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    /// <summary>
    /// .NET implementation of <see cref="NetworkManager"/>.
    /// </summary>
    public class DesktopNetworkManager : NetworkManager
    {
        #region FIELDS

        /// <summary>
        /// Singleton instance of <see cref="DesktopNetworkManager"/>.
        /// </summary>
        public static readonly NetworkManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="DesktopNetworkManager"/>.
        /// </summary>
        static DesktopNetworkManager()
        {
            Instance = new DesktopNetworkManager();
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        protected override string MachineNameImpl
        {
            get
            {
#if NETSTANDARD
                return Environment.GetEnvironmentVariable("COMPUTERNAME");
#else
                return Environment.MachineName;
#endif
            }
        }

        #endregion

        #region METHODS

        /// <inheritdoc />
        protected override INetworkListener CreateNetworkListenerImpl(string ipAddress, int port)
        {
            return new DesktopNetworkListener(ipAddress, port);
        }

        /// <inheritdoc />
        protected override INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            return new DesktopNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);
        }

        /// <inheritdoc />
        protected override bool IsSocketExceptionImpl(Exception exception, out int errorCode, out string errorDescriptor)
        {
            var socketEx = exception as SocketException;
            if (socketEx != null)
            {
#if NETSTANDARD
                errorCode = (int)socketEx.SocketErrorCode;
#else
                errorCode = socketEx.ErrorCode;
#endif
                errorDescriptor = socketEx.SocketErrorCode.ToString();
                return true;
            }

            errorCode = -1;
            errorDescriptor = null;
            return false;
        }

        /// <inheritdoc />
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
