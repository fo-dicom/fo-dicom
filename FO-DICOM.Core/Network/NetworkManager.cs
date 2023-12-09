// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    public interface INetworkManager
    {
        /// <summary>
        /// Platform-specific implementation to create a network listener object.
        /// </summary>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">Network port to listen to.</param>
        /// <returns>Network listener implementation.</returns>
        INetworkListener CreateNetworkListener(string ipAddress, int port);

        /// <summary>
        /// Platform-specific implementation to create a network stream object.
        /// </summary>
        /// <param name="options">The various options that specify how the network stream must be created</param>
        /// <returns>Network stream implementation.</returns>
        INetworkStream CreateNetworkStream(NetworkStreamCreationOptions options);

        /// <summary>
        /// Platform-specific implementation to check whether specified <paramref name="exception"/> represents a socket exception.
        /// </summary>
        /// <param name="exception">Exception to validate.</param>
        /// <param name="errorCode">Error code, valid if <paramref name="exception"/> is socket exception.</param>
        /// <param name="errorDescriptor">Error descriptor, valid if <paramref name="exception"/> is socket exception.</param>
        /// <returns>True if <paramref name="exception"/> is socket exception, false otherwise.</returns>
        bool IsSocketException(Exception exception, out int errorCode, out string errorDescriptor);

        /// <summary>
        /// Platform-specific implementation to attempt to obtain a unique network identifier, e.g. based on a MAC address.
        /// </summary>
        /// <param name="identifier">Unique network identifier, if found.</param>
        /// <returns>True if network identifier could be obtained, false otherwise.</returns>
        bool TryGetNetworkIdentifier(out DicomUID identifier);

        /// <summary>
        /// Gets the machine name
        /// </summary>
        string MachineName { get; }
    }
    
    /// <summary>
    /// Abstract manager class for network operations.
    /// </summary>
    public abstract class NetworkManager : INetworkManager
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

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Implementation of the machine name getter.
        /// </summary>
        protected internal abstract string MachineNameImpl { get; }

        #endregion

        #region METHODS

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
        /// <param name="networkStreamCreationOptions">The various options that specify how the network stream must be created</param>
        /// <returns>Network stream implementation.</returns>
        protected internal abstract INetworkStream CreateNetworkStreamImpl(NetworkStreamCreationOptions networkStreamCreationOptions);

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

        INetworkListener INetworkManager.CreateNetworkListener(string ipAddress, int port) 
            => CreateNetworkListenerImpl(ipAddress, port);

        INetworkStream INetworkManager.CreateNetworkStream(NetworkStreamCreationOptions options)
            => CreateNetworkStreamImpl(options);

        bool INetworkManager.IsSocketException(Exception exception, out int errorCode, out string errorDescriptor)
            => IsSocketExceptionImpl(exception, out errorCode, out errorDescriptor);

        bool INetworkManager.TryGetNetworkIdentifier(out DicomUID identifier)
            => TryGetNetworkIdentifierImpl(out identifier);

        string INetworkManager.MachineName => MachineNameImpl;

        #endregion
    }
}
