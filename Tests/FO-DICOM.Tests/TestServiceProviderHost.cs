// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests
{
    public class TestServiceProviderHost : IServiceProviderHost
    {
        private readonly Dictionary<string, IServiceProvider> _serviceProviders;

        public TestServiceProviderHost(IServiceProvider serviceProvider)
        {
            _serviceProviders = new Dictionary<string, IServiceProvider>() { { string.Empty, serviceProvider } };
        }


        public void Register(string category, IServiceProvider serviceProvider)
            => _serviceProviders.Add(category, serviceProvider);


        public IServiceProvider GetServiceProvider()
        {
            var category = string.Empty;

            var st = new StackTrace(1);
            var frames = st.GetFrames();
            var rootFrame = frames.LastOrDefault(f => f.GetMethod()?.DeclaringType?.Namespace?.StartsWith("FellowOakDicom") == true);

            var declType = rootFrame.GetMethod().DeclaringType;
            if (declType.IsNested)
            {
                declType = declType.DeclaringType;
            }
            category = declType.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(CollectionAttribute))?.ConstructorArguments.FirstOrDefault().Value.ToString() ?? string.Empty;

            if (!_serviceProviders.ContainsKey(category))
            {
                category = string.Empty;
            }

            return _serviceProviders[category];
        }

    }
}
