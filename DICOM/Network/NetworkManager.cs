// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;

    public abstract class NetworkManager
    {
        #region FIELDS

        private static NetworkManager implementation;

        #endregion

        #region CONSTRUCTORS

        static NetworkManager()
        {
            SetImplementation(DesktopNetworkManager.Instance);
        }

        #endregion

        #region METHODS

        public static void SetImplementation(NetworkManager impl)
        {
            implementation = impl;
        }

        public static INetworkListener CreateNetworkListener(int port)
        {
            return implementation.CreateNetworkListenerImpl(port);
        }

        public static INetworkStream CreateNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            return implementation.CreateNetworkStreamImpl(host, port, useTls, noDelay, ignoreSslPolicyErrors);
        }

        public static bool IsSocketException(Exception exception, out int errorCode, out string errorDescriptor)
        {
            return implementation.IsSocketExceptionImpl(exception, out errorCode, out errorDescriptor);
        }

        public static bool TryGetNetworkIdentifier(out DicomUID identifier)
        {
            return implementation.TryGetNetworkIdentifierImpl(out identifier);
        }

        protected abstract INetworkListener CreateNetworkListenerImpl(int port);

        protected abstract INetworkStream CreateNetworkStreamImpl(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors);

        protected abstract bool IsSocketExceptionImpl(Exception exception, out int errorCode, out string errorDescriptor);

        protected abstract bool TryGetNetworkIdentifierImpl(out DicomUID identifier);

        #endregion
    }
}
