// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using NLog.Config;
using NLog.Targets;

namespace FellowOakDicom.Tests.Helpers
{

    internal static class NLogHelper
    {
        #region Fields

        private static readonly LoggingConfiguration _config = new LoggingConfiguration();

        #endregion

        #region Unit tests

        internal static MemoryTarget AssignMemoryTarget(string name, string layout, NLog.LogLevel minLogLevel = null)
        {
            var target = new MemoryTarget { Layout = layout };

            lock (_config)
            {
                _config.AddTarget(name, target);
                _config.LoggingRules.Add(new LoggingRule(name, minLogLevel ?? NLog.LogLevel.Debug, target));

                NLog.LogManager.Configuration = _config;
            }

            return target;
        }

        #endregion
    }
}
