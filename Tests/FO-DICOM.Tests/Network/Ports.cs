// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Tests.Network
{

    public static class Ports
    {
        private static object _lock = new object();
        private static int _port = 11113;

        public static int GetNext()
        {
            lock (_lock)
            {
                return _port++;
            }
        }
    }
}
