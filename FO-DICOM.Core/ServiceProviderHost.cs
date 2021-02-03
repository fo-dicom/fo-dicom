// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom
{

    public interface IServiceProviderHost
    {
        IServiceProvider GetServiceProvider();
    }


    public class DefaultServiceProviderHost : IServiceProviderHost
    {

        private readonly IServiceProvider _seriveProvider;

        public DefaultServiceProviderHost(IServiceProvider serviceProvider)
        {
            _seriveProvider = serviceProvider;
        }

        public IServiceProvider GetServiceProvider()
            => _seriveProvider;

    }

}
