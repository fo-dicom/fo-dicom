// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Helpers
{
    using Dicom.Log;

    using Xunit.Abstractions;

    internal class TestOutputHelperLogger: Logger
    {
        private readonly ITestOutputHelper _testOutputHelper;
        
        public TestOutputHelperLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public override void Log(LogLevel level, string msg, params object[] args)
        {
            var positionMsg = NameFormatToPositionalFormat(msg);
            _testOutputHelper.WriteLine(level + ": " + positionMsg, args);
        }
    }
}
