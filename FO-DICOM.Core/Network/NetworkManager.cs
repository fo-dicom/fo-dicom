// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    /// <summary>
    /// Abstract manager class for network operations.
    /// </summary>
    public abstract class NetworkManager
    {
        #region FIELDS

        /// <summary>
        /// String representation of the "Any" IPv4 address, i.e. specifying the listener to listen to all applicable IPv4 addresses.
        /// </summary>
        public const string IPv4Any = "0.0.0.0";

        /// <summary>
        /// String representation of the "Any" IPv6 address, i.e. specifying the listener to listen to all applicable IPv6 addresses.
        /// </summary>
        public const string IPv6Any = "::";

        /// <summary>
        /// String representation of the "Loopback" IPv4 address, i.e. specifying the listener to listen to the local IPv4 host.
        /// </summary>
        public const string IPv4Loopback = "127.0.0.1";

        /// <summary>
        /// String representation of the "Loopback" IPv6 address, i.e. specifying the listener to listen to the local IPv6 host.
        /// </summary>
        public const string IPv6Loopback = "::1";

        /// <summary>
        /// Network manager implementation in current use.
        /// </summary>
        private static NetworkManager _implementation;

        private const int DefaultAssociationTimeout = 5000;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the single platform-specific network manager.
        /// </summary>
        static NetworkManager()
        {
            SetImplementation(Setup.GetSinglePlatformInstance<NetworkManager>());
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets machine name.
        /// </summary>
        public static string MachineName => _implementation.MachineNameImpl;

        /// <summary>
        /// Implementation of the machine name getter.
        /// </summary>
        protected internal abstract string MachineNameImpl { get; }

        /// <summary>
        /// Gets the currently active, static implementation
        /// </summary>
        internal static NetworkManager Implementation => _implementation;

        #endregion

        #region METHODS

        /// <summary>
        /// Instead of statically 'setting' the network manager (which changes the behavior for everything in the same AppDomain)
        /// it is also possible to use a scoped network manager which only impacts consumers with a reference to this scoped network manager.
        /// </summary>
        public static ScopedNetworkManager Use(NetworkManager implementation)
        {
            return new ScopedNetworkManager(implementation);
        }

        /// <summary>
        /// Sets the network manager implementation to use.
        /// </summary>
        /// <param name="impl">Network manager implementation.</param>
        public static void SetImplementation(NetworkManager impl)
        {
            _implementation = impl;
        }

        /// <summary>
        /// Create a network listener object, listening to all IPv4 addressess.
        /// </summary>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        [Obsolete("Use the CreateNetworkListener(string, int) overload with IPv4Any in string argument.")]
        public static INetworkListener CreateNetworkListener(int port)
        {
            return Use(_implementation).CreateNetworkListener(IPv4Any, port);
        }

        /// <summary>
        /// Create a network listener object.
        /// </summary>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        public static INetworkListener CreateNetworkListener(string ipAddress, int port)
        {
            return Use(_implementation).CreateNetworkListener(ipAddress, port);
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
        public static INetworkStream CreateNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors, int millisecondsTimeout = DefaultAssociationTimeout)
        {
            return Use(_implementation).CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);
        }

        /// <summary>
        /// Checks whether specified <paramref name="exception"/> represents a socket exception.
        /// </summary>
        /// <param name="exception">Exception to validate.</param>
        /// <param name="errorCode">Error code, valid if <paramref name="exception"/> is socket exception.</param>
        /// <param name="errorDescriptor">Error descriptor, valid if <paramref name="exception"/> is socket exception.</param>
        /// <returns>True if <paramref name="exception"/> is socket exception, false otherwise.</returns>
        public static bool IsSocketException(Exception exception, out int errorCode, out string errorDescriptor)
        {
            return Use(_implementation).IsSocketException(exception, out errorCode, out errorDescriptor);
        }

        /// <summary>
        /// Attempt to obtain a unique network identifier, e.g. based on a MAC address.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
        public static bool TryGetNetworkIdentifier(out DicomUID identifier)
        {
            return Use(_implementation).TryGetNetworkIdentifier(out identifier);
        }

        /// <summary>
        /// Platform-specific implementation to create a network listener object.
        /// </summary>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        protected internal abstract INetworkListener CreateNetworkListenerImpl(string ipAddress, int port);

        /// <summary>
        /// Platform-specific implementation to create a network stream object.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        /// <param name="millisecondsTimeout">The timeout in milliseconds for creating a network stream</param>
        /// <returns>Network stream implementation.</returns>
        protected internal abstract INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors, int millisecondsTimeout);

        /// <summary>
        /// Platform-specific implementation to check whether specified <paramref name="exception"/> represents a socket exception.
        /// </summary>
        /// <param name="exception">Exception to validate.</param>
        /// <param name="errorCode">Error code, valid if <paramref name="exception"/> is socket exception.</param>
        /// <param name="errorDescriptor">Error descriptor, valid if <paramref name="exception"/> is socket exception.</param>
        /// <returns>True if <paramref name="exception"/> is socket exception, false otherwise.</returns>
        protected internal abstract bool IsSocketExceptionImpl(Exception exception, out int errorCode, out string errorDescriptor);

        /// <summary>
        /// Platform-specific implementation to attempt to obtain a unique network identifier, e.g. based on a MAC address.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
        protected internal abstract bool TryGetNetworkIdentifierImpl(out DicomUID identifier);

        #endregion
    }
}
