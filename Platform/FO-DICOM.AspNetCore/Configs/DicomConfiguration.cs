using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;

namespace FellowOakDicom.AspNetCore.Configs
{
    public class DicomConfiguration
    {
        public static readonly string SectionName = "FellowOakDicom";

        public ServerConfiguration Server { get; } = new ServerConfiguration();

        public DicomServerOptions ServerOptions { get; } = new DicomServerOptions();

        public DicomServiceOptions ServiceOptions { get; } = new DicomServiceOptions();

        public DicomClientOptions ClientOptions { get; } = new DicomClientOptions();

    }
}
