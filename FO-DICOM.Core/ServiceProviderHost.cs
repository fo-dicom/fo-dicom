// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

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
