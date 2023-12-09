// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    /// <summary>User identity type</summary>
    public enum DicomUserIdentityType
    {
        /// <summary>Username as a string in UTF-8</summary>
        Username = 1,

        /// <summary>Username as a string in UTF-8 and passcode</summary>
        UsernameAndPasscode = 2,

        /// <summary>Kerberos Service ticket</summary>
        Kerberos = 3,

        /// <summary>SAML Assertion</summary>
        Saml = 4,

        /// <summary>JSON Web Token</summary>
        Jwt = 5
    }

    public class DicomUserIdentityNegotiation
    {
        /// <summary>
        /// Gets or sets the form of user identity being provided.
        /// </summary>
        public DicomUserIdentityType? UserIdentityType { get; set; }

        /// <summary>
        /// Gets or sets whether a positive server response is requested.
        /// </summary>
        public bool PositiveResponseRequested { get; set; }

        /// <summary>
        /// Gets or sets the identity primary field which might consist of the username,
        /// the Kerberos Service ticket or the SAML assertion.
        /// </summary>
        public string PrimaryField { get; set; }

        /// <summary>
        /// Gets or sets the identity secondary field which is only used when the
        /// user identity type is username and pass code.
        /// </summary>
        public string SecondaryField { get; set; }

        /// <summary>
        /// Gets or sets the server response which might consist of the
        /// Kerberos Service ticket, the SAML response or the JSON web token.
        /// </summary>
        public string ServerResponse { get; set; }

        /// <summary>
        /// Performs a validation of the user identity negotiation properties.
        /// </summary>
        /// <exception cref="ArgumentNullException">An exception is thrown if the user identity type is null</exception>
        /// <exception cref="ArgumentException">An exception is thrown if the secondary field is populated
        /// while the user identity type is not UsernameAndPasscode</exception>
        public void Validate()
        {
            if (!UserIdentityType.HasValue)
            {
                throw new ArgumentNullException(nameof(UserIdentityType), "User identity type should be one of Username, UsernameAndPasscode, Kerberos, Saml or Jwt");
            }

            if ((UserIdentityType == DicomUserIdentityType.Username ||
                UserIdentityType == DicomUserIdentityType.Kerberos ||
                UserIdentityType == DicomUserIdentityType.Saml ||
                UserIdentityType == DicomUserIdentityType.Jwt) &&
                !string.IsNullOrWhiteSpace(SecondaryField))
            {
                throw new ArgumentException("Secondary field should only be populated when the user identity type is UsernameAndPasscode");
            }
        }

        /// <summary>
        /// Clones the user identity negotiation.
        /// </summary>
        /// <returns>Clone of user identity negotiation</returns>
        public DicomUserIdentityNegotiation Clone() =>
            new DicomUserIdentityNegotiation
            {
                UserIdentityType = UserIdentityType,
                PositiveResponseRequested = PositiveResponseRequested,
                PrimaryField = PrimaryField,
                SecondaryField = SecondaryField,
                ServerResponse = ServerResponse
            };
    }
}
