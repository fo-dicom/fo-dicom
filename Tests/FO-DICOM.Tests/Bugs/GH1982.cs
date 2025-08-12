using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH1982
    {
        [Fact]
        public void ParsingInvlidFileShouldThrow()
        {
            var filename = TestData.Resolve("GH1982.dcm");
            var ex = Record.Exception(() =>
            {
                var dcmFile = DicomFile.Open(filename);
            });
            Assert.NotNull(ex);
            Assert.IsType<DicomFileException>(ex);
            Assert.IsType<DicomDataException>(ex.InnerException);
            // the exception has to show the tag, where the exception had happened
            Assert.Equal(DicomTag.PersonNamesToUseSequence, (ex.InnerException as DicomDataException).Tag);
        }


    }
}
