// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class GH859
    {
        private readonly XUnitDicomLogger _output;

        public GH859(ITestOutputHelper output)
        {
            _output = new XUnitDicomLogger(output)
                .IncludeTimestamps()
                .IncludeThreadId()
                .IncludePrefix("GH859");
        }

        [Fact]
        public async Task DicomService_reading_messages_with_invalid_UIDs_does_not_fail()
        {
            int port = Ports.GetNext();
            var clientLogger = _output.IncludePrefix(nameof(DicomClient));
            var serverLogger = _output.IncludePrefix(nameof(DicomCEchoProvider));
            var source = new CancellationTokenSource();

            using var server = DicomServerFactory.Create<SimpleCStoreProvider>(port, logger: serverLogger);
            server.Options.LogDataPDUs = true;
            server.Options.LogDimseDatasets = true;

            while (!server.IsListening)
            {
                await Task.Delay(50);
            }

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.Logger = clientLogger;

            var command = new DicomDataset
            {
                ValidateItems = false
            };
            command.Add(DicomTag.CommandField, (ushort)DicomCommandField.CStoreRequest);
            command.Add(DicomTag.MessageID, (ushort)1);
            command.Add(DicomTag.AffectedSOPClassUID, DicomUID.CTImageStorage);
            command.Add(new DicomUniqueIdentifier(DicomTag.AffectedSOPInstanceUID, "1.2.3.04"));

            var request = new DicomCStoreRequest(command)
            {
                File = new DicomFile(),
                Dataset = new DicomDataset()
            };
            request.Dataset.ValidateItems = false;
            request.Dataset.Add(DicomTag.SOPClassUID, DicomUID.CTImageStorage);
            request.Dataset.Add(new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3.04"));

            request.OnResponseReceived += (e, args) =>
            {
                _output.LogInformation("Response received. Cancelling in 500ms.");
                source.CancelAfter(100);
            };

            await client.AddRequestAsync(request);

            await client.SendAsync();
        }

    }
}
