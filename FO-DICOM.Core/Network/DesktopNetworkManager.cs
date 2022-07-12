// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network.Client;
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
        private readonly IDesktopNetworkStreamFactory _desktopNetworkStreamFactory;

        public DesktopNetworkManager(IDesktopNetworkStreamFactory desktopNetworkStreamFactory)
        {
            _desktopNetworkStreamFactory = desktopNetworkStreamFactory ?? throw new ArgumentNullException(nameof(desktopNetworkStreamFactory));
        }
        
        #region PROPERTIES

        /// <inheritdoc />
        protected internal override string MachineNameImpl => Environment.GetEnvironmentVariable("COMPUTERNAME");

        #endregion

        #region METHODS

        /// <inheritdoc />
        protected internal override INetworkListener CreateNetworkListenerImpl(string ipAddress, int port) => 
            CreateNetworkListenerImpl(new NetworkListenerCreationOptions { IpAddress = ipAddress, Port = port });

        protected internal override INetworkListener CreateNetworkListenerImpl(NetworkListenerCreationOptions options) => 
            new DesktopNetworkListener(_desktopNetworkStreamFactory, options);

        /// <inheritdoc />
        protected internal override INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, int millisecondsTimeout)
        {
            var options = new NetworkStreamCreationOptions
            {
                Host = host,
                Port = port,
                UseTls = useTls,
                NoDelay = noDelay,
                Timeout = TimeSpan.FromMilliseconds(millisecondsTimeout)
            };
            return CreateNetworkStreamImpl(options);
        }

        protected internal override INetworkStream CreateNetworkStreamImpl(NetworkStreamCreationOptions options) => 
            CreateNetworkStreamImplAsync(options, CancellationToken.None).GetAwaiter().GetResult();

        protected override async Task<INetworkStream> CreateNetworkStreamImplAsync(NetworkStreamCreationOptions options, CancellationToken cancellationToken)
            => await _desktopNetworkStreamFactory.CreateAsClientAsync(options, cancellationToken).ConfigureAwait(false);

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
