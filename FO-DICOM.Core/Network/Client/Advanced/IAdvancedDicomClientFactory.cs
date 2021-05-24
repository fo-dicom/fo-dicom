namespace FellowOakDicom.Network.Client.Advanced
{
    public interface IAdvancedDicomClientFactory
    {
        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        IAdvancedDicomClient Create(AdvancedDicomClientCreationRequest request);
    }
}