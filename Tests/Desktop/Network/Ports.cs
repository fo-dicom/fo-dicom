using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
