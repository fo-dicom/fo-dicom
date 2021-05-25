using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    /// <summary>
    /// Represents an advanced DICOM client that exposes a lot of manual control of the underlying DICOM communication when sending DICOM requests
    /// </summary>
    public interface IAdvancedDicomClient
    {
        Task<IAdvancedDicomClientConnection> OpenConnectionAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken);
        Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(IAdvancedDicomClientConnection connection, AdvancedDicomClientAssociationRequest request, CancellationToken cancellationToken);
    }
}