// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;

    /// <summary>
    /// Abstract manager class for network operations.
    /// </summary>
    public abstract class NetworkManager
    {
        #region FIELDS

        /// <summary>
        /// Network manager implementation in current use.
        /// </summary>
        private static NetworkManager implementation;

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
        public static string MachineName
        {
            get
            {
                return implementation.MachineNameImpl;
            }
        }

        /// <summary>
        /// Implementation of the machine name getter.
        /// </summary>
        protected abstract string MachineNameImpl { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Sets the network manager implementation to use.
        /// </summary>
        /// <param name="impl">Network manager implementation.</param>
        public static void SetImplementation(NetworkManager impl)
        {
            implementation = impl;
        }

#if !NET35
        /// <summary>
        /// Create a network listener object.
        /// </summary>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        public static INetworkListener CreateNetworkListener(int port)
        {
            return implementation.CreateNetworkListenerImpl(port);
        }

        /// <summary>
        /// Create a network stream object.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        /// <returns>Network stream implementation.</returns>
        public static INetworkStream CreateNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            return implementation.CreateNetworkStreamImpl(host, port, useTls, noDelay, ignoreSslPolicyErrors);
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
            return implementation.IsSocketExceptionImpl(exception, out errorCode, out errorDescriptor);
        }
#endif

        /// <summary>
        /// Attempt to obtain a unique network identifier, e.g. based on a MAC address.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
        public static bool TryGetNetworkIdentifier(out DicomUID identifier)
        {
            return implementation.TryGetNetworkIdentifierImpl(out identifier);
        }

#if !NET35
        /// <summary>
        /// Platform-specific implementation to create a network listener object.
        /// </summary>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        protected abstract INetworkListener CreateNetworkListenerImpl(int port);

        /// <summary>
        /// Platform-specific implementation to create a network stream object.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        /// <returns>Network stream implementation.</returns>
        protected abstract INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors);

        /// <summary>
        /// Platform-specific implementation to check whether specified <paramref name="exception"/> represents a socket exception.
        /// </summary>
        /// <param name="exception">Exception to validate.</param>
        /// <param name="errorCode">Error code, valid if <paramref name="exception"/> is socket exception.</param>
        /// <param name="errorDescriptor">Error descriptor, valid if <paramref name="exception"/> is socket exception.</param>
        /// <returns>True if <paramref name="exception"/> is socket exception, false otherwise.</returns>
        protected abstract bool IsSocketExceptionImpl(Exception exception, out int errorCode, out string errorDescriptor);
#endif

        /// <summary>
        /// Platform-specific implementation to attempt to obtain a unique network identifier, e.g. based on a MAC address.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
        protected abstract bool TryGetNetworkIdentifierImpl(out DicomUID identifier);

        #endregion
    }
}
