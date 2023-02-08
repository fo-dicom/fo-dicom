using System.IO;

namespace FellowOakDicom.Network.Tls
{
    public interface ITlsInitiator
    {
        Stream InitiateTls(Stream plainStream, string remoteAddress, int remotePort);
    }
}
