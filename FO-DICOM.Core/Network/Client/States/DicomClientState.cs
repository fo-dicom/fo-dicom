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
        
        public override string ToString() => "IDLE";
    }

    public class DicomClientConnectState : DicomClientState
    {
        public static readonly DicomClientConnectState Instance = new DicomClientConnectState();
        
        public override string ToString() => "CONNECTING";
    }

    public class DicomClientRequestAssociationState : DicomClientWithConnectionState
    {
        public static readonly DicomClientRequestAssociationState Instance = new DicomClientRequestAssociationState();
        
        public override string ToString() => "REQUESTING ASSOCIATION";
    }

    public class DicomClientSendingRequestsState : DicomClientWithAssociationState
    {
        public static readonly DicomClientSendingRequestsState Instance = new DicomClientSendingRequestsState();
        
        public override string ToString() => "SENDING REQUESTS";
    }

    public class DicomClientLingeringState : DicomClientWithAssociationState
    {
        public static readonly DicomClientLingeringState Instance = new DicomClientLingeringState();
        
        public override string ToString() => "LINGERING ASSOCIATION";
    }

    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState
    {
        public static readonly DicomClientReleaseAssociationState Instance = new DicomClientReleaseAssociationState();
        
        public override string ToString() => "RELEASING ASSOCIATION";
    }

    public class DicomClientAbortState : DicomClientWithAssociationState
    {
        public static readonly DicomClientAbortState Instance = new DicomClientAbortState();
        
        public override string ToString() => "ABORTING ASSOCIATION";
    }
}