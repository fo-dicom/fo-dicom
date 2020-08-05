// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    /// <summary>
    /// Manager class for network operations.
    /// </summary>
    public class ScopedNetworkManager
    {
        #region FIELDS

        /// <summary>
        /// String representation of the "Any" IPv4 address, i.e. specifying the listener to listen to all applicable IPv4 addresses.
        /// </summary>
        private const string IPv4Any = "0.0.0.0";

        /// <summary>
        /// String representation of the "Any" IPv6 address, i.e. specifying the listener to listen to all applicable IPv6 addresses.
        /// </summary>
        private const string IPv6Any = "::";

        /// <summary>
        /// String representation of the "Loopback" IPv4 address, i.e. specifying the listener to listen to the local IPv4 host.
        /// </summary>
        private const string IPv4Loopback = "127.0.0.1";

        /// <summary>
        /// String representation of the "Loopback" IPv6 address, i.e. specifying the listener to listen to the local IPv6 host.
        /// </summary>
        private const string IPv6Loopback = "::1";

        /// <summary>
        /// Scoped network manager in current use.
        /// </summary>
        private readonly NetworkManager _implementation;

        private const int DefaultAssociationTimeout = 5000;

        #endregion

        #region CONSTRUCTORS

        public ScopedNetworkManager(NetworkManager implementation)
        {
            _implementation = implementation;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets machine name.
        /// </summary>
        public string MachineName => _implementation.MachineNameImpl;

        #endregion

        #region METHODS

        /// <summary>
        /// Create a network listener object, listening to all IPv4 addressess.
        /// </summary>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        [Obsolete("Use the CreateNetworkListener(string, int) overload with IPv4Any in string argument.")]
        public INetworkListener CreateNetworkListener(int port)
        {
            return CreateNetworkListener(IPv4Any, port);
        }

        /// <summary>
        /// Create a network listener object.
        /// </summary>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        public INetworkListener CreateNetworkListener(string ipAddress, int port)
        {
            return _implementation.CreateNetworkListenerImpl(ipAddress, port);
        }

        /// <summary>
        /// Create a network stream object.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        /// <param name="millisecondsTimeout">a timeout in milliseconds for creating a network stream</param>
        /// <returns>Network stream implementation.</returns>
        public INetworkStream CreateNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors,
            int millisecondsTimeout = DefaultAssociationTimeout)
        {
            return _implementation.CreateNetworkStreamImpl(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);
        }

        /// <summary>
        /// Checks whether specified <paramref name="exception"/> represents a socket exception.
        /// </summary>
        /// <param name="exception">Exception to validate.</param>
        /// <param name="errorCode">Error code, valid if <paramref name="exception"/> is socket exception.</param>
        /// <param name="errorDescriptor">Error descriptor, valid if <paramref name="exception"/> is socket exception.</param>
        /// <returns>True if <paramref name="exception"/> is socket exception, false otherwise.</returns>
        public bool IsSocketException(Exception exception, out int errorCode, out string errorDescriptor)
        {
            return _implementation.IsSocketExceptionImpl(exception, out errorCode, out errorDescriptor);
        }

        /// <summary>
        /// Attempt to obtain a unique network identifier, e.g. based on a MAC address.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
        public bool TryGetNetworkIdentifier(out DicomUID identifier)
        {
            return _implementation.TryGetNetworkIdentifierImpl(out identifier);
        }

        #endregion
    }
}
