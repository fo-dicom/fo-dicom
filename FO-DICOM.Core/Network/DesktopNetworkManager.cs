// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

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
        protected internal override string MachineNameImpl => Environment.GetEnvironmentVariable("COMPUTERNAME");

        #endregion

        #region METHODS

        /// <inheritdoc />
        protected internal override INetworkListener CreateNetworkListenerImpl(string ipAddress, int port) => 
            CreateNetworkListenerImpl(new NetworkListenerCreationOptions { IpAddress = ipAddress, Port = port });

        protected internal override INetworkListener CreateNetworkListenerImpl(NetworkListenerCreationOptions options) => 
            new DesktopNetworkListener(options);

        /// <inheritdoc />
        protected internal override INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors, int millisecondsTimeout)
        {
            var options = new NetworkStreamCreationOptions
            {
                Host = host,
                Port = port,
                UseTls = useTls,
                NoDelay = noDelay,
                IgnoreSslPolicyErrors = ignoreSslPolicyErrors,
                Timeout = TimeSpan.FromMilliseconds(millisecondsTimeout)
            };
            return CreateNetworkStreamImpl(options);
        }

        protected internal override INetworkStream CreateNetworkStreamImpl(NetworkStreamCreationOptions options) => 
            CreateNetworkStreamImplAsync(options, CancellationToken.None).GetAwaiter().GetResult();

        protected override async Task<INetworkStream> CreateNetworkStreamImplAsync(NetworkStreamCreationOptions options, CancellationToken cancellationToken)
            => await DesktopNetworkStream.CreateAsClientAsync(options, cancellationToken).ConfigureAwait(false);

        /// <inheritdoc />
        protected internal override bool IsSocketExceptionImpl(Exception exception, out int errorCode, out string errorDescriptor)
        {
            if (exception is SocketException socketEx)
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
        protected internal override bool TryGetNetworkIdentifierImpl(out DicomUID identifier)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            for (var i = 0; i < interfaces.Length; i++)
            {
                if (NetworkInterface.LoopbackInterfaceIndex == i
                    || interfaces[i].OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }

                var hex = interfaces[i].GetPhysicalAddress().ToString();
                if (string.IsNullOrEmpty(hex))
                {
                    continue;
                }

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
