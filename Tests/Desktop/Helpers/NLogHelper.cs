// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Helpers
{
    using NLog.Config;
    using NLog.Targets;

    internal static class NLogHelper
    {
        #region Fields

        private static readonly LoggingConfiguration config = new LoggingConfiguration();

        #endregion

        #region Unit tests

        internal static MemoryTarget AssignMemoryTarget(string name, string layout, NLog.LogLevel minLogLevel = null)
        {
            var target = new MemoryTarget { Layout = layout };

            lock (config)
            {
                config.AddTarget(name, target);
                config.LoggingRules.Add(new LoggingRule(name, minLogLevel ?? NLog.LogLevel.Debug, target));

                NLog.LogManager.Configuration = config;
            }

            return target;
        }

        #endregion
    }
}
