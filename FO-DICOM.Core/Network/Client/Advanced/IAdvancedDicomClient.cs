// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    /// <summary>
    /// Represents an advanced DICOM client that exposes the highest amount of manual control over the underlying DICOM communication when sending DICOM requests<br/>
    /// Using an advanced DICOM client, it is possible to manage the lifetime of a DICOM connection + association in a fine-grained manner and send any number of DICOM requests over it<br/>
    /// <br/>
    /// The regular DICOM client is completely built on top of this advanced DICOM client.<br/>
    /// Be aware that consumers of the advanced DICOM client are left to their own devices to handle the fine details of interacting with other PACS software.<br/>
    /// Here is an incomplete sample of the things the regular DICOM client will do for you:<br/>
    /// - Enforce a maximum amount of requests per association<br/>
    /// - Keep an association alive for a certain amount of time to allow more requests to be sent<br/>
    /// - Automatically open more associations while more requests are enqueued<br/>
    /// - Automatically negotiate presentation contexts based on the requests that are enqueued<br/>
    /// <br/>
    /// This advanced DICOM client is offered to expert users of the Fellow Oak DICOM library.<br/>
    /// If you do not consider yourself such an expert, please reconsider the compatibility of the regular DicomClient with your use case.
    /// </summary>
    public interface IAdvancedDicomClient
    {
        /// <summary>
        /// Opens a new TCP connection to another AE using the parameters provided in the connection <paramref name="request"/>
        /// </summary>
        /// <param name="request">The connection request that specifies the details of the connection that should be opened</param>
        /// <param name="cancellationToken">The token that will cancel the opening of the connection</param>
        /// <returns>A new instance of <see cref="IAdvancedDicomClientConnection"/></returns>
        Task<IAdvancedDicomClientConnection> OpenConnectionAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAdvancedDicomClientAssociation> OpenAssociationAsync(IAdvancedDicomClientConnection connection, AdvancedDicomClientAssociationRequest request, CancellationToken cancellationToken);
    }
}