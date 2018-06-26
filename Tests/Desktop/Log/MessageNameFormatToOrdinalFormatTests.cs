// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using Xunit;

namespace Dicom.Log
{
    [Collection("General")]
    public class MessageNameFormatToOrdinalFormatTests
    {
        [Fact]
        public void ComplexMessageIsCorrectlyReformatted()
        {
            var complexMessage =
                "The widget {@widget} for file {filename} was opened by {@user} that owns the {widget:'format'} which was opened by {user:'fff'} at {today}";

            var expectedReformattedMessage =
                "The widget {0} for file {1} was opened by {2} that owns the {0:'format'} which was opened by {2:'fff'} at {3}";


            var dummyLogger = new ExposesProtectedLogMessageReformatter();
            var actualReformattedMessage = dummyLogger.NameFormatToPositionalFormat(complexMessage);

            Assert.Equal(expectedReformattedMessage, actualReformattedMessage);
        }

        [Fact]
        public void MessageUsingClassicOrdinalPositionsFormatsToItself()
        {
            var message =
                "The element at {0} using {1} and {2} was causing grief because {2} and {1} dislike {0} except {1} secretly likes {2}";

            var dummyLogger = new ExposesProtectedLogMessageReformatter();
            Assert.Equal(message, dummyLogger.NameFormatToPositionalFormat(message));

        }

        [Fact]
        public void MessageUsingOrdinalPositionsOutOfOrderDoesNotRearrangeTheOrder()
        {
            var message = "The elements in reverse {2}, {1}, {0} are out of order";

            var dummyLogger = new ExposesProtectedLogMessageReformatter();
            var actualReformattedMessage = dummyLogger.NameFormatToPositionalFormat(message);

            Assert.Equal(message, actualReformattedMessage);
        }

        [Fact]
        public void MessageUsingMixedOrdinalThenNamedPositionsIsHandled()
        {
            var message =
                "This message mixes some arbitrary positions like {0} and names like {name}, {@file} and could reuse positions {0:f}";
            var expectedMessage =
                "This message mixes some arbitrary positions like {0} and names like {1}, {2} and could reuse positions {0:f}";

            var dummyLogger = new ExposesProtectedLogMessageReformatter();
            var actualReformattedMessage = dummyLogger.NameFormatToPositionalFormat(message);

            Assert.Equal(expectedMessage, actualReformattedMessage);
        }

        [Fact]
        public void MessageUsingMixedOrdinalAndNamedPositionsIsHandled()
        {
            var message =
                "This message mixes names {name}, then some arbitrary positions like {0} and names like {name}, {@file} and could reuse positions {0:f}";
            var expected =
                "This message mixes names {0}, then some arbitrary positions like {1} and names like {0}, {2} and could reuse positions {1:f}";

            var dummyLogger = new ExposesProtectedLogMessageReformatter();
            var actualReformattedMessage = dummyLogger.NameFormatToPositionalFormat(message);

            Assert.Equal(expected, actualReformattedMessage);
        }

        /// <summary>
        /// This is present so we can get access to the protected static method
        /// </summary>
        private class ExposesProtectedLogMessageReformatter : Logger
        {
            public override void Log(LogLevel level, string msg, params object[] args)
            {
                //Not used
                throw new NotImplementedException();
            }

            /// <summary>
            /// Exposes the protected static method
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            public new string NameFormatToPositionalFormat(string message)
            {
                return Logger.NameFormatToPositionalFormat(message);
            }
        }
    }

}
