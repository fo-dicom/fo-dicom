using FellowOakDicom.Network.Client.Advanced.Association;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    /// <summary>
    /// Represents an advanced DICOM client that exposes a lot of manual control of the underlying DICOM communication when sending DICOM requests
    /// </summary>
    public interface IAdvancedDicomClient
    {
        Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(AdvancedDicomClientAssociationRequest request, CancellationToken cancellationToken);
    }
}