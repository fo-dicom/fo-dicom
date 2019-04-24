using System.Linq;

namespace Dicom.Network.Client
{
    public interface IInitialisationWithAssociationParameters : IInitialisationWithConnectionParameters
    {
        DicomAssociation Association { get; set; }
    }
}
