// Copyright (c) 2012-2015 fo-dicom contributors.
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

        /// <summary>
        /// Implementation of the machine name getter.
        /// </summary>
        protected override string MachineNameImpl
        {
            get
            {
                return Environment.MachineName;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Platform-specific implementation to create a network listener object.
        /// </summary>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        protected override INetworkListener CreateNetworkListenerImpl(int port)
        {
            return new DesktopNetworkListener(port);
        }

        /// <summary>
        /// Platform-specific implementation to create a network stream object.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        /// <returns>Network stream implementation.</returns>
        protected override INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            return new DesktopNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);
        }

        /// <summary>
        /// Platform-specific implementation to check whether specified <paramref name="exception"/> represents a socket exception.
        /// </summary>
        /// <param name="exception">Exception to validate.</param>
        /// <param name="errorCode">Error code, valid if <paramref name="exception"/> is socket exception.</param>
        /// <param name="errorDescriptor">Error descriptor, valid if <paramref name="exception"/> is socket exception.</param>
        /// <returns>True if <paramref name="exception"/> is socket exception, false otherwise.</returns>
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

        /// <summary>
        /// Platform-specific implementation to attempt to obtain a unique network identifier, e.g. based on a MAC address.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
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
