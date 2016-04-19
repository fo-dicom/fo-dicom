// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

using Dicom.Log;

using NLog.Config;
using NLog.Targets;

using Xunit;

namespace Dicom.Bugs
{
    public class GH258
    {
        [Fact]
        public void Log_ExceptionInFormattedString_DisplaysExceptionMessage()
        {
            LogManager.SetImplementation(NLogManager.Instance);

            var config = new LoggingConfiguration();

            var target = new MemoryTarget { Layout = @"${message}" };
            config.AddTarget("Memory", target);
            config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, target));

            NLog.LogManager.Configuration = config;

            var logger = LogManager.GetLogger("Test");
            logger.Debug("Message: {0}", new NullReferenceException());

            var expected = $"Message: {new NullReferenceException().Message}";
            var actual = target.Logs[0];
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Log_ExceptionNotInFormattedString_ExceptionLoggedNotIncludedInMessage()
        {
            LogManager.SetImplementation(NLogManager.Instance);

            var config = new LoggingConfiguration();

            var target = new MemoryTarget { Layout = @"${exception} ${message}" };
            config.AddTarget("Memory", target);
            config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, target));

            NLog.LogManager.Configuration = config;

            var logger = LogManager.GetLogger("Test");
            logger.Debug("Message but no exception", new NullReferenceException());

            var expected = $"{new NullReferenceException().Message} Message but no exception";
            var actual = target.Logs[0];
            Assert.Equal(expected, actual);
        }
    }
}