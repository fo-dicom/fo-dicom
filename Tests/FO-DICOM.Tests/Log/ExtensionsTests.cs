using System;
using System.Linq;
using FellowOakDicom.Log;
using Xunit;
using Xunit.Abstractions;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace FellowOakDicom.Tests.Log
{
    public class ExtensionsTests
    {
        private readonly ITestOutputHelper _output;

        public ExtensionsTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void WriteToLog_ShouldWriteCorrectly()
        {
            // Arrange
            var dataset = new DicomDataset
            {
                { DicomTag.AccessionNumber, "Accession" },
                new DicomSequence(DicomTag.OtherPatientIDsSequence, new DicomDataset[]
                {
                    new DicomDataset
                    {
                        { DicomTag.TypeOfPatientID, "NATIONALNUMBER" },
                        { DicomTag.PatientID, "202301017484" },
                    },
                    new DicomDataset
                    {
                        { DicomTag.TypeOfPatientID, "INTERNAL" },
                        { DicomTag.PatientID, "1234" },
                    },
                })
            };
            var logger = new CollectingLogger();
            logger.StartCollecting();

            // Act
            dataset.WriteToLog(logger, LogLevel.Information);
            logger.StopCollecting();

            // Assert
            var expected = @"
Information (0008,0050) SH [Accession]                                      #    10, AccessionNumber
Information (0010,1002) SQ Other Patient IDs Sequence
Information   Item:
Information     > (0010,0020) LO [202301017484]                             #    12, PatientID
Information     > (0010,0022) CS [NATIONALNUMBER]                           #    14, TypeOfPatientID
Information   Item:
Information     > (0010,0020) LO [1234]                                     #     4, PatientID
Information     > (0010,0022) CS [INTERNAL]                                 #     8, TypeOfPatientID
            ".Trim();
            var actual = string.Join(Environment.NewLine, logger.LogEntries.Select(entry => $"{entry.Item1} {entry.Item2}"));
            _output.WriteLine(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteToString_ShouldWriteCorrectly()
        {
            // Arrange
            var dataset = new DicomDataset
            {
                { DicomTag.AccessionNumber, "Accession" },
                new DicomSequence(DicomTag.OtherPatientIDsSequence, new DicomDataset[]
                {
                    new DicomDataset
                    {
                        { DicomTag.TypeOfPatientID, "NATIONALNUMBER" },
                        { DicomTag.PatientID, "202301017484" },
                    },
                    new DicomDataset
                    {
                        { DicomTag.TypeOfPatientID, "INTERNAL" },
                        { DicomTag.PatientID, "1234" },
                    },
                })
            };

            // Act
            var actual = dataset.WriteToString();

            // Assert
            var expected = @"
(0008,0050) SH [Accession]                                                        #    10, AccessionNumber
(0010,1002) SQ Other Patient IDs Sequence
  Item:
    > (0010,0020) LO [202301017484]                                               #    12, PatientID
    > (0010,0022) CS [NATIONALNUMBER]                                             #    14, TypeOfPatientID
  Item:
    > (0010,0020) LO [1234]                                                       #     4, PatientID
    > (0010,0022) CS [INTERNAL]                                                   #     8, TypeOfPatientID
            ".Trim() + Environment.NewLine;
            _output.WriteLine(actual);
            Assert.Equal(expected, actual);
        }
    }
}
