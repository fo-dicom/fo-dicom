// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Memory
{
    public interface IMemoryProvider
    {
        IMemory Provide(int length);
    }
}
