// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Log
{
    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public interface ILogger
    {
        /// <summary>
        /// Log a message to the logger.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        void Log(LogLevel level, string msg, params object[] args);

        /// <summary>
        /// Log a debug message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        void Debug(string msg, params object[] args);

        /// <summary>
        /// Log an informational message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        void Info(string msg, params object[] args);

        /// <summary>
        /// Log a warning message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        void Warn(string msg, params object[] args);

        /// <summary>
        /// Log an error message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        void Error(string msg, params object[] args);

        /// <summary>
        /// Log a fatal error message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        void Fatal(string msg, params object[] args);
    }

    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public interface ILogger<T> : ILogger
    {

    }

    /// <summary>
    /// Abstract base class for loggers.
    /// </summary>
    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    public abstract class Logger : ILogger
    {
        /// <summary>
        /// Log a message to the logger.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public abstract void Log(LogLevel level, string msg, params object[] args);

        /// <summary>
        /// Log a debug message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Debug(string msg, params object[] args)
        {
            Log(LogLevel.Debug, msg, args);
        }

        /// <summary>
        /// Log an informational message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Info(string msg, params object[] args)
        {
            Log(LogLevel.Info, msg, args);
        }

        /// <summary>
        /// Log a warning message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Warn(string msg, params object[] args)
        {
            Log(LogLevel.Warning, msg, args);
        }

        /// <summary>
        /// Log an error message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Error(string msg, params object[] args)
        {
            Log(LogLevel.Error, msg, args);
        }

        /// <summary>
        /// Log a fatal error message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Fatal(string msg, params object[] args)
        {
            Log(LogLevel.Fatal, msg, args);
        }
    }
}
