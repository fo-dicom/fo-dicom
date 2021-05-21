using System;

namespace FellowOakDicom.Network.Client.Advanced
{
    public class AdvancedDicomClientOptions
    {
        /// <summary>
        /// Gets or sets the timeout (in ms) that associations need to be held open after all requests have been processed
        /// </summary>
        public TimeSpan AssociationRequestTimeout { get; set; } = TimeSpan.FromMilliseconds(5000);

        /// <summary>
        /// Gets or sets the timeout (in ms) to wait for an association response after sending an association request
        /// </summary>
        public TimeSpan AssociationReleaseTimeout { get; set; } = TimeSpan.FromMilliseconds(10000);

        public AdvancedDicomClientOptions Clone() => new AdvancedDicomClientOptions {AssociationRequestTimeout = AssociationRequestTimeout, AssociationReleaseTimeout = AssociationReleaseTimeout};
    }
}