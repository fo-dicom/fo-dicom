// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Helpers;

namespace Dicom.Bugs
{
    using System;

    using Dicom.Log;

    using Xunit;

    public class GH258
    {
        [Fact]
        public void Log_ExceptionInFormattedString_DisplaysExceptionMessage()
        {
            var name = nameof(GH258) + "A";
            LogManager.SetImplementation(NLogManager.Instance);
            var target = NLogHelper.AssignMemoryTarget(name, @"${message}");

            var logger = LogManager.GetLogger(name);
            logger.Debug("Message: {0} {1}", new NullReferenceException(), target.Name);

            var expected = $"Message: {new NullReferenceException()} {target.Name}";
            var actual = target.Logs[0];
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Log_ExceptionNotInFormattedString_ExceptionLoggedNotIncludedInMessage()
        {
            var name = nameof(GH258) + "B";

            LogManager.SetImplementation(NLogManager.Instance);
            var target = NLogHelper.AssignMemoryTarget(name, @"${exception} ${message}");

            var logger = LogManager.GetLogger(name);
            logger.Debug("Message but no exception", new NullReferenceException());

            var expected = $"{new NullReferenceException().Message} Message but no exception";
            var actual = target.Logs[0];
            Assert.Equal(expected, actual);
        }
    }
}
