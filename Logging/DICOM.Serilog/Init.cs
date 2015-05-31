using Dicom.Log;

namespace DICOM.Log
{
    class Init {
        static Init() {
            LogManager.Default = new SerilogManager();
        }
    }
}
