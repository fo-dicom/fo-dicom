using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom.Log;
using Xunit.Abstractions;

namespace Dicom
{
    internal class TestOutputHelperLogger: Logger
    {
        private readonly ITestOutputHelper _testOutputHelper;
        
        public TestOutputHelperLogger(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        public override void Log(LogLevel level, string msg, params object[] args)
        {
            var positionMsg = NameFormatToPositionalFormat(msg);
            this._testOutputHelper.WriteLine(level.ToString() + ": " + positionMsg, args);
        }
    }
}
