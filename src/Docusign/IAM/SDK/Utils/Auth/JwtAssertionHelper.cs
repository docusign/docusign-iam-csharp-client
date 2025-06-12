// This file is manually maintained and should not be overwritten by code generation.
// Contains custom utilities for generating JWT assertions for Docusign authentication.

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Docusign.IAM.SDK.Models.Components;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Docusign.IAM.SDK.Utils.Auth
{
    /// <summary>
    /// Helper class for generating JWT (JSON Web Token) assertions used in Docusign authentication.
    /// Provides functionality to create JWT tokens using RSA private keys for Docusign OAuth flows.
    /// </summary>
    public class JwtAssertionHelper
    {
        /// <summary>
        /// Generates a JWT (JSON Web Token) for Docusign authentication.
        /// </summary>
        /// <param name="privateKey">The RSA private key in PEM format used to sign the JWT.</param>
        /// <param name="clientId">The Docusign integration key (client ID) that will be used as the JWT issuer.</param>
        /// <param name="userId">The Docusign user ID (typically an email) that will be used as the JWT subject.</param>
        /// <param name="oAuthBasePath">The Docusign OAuth base path that will be used as the JWT audience. Defaults to Demo.</param>
        /// <param name="scopes">A list of OAuth scopes to request access to. Defaults to all scopes.</param>
        /// <returns>A signed JWT token string in compact serialization format.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the generated JWT is not in valid format.</exception>
        public static string GenerateJWT(
            string privateKey,
            string clientId,
            string userId,
            OAuthBasePath oAuthBasePath = OAuthBasePath.Demo,
            IEnumerable<string>? scopes = null
        )
        {
            if (scopes == null)
            {
                scopes = Enum.GetValues<AuthScope>().Select(s => s.Value());
            }

            string audience;
            if (oAuthBasePath == OAuthBasePath.Demo)
            {
                audience = "account-d.docusign.com";
            }
            else if (oAuthBasePath == OAuthBasePath.Production)
            {
                audience = "account.docusign.com";
            }
            else
            {
                throw new ArgumentException("Invalid OAuth base path.");
            }

            var rsa = LoadRsaFromPem(privateKey);

            var securityKey = new RsaSecurityKey(rsa);
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.RsaSha256
            );
            var now = DateTime.UtcNow;
            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload(
                issuer: clientId,
                audience: audience,
                claims: new List<Claim>
                {
                    new Claim("sub", userId),
                    new Claim("scope", string.Join(" ", scopes)),
                },
                notBefore: now,
                expires: now.AddHours(1),
                issuedAt: now
            );

            var token = new JwtSecurityToken(header, payload);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            if (jwt.Split('.').Length != 3)
                throw new InvalidOperationException(
                    "Generated JWT is not in valid compact serialization format."
                );
            return jwt;
        }

        /// <summary>
        /// Loads an RSA private key from PEM format string.
        /// </summary>
        /// <param name="pem">The PEM-formatted RSA private key string.</param>
        /// <returns>An RSA object initialized with the private key.</returns>
        /// <remarks>
        /// This method handles both PKCS#1 ("-----BEGIN RSA PRIVATE KEY-----") and
        /// PKCS#8 format private keys by cleaning and converting the PEM string to the appropriate format.
        /// </remarks>
        private static RSA LoadRsaFromPem(string pem)
        {
            string cleanedPem = pem.Replace("-----BEGIN RSA PRIVATE KEY-----", "")
                .Replace("-----END RSA PRIVATE KEY-----", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Trim();

            byte[] keyBytes = Convert.FromBase64String(cleanedPem);

            if (pem.Contains("RSA PRIVATE KEY"))
            {
                var rsaPrivStruct = Org.BouncyCastle.Asn1.Pkcs.RsaPrivateKeyStructure.GetInstance(
                    Org.BouncyCastle.Asn1.Asn1Sequence.GetInstance(keyBytes)
                );

                var privKeyInfo = new Org.BouncyCastle.Asn1.Pkcs.PrivateKeyInfo(
                    new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(
                        Org.BouncyCastle.Asn1.Pkcs.PkcsObjectIdentifiers.RsaEncryption,
                        Org.BouncyCastle.Asn1.DerNull.Instance
                    ),
                    rsaPrivStruct.ToAsn1Object()
                );

                keyBytes = privKeyInfo.GetEncoded();
            }
            var privateKeyInfoParsed = Org.BouncyCastle.Asn1.Pkcs.PrivateKeyInfo.GetInstance(
                keyBytes
            );
            var key = PrivateKeyFactory.CreateKey(privateKeyInfoParsed);

            var rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)key);
            var rsa = RSA.Create();
            rsa.ImportParameters(rsaParams);
            return rsa;
        }
    }
}
