// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
    [Collection("Network"), Trait("Category", "Network")]
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

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(CancellationToken.None);
                    association.Dispose();
                }
            }

            Assert.NotNull(exception);
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
                client.AssociationAccepted += (sender, args) =>
                {
                    Assert.NotNull(args.Association);
                    Assert.NotNull(args.Association.UserIdentityNegotiation);
                    Assert.Equal(DicomUserIdentityType.Username, args.Association.UserIdentityNegotiation.UserIdentityType);
                    Assert.Empty(args.Association.UserIdentityNegotiation.ServerResponse);
                };
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
                    PositiveResponseRequested = false
                }
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(CancellationToken.None);
                    association.Dispose();
                }
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

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);

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
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(CancellationToken.None);
                    association.Dispose();
                }
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

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);

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
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(CancellationToken.None);
                    association.Dispose();
                }
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

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);

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
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(CancellationToken.None);
                    association.Dispose();
                }
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

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);

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
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(CancellationToken.None);
                    association.Dispose();
                }
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

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);

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
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(CancellationToken.None);
                    association.Dispose();
                }
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

            DicomCEchoResponse cEchoResponse;

            using var association = await connection.OpenAssociationAsync(openAssociationRequest, CancellationToken.None);
            try
            {
                cEchoResponse = await association.SendCEchoRequestAsync(cEchoRequest, CancellationToken.None).ConfigureAwait(false);
            }
            finally
            {
                await association.ReleaseAsync(CancellationToken.None);
            }

            Assert.NotNull(cEchoResponse);
            Assert.Equal(DicomState.Success, cEchoResponse.Status.State);
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
            public static readonly String Username = "USERNAME";
            public static readonly String Passcode = "PASSWORD";

            public static readonly String KerberosServiceTicket = "KERBEROS_SERVICE_TICKET";
            public static readonly String KerberosServerResponse = "KERBEROS_SERVER_RESPONSE";

            public static readonly String SamlAssertion = "SAML_ASSERTION";
            public static readonly String SamlServerResponse = "SAML_SERVER_RESPONSE";

            public static readonly String JwtToken = "JWT_TOKEN";
            public static readonly String JwtServerResponse = "JWT_SERVER_RESPONSE";
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
                    association.UserIdentityNegotiation.PositiveResponseRequested)
                {
                    if (association.UserIdentityNegotiation.UserIdentityType == DicomUserIdentityType.Username &&
                        association.UserIdentityNegotiation.PrimaryField == DicomUserIdentityNegotiationTestData.Username)
                    {
                        association.UserIdentityNegotiation.ServerResponse = String.Empty;
                        return SendAssociationAcceptAsync(association);
                    }

                    if (association.UserIdentityNegotiation.UserIdentityType == DicomUserIdentityType.UsernameAndPasscode &&
                        association.UserIdentityNegotiation.PrimaryField == DicomUserIdentityNegotiationTestData.Username &&
                        association.UserIdentityNegotiation.SecondaryField == DicomUserIdentityNegotiationTestData.Passcode)
                    {
                        association.UserIdentityNegotiation.ServerResponse = String.Empty;
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
    }
}
