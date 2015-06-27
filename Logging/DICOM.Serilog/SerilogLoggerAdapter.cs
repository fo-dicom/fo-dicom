using Serilog;

namespace Dicom.Log
{
    internal class SerilogLoggerAdapter :Logger
    {
        private readonly ILogger _serilog;

        public SerilogLoggerAdapter(ILogger serilog) {
            _serilog = serilog;
        }

        public override void Log(LogLevel level, string msg, params object[] args) {
            _serilog.Write(level.ToSerilog(), msg, args);
        }
    }
}
