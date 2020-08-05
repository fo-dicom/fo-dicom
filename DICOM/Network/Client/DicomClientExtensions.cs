// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Threading.Tasks;
using Dicom.Network.Client.States;

namespace Dicom.Network.Client
{
    /// <summary>
    /// Internal extension methods that provide easy access to the various transitions a DICOM client can go through
    /// </summary>
    internal static class DicomClientExtensions
    {
        public static Task<IDicomClientState> TransitionToIdleState(this DicomClient dicomClient, DicomClientCancellation cancellation)
        {
            var idleState = new DicomClientIdleState(dicomClient);
            return dicomClient.Transition(idleState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToConnectState(this DicomClient dicomClient, DicomClientCancellation cancellation)
        {
            var connectState = new DicomClientConnectState(dicomClient);
            return dicomClient.Transition(connectState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToRequestAssociationState(this DicomClient dicomClient, IInitialisationWithConnectionParameters parameters,
            DicomClientCancellation cancellation)
        {
            return TransitionToRequestAssociationState(dicomClient, parameters.Connection, cancellation);
        }

        public static Task<IDicomClientState> TransitionToRequestAssociationState(this DicomClient dicomClient, IDicomClientConnection connection,
            DicomClientCancellation cancellation)
        {
            var requestAssociationParameters = new DicomClientRequestAssociationState.InitialisationParameters(connection);
            var requestAssociationState = new DicomClientRequestAssociationState(dicomClient, requestAssociationParameters);
            return dicomClient.Transition(requestAssociationState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToSendingRequestsState(this DicomClient dicomClient,
            IInitialisationWithAssociationParameters parameters, DicomClientCancellation cancellation)
        {
            return TransitionToSendingRequestsState(dicomClient, parameters, parameters.Association, cancellation);
        }

        public static Task<IDicomClientState> TransitionToSendingRequestsState(this DicomClient dicomClient, IInitialisationWithConnectionParameters parameters,
            DicomAssociation association, DicomClientCancellation cancellation)
        {
            var sendingRequestsParameters = new DicomClientSendingRequestsState.InitialisationParameters(association, parameters.Connection);
            var sendingRequestsState = new DicomClientSendingRequestsState(dicomClient, sendingRequestsParameters);
            return dicomClient.Transition(sendingRequestsState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToLingerState(this DicomClient dicomClient,
            IInitialisationWithAssociationParameters parameters, DicomClientCancellation cancellation)
        {
            var lingerParameters = new DicomClientLingeringState.InitialisationParameters(parameters.Association, parameters.Connection);
            var lingerState = new DicomClientLingeringState(dicomClient, lingerParameters);
            return dicomClient.Transition(lingerState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToReleaseAssociationState(this DicomClient dicomClient,
            IInitialisationWithAssociationParameters parameters, DicomClientCancellation cancellation)
        {
            var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(parameters.Association, parameters.Connection);
            var releaseAssociationState = new DicomClientReleaseAssociationState(dicomClient, releaseAssociationParameters);
            return dicomClient.Transition(releaseAssociationState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToAbortState(this DicomClient dicomClient, IInitialisationWithConnectionParameters parameters,
            DicomClientCancellation cancellation)
        {
            var abortParameters = new DicomClientAbortState.InitialisationParameters(parameters.Connection);
            var abortState = new DicomClientAbortState(dicomClient, abortParameters);
            return dicomClient.Transition(abortState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToCompletedState(this DicomClient dicomClient, IInitialisationWithConnectionParameters parameters,
            DicomClientCancellation cancellation)
        {
            return TransitionToCompletedState(dicomClient, parameters.Connection, cancellation);
        }

        public static Task<IDicomClientState> TransitionToCompletedState(this DicomClient dicomClient, IDicomClientConnection connection,
            DicomClientCancellation cancellation)
        {
            var completedParameters = new DicomClientCompletedState.DicomClientCompletedWithoutErrorInitialisationParameters(connection);
            var completedState = new DicomClientCompletedState(dicomClient, completedParameters);
            return dicomClient.Transition(completedState, cancellation);
        }

        public static Task<IDicomClientState> TransitionToCompletedWithErrorState(this DicomClient dicomClient,
            IInitialisationWithConnectionParameters parameters,
            Exception exception, DicomClientCancellation cancellation)
        {
            return TransitionToCompletedWithErrorState(dicomClient, parameters.Connection, exception, cancellation);
        }

        public static Task<IDicomClientState> TransitionToCompletedWithErrorState(this DicomClient dicomClient,
            IDicomClientConnection connection,
            Exception exception, DicomClientCancellation cancellation)
        {
            var completedWithErrorParameters = connection == null
                ? new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception)
                : new DicomClientCompletedState.DicomClientCompletedWithErrorInitialisationParameters(exception, connection);
            var completedWithErrorState = new DicomClientCompletedState(dicomClient, completedWithErrorParameters);
            return dicomClient.Transition(completedWithErrorState, cancellation);
        }
    }
}
