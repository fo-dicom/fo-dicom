// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Abstract base class for loggers.
    /// </summary>
    public abstract class Logger
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
            this.Log(LogLevel.Debug, msg, args);
        }

        /// <summary>
        /// Log an informational message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Info(string msg, params object[] args)
        {
            this.Log(LogLevel.Info, msg, args);
        }

        /// <summary>
        /// Log a warning message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Warn(string msg, params object[] args)
        {
            this.Log(LogLevel.Warning, msg, args);
        }

        /// <summary>
        /// Log an error message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Error(string msg, params object[] args)
        {
            this.Log(LogLevel.Error, msg, args);
        }

        /// <summary>
        /// Log a fatal error message to the logger.
        /// </summary>
        /// <param name="msg">Log message (format string).</param>
        /// <param name="args">Log message arguments.</param>
        public void Fatal(string msg, params object[] args)
        {
            this.Log(LogLevel.Fatal, msg, args);
        }

        private static readonly Regex CurlyBracePairRegex = new Regex(@"{.*?}");

        /// <summary>
        /// Called to adapt the string message before passing through
        /// Adapts messages of the form
        /// Beginning parsing for {@file} using widget {widgetName}
        /// To
        /// Beginning parsing for {0} using widget {1}
        /// Required by loggers that expect positional format rather than named format
        /// such as NLog
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static string NameFormatToPositionalFormat(string message)
        {
            var matches = CurlyBracePairRegex.Matches(message).Cast<Match>();

            var handledMatchNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            //Stores the updated message
            var updatedMessage = message;
            var positionDelta = 0;

            //Is every encountered match a number?  If so, we've been given a string already in positional format so it should not be amended
            var everyMatchIsANumber = true; //until proven otherwise

            foreach (var match in matches)
            {
                //Remove the braces
                var matchNameFormattingNoBraces = match.Value.Substring(1, match.Value.Length - 2);

                //Split into the name and the formatting
                var colonIndex = matchNameFormattingNoBraces.IndexOf(':');
                var matchName = colonIndex < 0
                                    ? matchNameFormattingNoBraces
                                    : matchNameFormattingNoBraces.Substring(0, colonIndex);
                var formattingIncludingColon = colonIndex < 0 ? "" : matchNameFormattingNoBraces.Substring(colonIndex);

                everyMatchIsANumber = everyMatchIsANumber && IsNumber(matchName);

                //Remove leading "@" sign (indicates destructuring was desired)
                var destructured = matchName.StartsWith("@");
                if (destructured)
                {
                    matchName = matchName.Substring(1);
                }

                int ordinalOutputPosition;
                //Already seen the match?
                if (!handledMatchNames.ContainsKey(matchName))
                {
                    //first time
                    ordinalOutputPosition = handledMatchNames.Count;
                    handledMatchNames.Add(matchName, ordinalOutputPosition);
                }
                else
                {
                    //resuse previous number
                    ordinalOutputPosition = handledMatchNames[matchName];
                }

                var replacement = "{" + ordinalOutputPosition + formattingIncludingColon + "}";

                //Substitute the new text in place in the message
                updatedMessage = updatedMessage.Substring(0, match.Index + positionDelta) + replacement
                                 + updatedMessage.Substring(match.Index + match.Length + positionDelta);
                //Update positionDelta to account for differing lengths of substitution
                positionDelta = positionDelta + (replacement.Length - match.Length);
            }


            if (everyMatchIsANumber)
            {
                return message;
            }

            return updatedMessage;
        }

        /// <summary>
        /// Checks whether string represents an integer value.
        /// </summary>
        /// <param name="s">String potentially containing integer value.</param>
        /// <returns>True if <paramref name="s"/> could be interpreted as integer value, false otherwise.</returns>
        internal static bool IsNumber(string s)
        {
            int dummy;
            return int.TryParse(s, out dummy);
        }
    }
}
