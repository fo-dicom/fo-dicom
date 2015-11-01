// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Globalization;
    using System.Net.Sockets;

    using Windows.Networking;
    using Windows.Networking.Connectivity;

    /// <summary>
    /// Universal Windows Platform implementation of <see cref="NetworkManager"/>.
    /// </summary>
    public class WindowsNetworkManager : NetworkManager
    {
        #region FIELDS

        /// <summary>
        /// Singleton instance of <see cref="WindowsNetworkManager"/>.
        /// </summary>
        public static readonly NetworkManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="WindowsNetworkManager"/>.
        /// </summary>
        static WindowsNetworkManager()
        {
            Instance = new WindowsNetworkManager();
        }

        /// <summary>
        /// Initializes an instance of <see cref="WindowsNetworkManager"/>.
        /// </summary>
        private WindowsNetworkManager()
        {
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
                // Want to store the hostname to send for push notifications to make
                // the management UI better. Take the substring up to the first period
                // of the first DomainName entry.
                // Thanks to Jeff Wilcox and Matthijs Hoekstra
                // Adapted from Q42.WinRT library at https://github.com/Q42/Q42.WinRT
                var list = NetworkInformation.GetHostNames();
                string name = null;
                if (list.Count > 0)
                {
                    foreach (var entry in list)
                    {
                        if (entry.Type == HostNameType.DomainName)
                        {
                            var s = entry.CanonicalName;
                            if (!string.IsNullOrEmpty(s))
                            {
                                // Domain-joined. Requires at least a one-character name.
                                var j = s.IndexOf('.');

                                if (j > 0)
                                {
                                    name = s.Substring(0, j);
                                    break;
                                }

                                // Typical home machine.
                                name = s;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(name))
                {
                    name = "Unknown";
                }

                return name;
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
            return new WindowsNetworkListener(port);
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
            return new WindowsNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);
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
                errorCode = socketEx.HResult;
                errorDescriptor = socketEx.SocketErrorCode.ToString();
                return true;
            }

            errorCode = -1;
            errorDescriptor = null;
            return false;
        }

        /// <summary>
        /// Platform-specific implementation to attempt to obtain a unique network identifier.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
        protected override bool TryGetNetworkIdentifierImpl(out DicomUID identifier)
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();

            if (profile != null)
            {
                try
                {
                    var hex = profile.NetworkAdapter.NetworkAdapterId.ToString("N").Substring(0, 12);
                    var dec = long.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    {
                        identifier = DicomUID.Append(DicomImplementation.ClassUID, dec);
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
