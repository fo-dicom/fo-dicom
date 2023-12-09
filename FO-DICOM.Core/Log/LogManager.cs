// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Log
{
    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public interface ILogManager
    {
        /// <summary>
        /// Get logger.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>A logger</returns>
        ILogger GetLogger(string name);
    }

    /// <summary>
    /// Main class for logging management.
    /// </summary>
    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public abstract class LogManager : ILogManager
    {
        #region METHODS

        /// <summary>
        /// Get logger from the current log manager implementation.
        /// </summary>
        /// <param name="name">Classifier name, typically namespace or type name.</param>
        /// <returns>Logger from the current log manager implementation.</returns>
        protected abstract Logger GetLoggerImpl(string name);

        ILogger ILogManager.GetLogger(string name) => GetLoggerImpl(name);

        #endregion
    }
}