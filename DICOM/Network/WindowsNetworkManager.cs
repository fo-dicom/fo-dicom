// Copyright (c) 2012-2018 fo-dicom contributors.
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

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override INetworkListener CreateNetworkListenerImpl(string ipAddress, int port)
        {
            return new WindowsNetworkListener(ipAddress, port);
        }

        /// <inheritdoc />
        protected override INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            return new WindowsNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);
        }

        /// <inheritdoc />
        protected override bool IsSocketExceptionImpl(Exception exception, out int errorCode, out string errorDescriptor)
        {
            var socketEx = exception as SocketException;
            if (socketEx != null)
            {
                errorCode = (int)socketEx.SocketErrorCode;
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
