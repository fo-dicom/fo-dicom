// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomCGetRequestTest : IClassFixture<GlobalFixture>
    {

        #region Unit tests

        [Fact(Skip = "Require running Q/R SCP containing CT-MONO2-16-ankle image")]
        public async Task DicomCGetRequest_OneImageInSeries_Received()
        {
            var client = DicomClientFactory.Create("localhost", 11112, false, "SCU", "COMMON");

            var pcs = DicomPresentationContext.GetScpRolePresentationContextsFromStorageUids(
                DicomStorageCategory.Image,
                DicomTransferSyntax.ExplicitVRLittleEndian,
                DicomTransferSyntax.ImplicitVRLittleEndian,
                DicomTransferSyntax.ImplicitVRBigEndian);
            client.AdditionalPresentationContexts.AddRange(pcs);

            DicomDataset dataset = null;
            client.OnCStoreRequest = request =>
                {
                    dataset = request.Dataset;
                    return Task.FromResult( new DicomCStoreResponse(request, DicomStatus.Success));
                };

            var get = new DicomCGetRequest(
                "1.2.840.113619.2.1.1.322987881.621.736170080.681",
                "1.2.840.113619.2.1.2411.1031152382.365.736169244");

            var handle = new ManualResetEventSlim();
            get.OnResponseReceived = (request, response) =>
            {
                handle.Set();
            };
            await client.AddRequestAsync(get);
            await client.SendAsync();
            handle.Wait();

            Assert.Equal("RT ANKLE", dataset.GetString(DicomTag.StudyDescription));
        }


        [Fact(Skip = "Require running Q/R SCP containing specific study")]
        public async Task DicomCGetRequest_PickCTImagesInStudy_OnlyCTImagesRetrieved()
        {
            var client = DicomClientFactory.Create("localhost", 11112, false, "SCU", "COMMON");

            var pc = DicomPresentationContext.GetScpRolePresentationContext(DicomUID.CTImageStorage);
            client.AdditionalPresentationContexts.Add(pc);

            var counter = 0;
            var locker = new object();
            client.OnCStoreRequest = request =>
            {
                lock (locker)
                {
                    ++counter;
                }

                return Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success));
            };

            var get = new DicomCGetRequest("1.2.840.113619.2.55.3.2609388324.145.1222836278.84");

            var handle = new ManualResetEventSlim();
            get.OnResponseReceived = (request, response) =>
            {
                if (response.Remaining == 0)
                {
                    handle.Set();
                }
            };
            await client.AddRequestAsync(get);
            await client.SendAsync();
            handle.Wait();

            Assert.Equal(140, counter);
        }

        [Fact]
        public void Level_GetterOnRequestCreatedFromCommand_Throws()
        {
            var request = new DicomCGetRequest(new DicomDataset());
            var exception = Record.Exception(() => request.Level);
            Assert.NotNull(exception);
        }

        [Theory, MemberData(nameof(InstancesLevels))]
        public void Level_Getter_ReturnsCorrectQueryRetrieveLevel(DicomCGetRequest request, DicomQueryRetrieveLevel expected)
        {
            var actual = request.Level;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateQueryWithInvalidUID()
        {
            var invalidStudyUID = "1.2.0004";
            var e = Record.Exception(() =>
            {
                var request = new DicomCGetRequest(invalidStudyUID, DicomPriority.Medium);
                Assert.Equal(invalidStudyUID, request.Dataset.GetSingleValue<string>(DicomTag.StudyInstanceUID));
            });
            Assert.Null(e);
        }

        [Fact]
        public void AddInvalidUIDToQuery()
        {
            var invalidStudyUID = "1.2.0004";
            var e = Record.Exception(() =>
            {
                var request = new DicomCGetRequest(invalidStudyUID, DicomPriority.Medium);
                request.Dataset.AddOrUpdate(DicomTag.SeriesInstanceUID, invalidStudyUID);
                Assert.Equal(invalidStudyUID, request.Dataset.GetSingleValue<string>(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);
        }

        [Fact]
        public void AddSeveralUIDsToQuery()
        {
            var e = Record.Exception(() =>
            {
                var request = new DicomCGetRequest("1.2.3.456");
                request.Dataset.Add(DicomTag.SeriesInstanceUID, "1.2.3\\3.4.5");
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);

            e = Record.Exception(() =>
            {
                var request = new DicomCGetRequest("1.2.3.456");
                request.Dataset.Add(DicomTag.SeriesInstanceUID, "1.2.3", "2.3.4");
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);

            e = Record.Exception(() =>
            {
                var request = new DicomCGetRequest("1.2.3.456");
                request.Dataset.Add(new DicomUniqueIdentifier(DicomTag.SeriesInstanceUID, "1.2.3", "3.4.5"));
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);
        }

        #endregion

        #region Support Data

        public static readonly IEnumerable<object[]> InstancesLevels = new[]
        {
            new object[] { new DicomCGetRequest("1.2.3"), DicomQueryRetrieveLevel.Study },
            new object[] { new DicomCGetRequest("1.2.3", "2.3.4"), DicomQueryRetrieveLevel.Series },
            new object[] { new DicomCGetRequest("1.2.3", "2.3.4", "3.4.5"), DicomQueryRetrieveLevel.Image },
        };


        #endregion
    }
}
