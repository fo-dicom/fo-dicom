// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Network;

namespace FellowOakDicom.Tests.Helpers
{
    public class ConfigurableDicomCEchoProviderServer : DicomServer<ConfigurableDicomCEchoProvider>
    {
        private readonly DicomServiceDependencies _dicomServiceDependencies;
        private Func<DicomAssociation, Task<bool>> _onAssociationRequest;
        private Func<DicomCEchoRequest, Task> _onRequest;

        public ConfigurableDicomCEchoProviderServer(DicomServerDependencies dicomServerDependencies,
            DicomServiceDependencies dicomServiceDependencies) : base(dicomServerDependencies)
        {
            _dicomServiceDependencies = dicomServiceDependencies ??
                                        throw new ArgumentNullException(nameof(dicomServiceDependencies));
            _onAssociationRequest = _ => Task.FromResult(true);
            _onRequest = _ => Task.FromResult(0);
        }

        public void OnAssociationRequest(Func<DicomAssociation, Task<bool>> onAssociationRequest)
        {
            _onAssociationRequest = onAssociationRequest;
        }

        public void OnRequest(Func<DicomCEchoRequest, Task> onRequest)
        {
            _onRequest = onRequest;
        }

        protected sealed override ConfigurableDicomCEchoProvider CreateScp(INetworkStream stream)
        {
            var provider = new ConfigurableDicomCEchoProvider(stream, Encoding.UTF8, Logger, _dicomServiceDependencies,
                _onAssociationRequest, _onRequest);
            return provider;
        }
    }
}
