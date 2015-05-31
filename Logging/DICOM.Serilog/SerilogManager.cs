using Dicom.Log;

namespace DICOM.Log
{
    public class SerilogManager :LogManager{
        public override Logger GetLogger(string name) {
            var serilogLogger = Serilog.Log.ForContext("fo-DICOM", name);
            return new SerilogLogger(serilogLogger);
        }
    }
}
