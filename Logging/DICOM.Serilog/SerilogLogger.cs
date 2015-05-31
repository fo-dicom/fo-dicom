using Dicom.Log;
using Serilog;

namespace DICOM.Log
{
    internal class SerilogLogger :Logger
    {
        private readonly ILogger _serilog;

        public SerilogLogger(ILogger serilog) {
            _serilog = serilog;
        }

        public override void Log(LogLevel level, string msg, params object[] args) {
            _serilog.Write(level.ToSerilog(), msg, args);
        }
    }
}
