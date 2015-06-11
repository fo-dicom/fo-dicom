using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom.Log;

namespace DICOM.Log
{
    class Init {
        static Init() {
            LogManager.Default = new NLogManager();
        }
    }
}
