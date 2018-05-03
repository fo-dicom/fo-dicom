// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public static class Ports
    {
        private static object _lock = new object();
        private static int _port = 11112;

        public static int GetNext()
        {
            lock (_lock)
            {
                return _port++;
            }
        }
    }
}
