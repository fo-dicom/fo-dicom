using System.IO;

namespace FellowOakDicom.Network.Tls
{
    public interface ITlsAcceptor
    {
        Stream AcceptTls(Stream encryptedStream, string remoteAddress, int localPort);
    }
}
