using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    public interface IAdvancedDicomClientConnectionFactory
    {
        Task<IAdvancedDicomClientConnection> ConnectAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken);
    }
}