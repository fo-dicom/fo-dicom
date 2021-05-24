// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network.Client.States
{
    public abstract class DicomClientState { }

    public abstract class DicomClientWithConnectionState : DicomClientState { }

    public abstract class DicomClientWithAssociationState : DicomClientWithConnectionState { }

    public class DicomClientIdleState : DicomClientState
    {
        public static readonly DicomClientIdleState Instance = new DicomClientIdleState();
    }

    public class DicomClientConnectState : DicomClientState
    {
        public static readonly DicomClientIdleState Instance = new DicomClientIdleState();
    }

    public class DicomClientRequestAssociationState : DicomClientWithConnectionState
    {
        public static readonly DicomClientRequestAssociationState Instance = new DicomClientRequestAssociationState();
    }

    public class DicomClientSendingRequestsState : DicomClientWithAssociationState
    {
        public static readonly DicomClientSendingRequestsState Instance = new DicomClientSendingRequestsState();
    }

    public class DicomClientLingeringState : DicomClientWithAssociationState
    {
        public static readonly DicomClientLingeringState Instance = new DicomClientLingeringState();
    }

    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState
    {
        public static readonly DicomClientReleaseAssociationState Instance = new DicomClientReleaseAssociationState();
    }

    public class DicomClientAbortState : DicomClientWithAssociationState
    {
        public static readonly DicomClientAbortState Instance = new DicomClientAbortState();
    }
}