// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.AspNetCore.Configs
{
    public class ServerConfiguration
    {
        public int Port { get; set; } = 104;

        public string AETitle { get; set; } = "FODICOMSERVER";
    }
}
