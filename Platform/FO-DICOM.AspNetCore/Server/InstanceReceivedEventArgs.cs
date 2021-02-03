// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;

namespace FellowOakDicom.AspNetCore.Server
{
    public class InstanceReceivedEventArgs
    {

        public InstanceReceivedEventArgs(DicomAssociation association, DicomFile instance)
        {
            Association = association;
            Instance = instance;
        }

        public DicomAssociation Association { get; }

        public DicomFile Instance { get; }

        public DicomStatus ResultStatus { get; set; } = DicomStatus.Success;

    }
}
