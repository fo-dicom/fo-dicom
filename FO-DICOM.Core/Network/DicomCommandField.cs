// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    public enum DicomCommandField : ushort
    {
        CStoreRequest = 0x0001,

        CStoreResponse = 0x8001,

        CGetRequest = 0x0010,

        CGetResponse = 0x8010,

        CFindRequest = 0x0020,

        CFindResponse = 0x8020,

        CMoveRequest = 0x0021,

        CMoveResponse = 0x8021,

        CEchoRequest = 0x0030,

        CEchoResponse = 0x8030,

        NEventReportRequest = 0x0100,

        NEventReportResponse = 0x8100,

        NGetRequest = 0x0110,

        NGetResponse = 0x8110,

        NSetRequest = 0x0120,

        NSetResponse = 0x8120,

        NActionRequest = 0x0130,

        NActionResponse = 0x8130,

        NCreateRequest = 0x0140,

        NCreateResponse = 0x8140,

        NDeleteRequest = 0x0150,

        NDeleteResponse = 0x8150,

        CCancelRequest = 0x0FFF
    }
}
