// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using ILogger = FellowOakDicom.Log.ILogger;
using ILogManager = FellowOakDicom.Log.ILogManager;
using LogLevel = FellowOakDicom.Log.LogLevel;

namespace FellowOakDicom.Tests.Log
{
    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public class LogManagerTest
    {
        [Fact]
        public void ShouldSupportLoggingThroughObsoleteLogManager()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services
                .AddLogging(logging => logging.ClearProviders())
                .AddFellowOakDicom()
                .AddLogManager<TestLoggerManager>()
                .BuildServiceProvider();

            // Act
            var testLogManager = serviceProvider.GetRequiredService<ILogManager>();
            var testLogger = testLogManager.GetLogger("Test");
            testLogger.Info("This is a test message");

            // Assert
            Assert.IsType<TestLoggerManager>(testLogManager);
            Assert.IsType<TestLogger>(testLogger);
            var logEntries = (testLogManager as TestLoggerManager)!.LogEntries;
            var logEntry = logEntries.Single();
            Assert.Equal("This is a test message", logEntry.Msg);
            Assert.Equal(LogLevel.Info, logEntry.LogLevel);
        }

        [Fact]
        public void ShouldSupportLoggingThroughModernLoggerFactory()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services
                .AddLogging(logging => logging.ClearProviders())
                .AddFellowOakDicom()
                .AddLogManager<TestLoggerManager>()
                .BuildServiceProvider();

            // Act
            var testLogManager = serviceProvider.GetRequiredService<ILogManager>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("Test");
            logger.LogInformation("This is a test message");

            // Assert
            var logEntries = (testLogManager as TestLoggerManager)!.LogEntries;
            var logEntry = logEntries.Single();
            Assert.Equal("This is a test message", logEntry.Msg);
            Assert.Equal(LogLevel.Info, logEntry.LogLevel);
        }

        public class TestLoggerManager: FellowOakDicom.Log.ILogManager
        {
            public readonly List<TestLogEntry> LogEntries;

            public TestLoggerManager()
            {
                LogEntries = new List<TestLogEntry>();
            }

            public ILogger GetLogger(string name)
            {
                return new TestLogger(name, LogEntries);
            }
        }

        public class TestLogger : FellowOakDicom.Log.ILogger
        {
            private readonly string _name;
            private readonly List<TestLogEntry> _entries;

            public TestLogger(string name, List<TestLogEntry> entries)
            {
                _name = name ?? throw new ArgumentNullException(nameof(name));
                _entries = entries ?? throw new ArgumentNullException(nameof(entries));
            }

            public void Log(LogLevel level, string msg, params object[] args)
            {
                _entries.Add(new TestLogEntry(_name, level, msg, args));
            }

            public void Debug(string msg, params object[] args)
            {
                _entries.Add(new TestLogEntry(_name, LogLevel.Debug, msg, args));
            }

            public void Info(string msg, params object[] args)
            {
                _entries.Add(new TestLogEntry(_name, LogLevel.Info, msg, args));
            }

            public void Warn(string msg, params object[] args)
            {
                _entries.Add(new TestLogEntry(_name, LogLevel.Warning, msg, args));
            }

            public void Error(string msg, params object[] args)
            {
                _entries.Add(new TestLogEntry(_name, LogLevel.Error, msg, args));
            }

            public void Fatal(string msg, params object[] args)
            {
                _entries.Add(new TestLogEntry(_name, LogLevel.Fatal, msg, args));
            }
        }

        public class TestLogEntry
        {
            public string Name { get; }
            public LogLevel LogLevel { get; }
            public string Msg { get; }
            public object[] Args { get; }

            public TestLogEntry(string name, LogLevel logLevel, string msg, object[] args)
            {
                Name = name;
                LogLevel = logLevel;
                Msg = msg;
                Args = args;
            }
        }
    }
}
