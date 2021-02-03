// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FellowOakDicom.Tests.Log
{
    public class LogManagerTest
    {
        [Fact]
        public void GetNLogManagerInstanceTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogManager<NLogManager>();

            var provider = services.BuildServiceProvider();

            ILogManager logManager = provider.GetRequiredService<ILogManager>();

            Assert.IsAssignableFrom<NLogManager>(logManager);
        }
    }
}
