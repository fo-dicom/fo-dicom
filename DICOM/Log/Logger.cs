using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dicom.Log {
	public abstract class Logger {
		public abstract void Log(LogLevel level, string msg, params object[] args);

		public void Debug(string msg, params object[] args) {
			Log(LogLevel.Debug, msg, args);
		}

		public void Info(string msg, params object[] args) {
			Log(LogLevel.Info, msg, args);
		}

		public void Warn(string msg, params object[] args) {
			Log(LogLevel.Warning, msg, args);
		}

		public void Error(string msg, params object[] args) {
			Log(LogLevel.Error, msg, args);
		}

		public void Fatal(string msg, params object[] args) {
			Log(LogLevel.Fatal, msg, args);
		}


        private static readonly Regex _curlyBracePairRegex = new Regex(@"{.*?}");

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
        protected static string NameFormatToPositionalFormat(string message) {

            var matches = _curlyBracePairRegex.Matches(message).Cast<Match>();

            var handledMatchNames = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

            //Stores the updated message
            var updatedMessage = message;
            var positionDelta = 0;

            //Is every encountered match a number?  If so, we've been given a string already in positional format so it should not be amended
            bool everyMatchIsANumber = true;  //until proven otherwise
            int dummyParseOutput;

            foreach (var match in matches)
            {
                //Remove the braces
                var matchNameFormattingNoBraces = match.Value.Substring(1, match.Value.Length - 2);

                //Split into the name and the formatting
                var colonIndex = matchNameFormattingNoBraces.IndexOf(':');
                var matchName = colonIndex < 0 ? matchNameFormattingNoBraces : matchNameFormattingNoBraces.Substring(0, colonIndex);
                var formattingIncludingColon = colonIndex < 0 ? "" : matchNameFormattingNoBraces.Substring(colonIndex);

                everyMatchIsANumber = everyMatchIsANumber && int.TryParse(matchName, out dummyParseOutput);

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
                updatedMessage = updatedMessage.Substring(0, match.Index + positionDelta) + replacement + updatedMessage.Substring(match.Index + match.Length + positionDelta);
                //Update positionDelta to account for differing lengths of substitution
                positionDelta = positionDelta + (replacement.Length - match.Length);
            }


            if (everyMatchIsANumber) {
                return message;
            }

            return updatedMessage;

        }
	}
}
