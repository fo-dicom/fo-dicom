// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network.Client.States
{
    [Obsolete(nameof(DicomClientState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public abstract class DicomClientState { }

    [Obsolete(nameof(DicomClientWithConnectionState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public abstract class DicomClientWithConnectionState : DicomClientState { }

    [Obsolete(nameof(DicomClientWithAssociationState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public abstract class DicomClientWithAssociationState : DicomClientWithConnectionState { }

    [Obsolete(nameof(DicomClientIdleState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public class DicomClientIdleState : DicomClientState
    {
        public static readonly DicomClientIdleState Instance = new DicomClientIdleState();
        
        public override string ToString() => "IDLE";
    }

    [Obsolete(nameof(DicomClientConnectState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public class DicomClientConnectState : DicomClientState
    {
        public static readonly DicomClientConnectState Instance = new DicomClientConnectState();
        
        public override string ToString() => "CONNECTING";
    }

    [Obsolete(nameof(DicomClientRequestAssociationState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public class DicomClientRequestAssociationState : DicomClientWithConnectionState
    {
        public static readonly DicomClientRequestAssociationState Instance = new DicomClientRequestAssociationState();
        
        public override string ToString() => "REQUESTING ASSOCIATION";
    }

    [Obsolete(nameof(DicomClientSendingRequestsState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public class DicomClientSendingRequestsState : DicomClientWithAssociationState
    {
        public static readonly DicomClientSendingRequestsState Instance = new DicomClientSendingRequestsState();
        
        public override string ToString() => "SENDING REQUESTS";
    }

    [Obsolete(nameof(DicomClientLingeringState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public class DicomClientLingeringState : DicomClientWithAssociationState
    {
        public static readonly DicomClientLingeringState Instance = new DicomClientLingeringState();
        
        public override string ToString() => "LINGERING ASSOCIATION";
    }

    [Obsolete(nameof(DicomClientReleaseAssociationState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public class DicomClientReleaseAssociationState : DicomClientWithAssociationState
    {
        public static readonly DicomClientReleaseAssociationState Instance = new DicomClientReleaseAssociationState();
        
        public override string ToString() => "RELEASING ASSOCIATION";
    }

    [Obsolete(nameof(DicomClientAbortState) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
    public class DicomClientAbortState : DicomClientWithAssociationState
    {
        public static readonly DicomClientAbortState Instance = new DicomClientAbortState();
        
        public override string ToString() => "ABORTING ASSOCIATION";
    }
}