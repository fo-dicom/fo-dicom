// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Helpers
{
    public class XUnitDicomLogger : ILogger
    {
        private delegate string PrefixEnricher(string prefix);

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly List<PrefixEnricher> _prefixEnrichers;
        private readonly LogLevel _minimumLevel;

        public XUnitDicomLogger(ITestOutputHelper testOutputHelper) : this(testOutputHelper, LogLevel.Debug, new List<PrefixEnricher>()) { }

        XUnitDicomLogger(ITestOutputHelper testOutputHelper, LogLevel minimumLevel, IEnumerable<PrefixEnricher> prefixEnrichers)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            _minimumLevel = minimumLevel;
            _prefixEnrichers = prefixEnrichers?.ToList() ?? throw new ArgumentNullException(nameof(prefixEnrichers));
        }

        XUnitDicomLogger WithPrefixEnricher(PrefixEnricher prefixEnricher)
        {
            if (prefixEnricher == null) throw new ArgumentNullException(nameof(prefixEnricher));

            var prefixEnrichers = new List<PrefixEnricher>(_prefixEnrichers) {prefixEnricher};

            return new XUnitDicomLogger(_testOutputHelper, _minimumLevel, prefixEnrichers);
        }

        public XUnitDicomLogger IncludeThreadId() => WithPrefixEnricher(prefix => $"{prefix} #{System.Threading.Thread.CurrentThread.ManagedThreadId,3}");

        public XUnitDicomLogger IncludeTimestamps() => WithPrefixEnricher(prefix => $"{prefix} {DateTime.Now: HH:mm:ss.fff}");

        public XUnitDicomLogger IncludePrefix(string prefix) => WithPrefixEnricher(existingPrefix => $"{existingPrefix} {prefix, 25}");

        public XUnitDicomLogger WithMinimumLevel(LogLevel minimumLevel) => new XUnitDicomLogger(_testOutputHelper, minimumLevel, _prefixEnrichers);

        public void Log<TState>(LogLevel level, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (level < _minimumLevel)
            {
                return;
            }

            var prefix = _prefixEnrichers.Aggregate(
                $"{nameof(XUnitDicomLogger), 20} {level.ToString().ToUpper(), 7}",
                (intermediatePrefix, enrichPrefix) => enrichPrefix(intermediatePrefix));
            var message = formatter(state, exception);
            var line = $"{prefix} : {message}";
            try
            {
                _testOutputHelper.WriteLine(line);
            }
            catch (Exception)
            {
                // Ignored, trying to log before or after tests cannot be handled properly
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _minimumLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }
    }
}
