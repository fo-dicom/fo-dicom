// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network.Client.Advanced;

namespace FellowOakDicom.Network.Client.States
{
    public interface IInitialisationWithAssociationParameters : IInitialisationWithConnectionParameters
    {
        IAdvancedDicomClientAssociation Association { get; set; }
    }
}
