// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomUserIdentityNegotiationTest
    {
        #region Fields

        private readonly XUnitDicomLogger _logger;

        #endregion

        #region Constructors

        public DicomUserIdentityNegotiationTest(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        #endregion

        #region Helper functions

        private IDicomServer CreateServer<T>(int port) where T : DicomService, IDicomServiceProvider
        {
            var server = DicomServerFactory.Create<T>(port);
            server.Logger = _logger.IncludePrefix(nameof(IDicomServer));
            return server;
        }

        #endregion

        [Fact]
        public void DicomUserIdentityNegotiation_ShouldInstantiateAndClone()
        {
            var expectedUserIdentityType = DicomUserIdentityType.Kerberos;
            var expectedPositiveResponseRequested = true;
            var expectedPrimaryField = DicomUserIdentityNegotiationTestData.Username;
            var expectedSecondaryField = DicomUserIdentityNegotiationTestData.Passcode;
            var expectedServerResponse = DicomUserIdentityNegotiationTestData.KerberosServerResponse;

            var userIdentity = new DicomUserIdentityNegotiation
            {
                UserIdentityType = expectedUserIdentityType,
                PositiveResponseRequested = expectedPositiveResponseRequested,
                PrimaryField = expectedPrimaryField,
                SecondaryField = expectedSecondaryField,
                ServerResponse = expectedServerResponse
            };

            var clonedUserIdentity = userIdentity.Clone();

            Assert.Equal(clonedUserIdentity.UserIdentityType, expectedUserIdentityType);
            Assert.Equal(clonedUserIdentity.PositiveResponseRequested, expectedPositiveResponseRequested);
            Assert.Equal(clonedUserIdentity.PrimaryField, expectedPrimaryField);
            Assert.Equal(clonedUserIdentity.SecondaryField, expectedSecondaryField);
            Assert.Equal(clonedUserIdentity.ServerResponse, expectedServerResponse);
        }

        [Fact]
        public void DicomUserIdentityNegotiation_ShouldCorrectlyValidate()
        {
            ArgumentNullException nullUserIdentityTypeException = null;
            try
            {
                var userIdentity = new DicomUserIdentityNegotiation();
                userIdentity.Validate();
            }
            catch (ArgumentNullException e)
            {
                nullUserIdentityTypeException = e;
            }

            ArgumentException populatedSecondaryFieldException = null;
            try
            {
                var userIdentity = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Username,
                    PrimaryField = DicomUserIdentityNegotiationTestData.Username,
                    SecondaryField = DicomUserIdentityNegotiationTestData.Passcode
                };

                userIdentity.Validate();
            }
            catch (ArgumentException e)
            {
                populatedSecondaryFieldException = e;
            }

            ArgumentException validException = null;
            try
            {
                var userIdentity = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Jwt,
                    PrimaryField = DicomUserIdentityNegotiationTestData.JwtToken
                };

                userIdentity.Validate();
            }
            catch (ArgumentException e)
            {
                validException = e;
            }

            Assert.NotNull(nullUserIdentityTypeException);
            Assert.NotNull(populatedSecondaryFieldException);
            Assert.Null(validException);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_ThrowsForNonValidUserIdentityNegotiation()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Jwt,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.JwtToken,
                    SecondaryField = "SECONDARY_FIELD"
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            ArgumentException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);
            }
            catch (ArgumentException e)
            {
                exception = e;
            }

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_ThrowsRejectionForEmptyUserIdentity()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP"
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DicomClientFactory_OpenAssociation_ThrowsForNonValidUserIdentityNegotiation()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            ArgumentException exception = null;
            try
            {
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "SCP");
                client.Logger = _logger.IncludePrefix("Client");

                client.NegotiateUserIdentity(new DicomUserIdentityNegotiation());
                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();
            }
            catch (ArgumentException e)
            {
                exception = e;
            }

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DicomClientFactory_OpenAssociation_ThrowsForNonSuccessfulUserIdentityNegotiation()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockUserIdentityUnawareCEchoProvider>(port);

            DicomNetworkException nonSuccessfulException = null;
            try
            {
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "SCP");
                client.Logger = _logger.IncludePrefix("Client");

                client.RequireSuccessfulUserIdentityNegotiation = true;
                client.NegotiateUserIdentity(new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Kerberos,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.KerberosServiceTicket
                });
                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();
            }
            catch (DicomNetworkException e)
            {
                nonSuccessfulException = e;
            }

            DicomNetworkException successfulExceptionPositiveResponseRequested = null;
            try
            {
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "SCP");
                client.Logger = _logger.IncludePrefix("Client");

                client.RequireSuccessfulUserIdentityNegotiation = true;
                client.NegotiateUserIdentity(new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Kerberos,
                    PositiveResponseRequested = false,
                    PrimaryField = DicomUserIdentityNegotiationTestData.KerberosServiceTicket
                });
                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();
            }
            catch (DicomNetworkException e)
            {
                successfulExceptionPositiveResponseRequested = e;
            }

            DicomNetworkException successfulExceptionRequireSuccessfulUserIdentityNegotiation = null;
            try
            {
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "SCP");
                client.Logger = _logger.IncludePrefix("Client");

                client.RequireSuccessfulUserIdentityNegotiation = false;
                client.NegotiateUserIdentity(new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Kerberos,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.KerberosServiceTicket
                });
                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();
            }
            catch (DicomNetworkException e)
            {
                successfulExceptionRequireSuccessfulUserIdentityNegotiation = e;
            }

            Assert.NotNull(nonSuccessfulException);
            Assert.Null(successfulExceptionPositiveResponseRequested);
            Assert.Null(successfulExceptionRequireSuccessfulUserIdentityNegotiation);
        }

        [Fact]
        public async Task DicomClientFactory_OpenAssociation_ThrowsRejectionForEmptyUserIdentity()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            DicomAssociationRejectedException exception = null;
            try
            {
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "SCP");
                client.Logger = _logger.IncludePrefix("Client");

                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_ThrowsRejectionForNegativeUserIdentityResponseRequest()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Username,
                    PositiveResponseRequested = false
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DicomClientFactory_OpenAssociation_AcceptsAssociationForUserIdentityWithUsername()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            DicomAssociationRejectedException exception = null;
            DicomAssociation association = null;
            try
            {
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "SCP");
                client.Logger = _logger.IncludePrefix("Client");
                client.NegotiateUserIdentity(new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Username,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.Username
                });

                await client.AddRequestAsync(new DicomCEchoRequest());
                client.AssociationAccepted += (sender, args) =>
                {
                    association = args.Association;
                };
                await client.SendAsync();
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);

            Assert.NotNull(association);
            Assert.NotNull(association.UserIdentityNegotiation);
            Assert.Equal(DicomUserIdentityType.Username, association.UserIdentityNegotiation.UserIdentityType);
            Assert.Empty(association.UserIdentityNegotiation.ServerResponse);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_AcceptsAssociationForUserIdentityWithUsername()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Username,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.Username
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);

                Assert.NotNull(association);
                Assert.NotNull(association.Association);
                Assert.NotNull(association.Association.UserIdentityNegotiation);
                Assert.Equal(DicomUserIdentityType.Username, association.Association.UserIdentityNegotiation.UserIdentityType);
                Assert.Empty(association.Association.UserIdentityNegotiation.ServerResponse);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_AcceptsAssociationForUserIdentityWithUsernameAndPasscode()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.UsernameAndPasscode,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.Username,
                    SecondaryField = DicomUserIdentityNegotiationTestData.Passcode,
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);

                Assert.NotNull(association);
                Assert.NotNull(association.Association);
                Assert.NotNull(association.Association.UserIdentityNegotiation);
                Assert.Equal(DicomUserIdentityType.UsernameAndPasscode, association.Association.UserIdentityNegotiation.UserIdentityType);
                Assert.Empty(association.Association.UserIdentityNegotiation.ServerResponse);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_AcceptsAssociationForUserIdentityWithKerberos()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Kerberos,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.KerberosServiceTicket,
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);

                Assert.NotNull(association);
                Assert.NotNull(association.Association);
                Assert.NotNull(association.Association.UserIdentityNegotiation);
                Assert.Equal(DicomUserIdentityType.Kerberos, association.Association.UserIdentityNegotiation.UserIdentityType);
                Assert.Equal(DicomUserIdentityNegotiationTestData.KerberosServerResponse, association.Association.UserIdentityNegotiation.ServerResponse);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_AcceptsAssociationForUserIdentityWithSaml()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Saml,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.SamlAssertion,
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);

                Assert.NotNull(association);
                Assert.NotNull(association.Association);
                Assert.NotNull(association.Association.UserIdentityNegotiation);
                Assert.Equal(DicomUserIdentityType.Saml, association.Association.UserIdentityNegotiation.UserIdentityType);
                Assert.Equal(DicomUserIdentityNegotiationTestData.SamlServerResponse, association.Association.UserIdentityNegotiation.ServerResponse);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task AdvancedDicomClient_OpenAssociation_AcceptsAssociationForUserIdentityWithJwt()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client")
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Jwt,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.JwtToken,
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);

                Assert.NotNull(association);
                Assert.NotNull(association.Association);
                Assert.NotNull(association.Association.UserIdentityNegotiation);
                Assert.Equal(DicomUserIdentityType.Jwt, association.Association.UserIdentityNegotiation.UserIdentityType);
                Assert.Equal(DicomUserIdentityNegotiationTestData.JwtServerResponse, association.Association.UserIdentityNegotiation.ServerResponse);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task AdvancedDicomClient_C_ECHO_ReturnsResponseForUserIdentity()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "SCP",
                UserIdentityNegotiation = new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Jwt,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.JwtToken,
                }
            };

            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);

            DicomAssociationRejectedException exception = null;
            try
            {
                using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
                var cEchoResponse = await association.SendCEchoRequestAsync(cEchoRequest, CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);

                Assert.NotNull(cEchoResponse);
                Assert.Equal(DicomState.Success, cEchoResponse.Status.State);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task DicomClientFactory_C_ECHO_ReturnsResponseForUserIdentity()
        {
            var port = Ports.GetNext();
            using var server = CreateServer<MockMandatoryUserIdentityCEchoProvider>(port);

            DicomAssociationRejectedException exception = null;
            DicomCEchoResponse echoResponse = null;
            try
            {
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "SCP");
                client.Logger = _logger.IncludePrefix("Client");
                client.NegotiateUserIdentity(new DicomUserIdentityNegotiation
                {
                    UserIdentityType = DicomUserIdentityType.Jwt,
                    PositiveResponseRequested = true,
                    PrimaryField = DicomUserIdentityNegotiationTestData.JwtToken
                });

                var echoRequest = new DicomCEchoRequest();
                echoRequest.OnResponseReceived += (request, response) =>
                {
                    echoResponse = response;
                };

                await client.AddRequestAsync(echoRequest);
                await client.SendAsync();
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }

            Assert.Null(exception);

            Assert.NotNull(echoResponse);
            Assert.Equal(DicomState.Success, echoResponse.Status.State);
        }

        public class DicomUserIdentityNegotiationTestData
        {
            #region Username/Passcode

            public static readonly string Username = "USERNAME";
            public static readonly string Passcode = "PASSWORD";

            #endregion

            #region Kerberos

            public static readonly string KerberosServiceTicket =
                @"Client: user @ DOMAIN.COM
                Server: HTTP/proxy.domain.com @ DOMAIN.COM
                KerbTicket Encryption Type: AES-256-CTS-HMAC-SHA1-96
                Ticket Flags 0x40a10000 -> forwardable renewable pre_authent name_canonicalize
                Start Time: 4/11/2023 11:30:13 (local)
                End Time:   4/11/2023 20:57:31 (local)
                Renew Time: 4/18/2023 10:57:31 (local)
                Session Key Type: AES-256-CTS-HMAC-SHA1-96
                Cache Flags: 0
                Kdc Called: DC1.DOMAIN.COM";
            public static readonly string KerberosServerResponse = "KERBEROS_SERVER_RESPONSE";

            #endregion

            #region SAML Assertion

            public static readonly string SamlAssertion =
                @"<samlp:AuthnRequest xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:saml=""urn:oasis:names:tc:SAML:2.0:assertion"" ID=""pfx41d8ef22-e612-8c50-9960-1b16f15741b3"" Version=""2.0"" ProviderName=""SP test"" IssueInstant=""2014-07-16T23:52:45Z"" Destination=""http://idp.example.com/SSOService.php"" ProtocolBinding=""urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST"" AssertionConsumerServiceURL=""http://sp.example.com/demo1/index.php?acs"">
                  <saml:Issuer>http://sp.example.com/demo1/metadata.php</saml:Issuer>
                  <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
                    <ds:SignedInfo>
                      <ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
                      <ds:SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1""/>
                      <ds:Reference URI=""#pfx41d8ef22-e612-8c50-9960-1b16f15741b3"">
                        <ds:Transforms>
                          <ds:Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature""/>
                          <ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
                        </ds:Transforms>
                        <ds:DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1""/>
                        <ds:DigestValue>yJN6cXUwQxTmMEsPesBP2NkqYFI=</ds:DigestValue>
                      </ds:Reference>
                    </ds:SignedInfo>
                    <ds:SignatureValue>g5eM9yPnKsmmE/Kh2qS7nfK8HoF6yHrAdNQxh70kh8pRI4KaNbYNOL9sF8F57Yd+jO6iNga8nnbwhbATKGXIZOJJSugXGAMRyZsj/rqngwTJk5KmujbqouR1SLFsbo7Iuwze933EgefBbAE4JRI7V2aD9YgmB3socPqAi2Qf97E=</ds:SignatureValue>
                    <ds:KeyInfo>
                      <ds:X509Data>
                        <ds:X509Certificate>MIICajCCAdOgAwIBAgIBADANBgkqhkiG9w0BAQQFADBSMQswCQYDVQQGEwJ1czETMBEGA1UECAwKQ2FsaWZvcm5pYTEVMBMGA1UECgwMT25lbG9naW4gSW5jMRcwFQYDVQQDDA5zcC5leGFtcGxlLmNvbTAeFw0xNDA3MTcwMDI5MjdaFw0xNTA3MTcwMDI5MjdaMFIxCzAJBgNVBAYTAnVzMRMwEQYDVQQIDApDYWxpZm9ybmlhMRUwEwYDVQQKDAxPbmVsb2dpbiBJbmMxFzAVBgNVBAMMDnNwLmV4YW1wbGUuY29tMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC7vU/6R/OBA6BKsZH4L2bIQ2cqBO7/aMfPjUPJPSn59d/f0aRqSC58YYrPuQODydUABiCknOn9yV0fEYm4bNvfjroTEd8bDlqo5oAXAUAI8XHPppJNz7pxbhZW0u35q45PJzGM9nCv9bglDQYJLby1ZUdHsSiDIpMbGgf/ZrxqawIDAQABo1AwTjAdBgNVHQ4EFgQU3s2NEpYx7wH6bq7xJFKa46jBDf4wHwYDVR0jBBgwFoAU3s2NEpYx7wH6bq7xJFKa46jBDf4wDAYDVR0TBAUwAwEB/zANBgkqhkiG9w0BAQQFAAOBgQCPsNO2FG+zmk5miXEswAs30E14rBJpe/64FBpM1rPzOleexvMgZlr0/smF3P5TWb7H8Fy5kEiByxMjaQmml/nQx6qgVVzdhaTANpIE1ywEzVJlhdvw4hmRuEKYqTaFMLez0sRL79LUeDxPWw7Mj9FkpRYT+kAGiFomHop1nErV6Q==</ds:X509Certificate>
                      </ds:X509Data>
                    </ds:KeyInfo>
                  </ds:Signature>
                  <samlp:NameIDPolicy Format=""urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress"" AllowCreate=""true""/>
                  <samlp:RequestedAuthnContext Comparison=""exact"">
                    <saml:AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport</saml:AuthnContextClassRef>
                  </samlp:RequestedAuthnContext>
                </samlp:AuthnRequest>";
            public static readonly string SamlServerResponse =
                @"<samlp:Response xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:saml=""urn:oasis:names:tc:SAML:2.0:assertion"" ID=""_8e8dc5f69a98cc4c1ff3427e5ce34606fd672f91e6"" Version=""2.0"" IssueInstant=""2014-07-17T01:01:48Z"" Destination=""http://sp.example.com/demo1/index.php?acs"" InResponseTo=""ONELOGIN_4fee3b046395c4e751011e97f8900b5273d56685"">
                  <saml:Issuer>http://idp.example.com/metadata.php</saml:Issuer>
                  <samlp:Status>
                    <samlp:StatusCode Value=""urn:oasis:names:tc:SAML:2.0:status:Success""/>
                  </samlp:Status>
                  <saml:Assertion xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" ID=""pfx575917c1-56ac-d2ea-d529-2881362281cf"" Version=""2.0"" IssueInstant=""2014-07-17T01:01:48Z"">
                    <saml:Issuer>http://idp.example.com/metadata.php</saml:Issuer><ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
                  <ds:SignedInfo><ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
                    <ds:SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1""/>
                  <ds:Reference URI=""#pfx575917c1-56ac-d2ea-d529-2881362281cf""><ds:Transforms><ds:Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature""/><ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/></ds:Transforms><ds:DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1""/><ds:DigestValue>TsYOfr/FxfGAOefkdjLzhmJwBtc=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>Sst2982MMebR7YzL9f1EFs0mZAlKlItSwwTnuYJAxdJCh3kARVZwlCtzir09uI5oxVs2WcnBtX6VULbF3M5v5RY//TE+JZRbZzZcUHZYCKZb+hvjN8Tg2DFDIPAlZd3G6gaDCzYSbdELvC/t5XwbffGZRC9lFv+RskFvO6RADnk=</ds:SignatureValue>
                <ds:KeyInfo><ds:X509Data><ds:X509Certificate>MIICajCCAdOgAwIBAgIBADANBgkqhkiG9w0BAQ0FADBSMQswCQYDVQQGEwJ1czETMBEGA1UECAwKQ2FsaWZvcm5pYTEVMBMGA1UECgwMT25lbG9naW4gSW5jMRcwFQYDVQQDDA5zcC5leGFtcGxlLmNvbTAeFw0xNDA3MTcxNDEyNTZaFw0xNTA3MTcxNDEyNTZaMFIxCzAJBgNVBAYTAnVzMRMwEQYDVQQIDApDYWxpZm9ybmlhMRUwEwYDVQQKDAxPbmVsb2dpbiBJbmMxFzAVBgNVBAMMDnNwLmV4YW1wbGUuY29tMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDZx+ON4IUoIWxgukTb1tOiX3bMYzYQiwWPUNMp+Fq82xoNogso2bykZG0yiJm5o8zv/sd6pGouayMgkx/2FSOdc36T0jGbCHuRSbtia0PEzNIRtmViMrt3AeoWBidRXmZsxCNLwgIV6dn2WpuE5Az0bHgpZnQxTKFek0BMKU/d8wIDAQABo1AwTjAdBgNVHQ4EFgQUGHxYqZYyX7cTxKVODVgZwSTdCnwwHwYDVR0jBBgwFoAUGHxYqZYyX7cTxKVODVgZwSTdCnwwDAYDVR0TBAUwAwEB/zANBgkqhkiG9w0BAQ0FAAOBgQByFOl+hMFICbd3DJfnp2Rgd/dqttsZG/tyhILWvErbio/DEe98mXpowhTkC04ENprOyXi7ZbUqiicF89uAGyt1oqgTUCD1VsLahqIcmrzgumNyTwLGWo17WDAa1/usDhetWAMhgzF/Cnf5ek0nK00m0YZGyc4LzgD0CROMASTWNg==</ds:X509Certificate></ds:X509Data></ds:KeyInfo></ds:Signature>
                    <saml:Subject>
                      <saml:NameID SPNameQualifier=""http://sp.example.com/demo1/metadata.php"" Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:transient"">_ce3d2948b4cf20146dee0a0b3dd6f69b6cf86f62d7</saml:NameID>
                      <saml:SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"">
                        <saml:SubjectConfirmationData NotOnOrAfter=""2024-01-18T06:21:48Z"" Recipient=""http://sp.example.com/demo1/index.php?acs"" InResponseTo=""ONELOGIN_4fee3b046395c4e751011e97f8900b5273d56685""/>
                      </saml:SubjectConfirmation>
                    </saml:Subject>
                    <saml:Conditions NotBefore=""2014-07-17T01:01:18Z"" NotOnOrAfter=""2024-01-18T06:21:48Z"">
                      <saml:AudienceRestriction>
                        <saml:Audience>http://sp.example.com/demo1/metadata.php</saml:Audience>
                      </saml:AudienceRestriction>
                    </saml:Conditions>
                    <saml:AuthnStatement AuthnInstant=""2014-07-17T01:01:48Z"" SessionNotOnOrAfter=""2024-07-17T09:01:48Z"" SessionIndex=""_be9967abd904ddcae3c0eb4189adbe3f71e327cf93"">
                      <saml:AuthnContext>
                        <saml:AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</saml:AuthnContextClassRef>
                      </saml:AuthnContext>
                    </saml:AuthnStatement>
                    <saml:AttributeStatement>
                      <saml:Attribute Name=""uid"" NameFormat=""urn:oasis:names:tc:SAML:2.0:attrname-format:basic"">
                        <saml:AttributeValue xsi:type=""xs:string"">test</saml:AttributeValue>
                      </saml:Attribute>
                      <saml:Attribute Name=""mail"" NameFormat=""urn:oasis:names:tc:SAML:2.0:attrname-format:basic"">
                        <saml:AttributeValue xsi:type=""xs:string"">test@example.com</saml:AttributeValue>
                      </saml:Attribute>
                      <saml:Attribute Name=""eduPersonAffiliation"" NameFormat=""urn:oasis:names:tc:SAML:2.0:attrname-format:basic"">
                        <saml:AttributeValue xsi:type=""xs:string"">users</saml:AttributeValue>
                        <saml:AttributeValue xsi:type=""xs:string"">examplerole1</saml:AttributeValue>
                      </saml:Attribute>
                    </saml:AttributeStatement>
                  </saml:Assertion>
                </samlp:Response>";

            #endregion

            #region JWT

            public static readonly string JwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            public static readonly string JwtServerResponse = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzaWQiOiJkYmViMjlhYTMyYjg0MTMxYTA0NjY4MDAyNzAxNWEwZSIsInJvbGUiOlsiQWRtaW5pc3RyYXRvcnMiLCJSZWdpc3RlcmVkIFVzZXJzIiwiU3Vic2NyaWJlcnMiXSwiaXNzIjoidGVzdHNpdGVjZS5sdmgubWUiLCJleHAiOjE0NTA4MzU2ODMsIm5iZiI6MTQ1MDgzMTc4M30.Yf3mmBJ8nV_IozqvvLc8L34dDklU2J7z0uXn3jsICp0";

            #endregion
        }

        public class MockMandatoryUserIdentityCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            public MockMandatoryUserIdentityCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
                DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.AcceptTransferSyntaxes(DicomTransferSyntax.ImplicitVRLittleEndian);
                }

                if (association.UserIdentityNegotiation != null &&
                    association.UserIdentityNegotiation.UserIdentityType.HasValue &&
                    association.UserIdentityNegotiation.PositiveResponseRequested)
                {
                    if (association.UserIdentityNegotiation.UserIdentityType == DicomUserIdentityType.Username &&
                        association.UserIdentityNegotiation.PrimaryField == DicomUserIdentityNegotiationTestData.Username)
                    {
                        association.UserIdentityNegotiation.ServerResponse = string.Empty;
                        return SendAssociationAcceptAsync(association);
                    }

                    if (association.UserIdentityNegotiation.UserIdentityType == DicomUserIdentityType.UsernameAndPasscode &&
                        association.UserIdentityNegotiation.PrimaryField == DicomUserIdentityNegotiationTestData.Username &&
                        association.UserIdentityNegotiation.SecondaryField == DicomUserIdentityNegotiationTestData.Passcode)
                    {
                        association.UserIdentityNegotiation.ServerResponse = string.Empty;
                        return SendAssociationAcceptAsync(association);
                    }

                    if (association.UserIdentityNegotiation.UserIdentityType == DicomUserIdentityType.Kerberos &&
                        association.UserIdentityNegotiation.PrimaryField == DicomUserIdentityNegotiationTestData.KerberosServiceTicket)
                    {
                        association.UserIdentityNegotiation.ServerResponse = DicomUserIdentityNegotiationTestData.KerberosServerResponse;
                        return SendAssociationAcceptAsync(association);
                    }

                    if (association.UserIdentityNegotiation.UserIdentityType == DicomUserIdentityType.Saml &&
                        association.UserIdentityNegotiation.PrimaryField == DicomUserIdentityNegotiationTestData.SamlAssertion)
                    {
                        association.UserIdentityNegotiation.ServerResponse = DicomUserIdentityNegotiationTestData.SamlServerResponse;
                        return SendAssociationAcceptAsync(association);
                    }

                    if (association.UserIdentityNegotiation.UserIdentityType == DicomUserIdentityType.Jwt &&
                        association.UserIdentityNegotiation.PrimaryField == DicomUserIdentityNegotiationTestData.JwtToken)
                    {
                        association.UserIdentityNegotiation.ServerResponse = DicomUserIdentityNegotiationTestData.JwtServerResponse;
                        return SendAssociationAcceptAsync(association);
                    }
                }

                return SendAssociationRejectAsync(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.NoReasonGiven);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
                => SendAssociationReleaseResponseAsync();

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
                => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
        }

        public class MockUserIdentityUnawareCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            public MockUserIdentityUnawareCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
                DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.AcceptTransferSyntaxes(DicomTransferSyntax.ImplicitVRLittleEndian);
                }

                // Emulate user identity unawareness
                association.UserIdentityNegotiation = null;

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
                => SendAssociationReleaseResponseAsync();

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
                => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
        }
    }
}
