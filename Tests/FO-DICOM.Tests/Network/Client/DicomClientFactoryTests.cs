// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FellowOakDicom.Tests.Network.Client
{
    public class DicomClientFactoryTests
    {
        private readonly IDicomClientFactory _factory;
        private readonly string _host;
        private readonly int _port;
        private readonly bool _useTls;
        private readonly string _callingAe;
        private readonly string _calledAe;

        public DicomClientFactoryTests()
        {
            _factory = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>();
            _host = "127.0.0.1";
            _port = 104;
            _useTls = false;
            _callingAe = "ANY-SCU";
            _calledAe = "ANY-SCP";
        }

        [Fact]
        public void ShouldThrowWhenHostIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _factory.Create(null, _port, _useTls, _callingAe, _calledAe));
        }

        [Fact]
        public void ShouldThrowWhenCallingAeTitleIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => _factory.Create(_host, _port, _useTls, "SuperDuperLongAeTitle", _calledAe));
        }

        [Fact]
        public void ShouldThrowWhenCalledAeTitleIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => _factory.Create(_host, _port, _useTls, _callingAe, "SuperDuperLongAeTitle"));
        }
    }
}
