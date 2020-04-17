// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#pragma warning disable 1591

using System.IdentityModel.Selectors;
using Microsoft.IdentityModel.Tokens.Saml;
using Microsoft.IdentityModel.Tokens.Saml2;

namespace System.ServiceModel.Federation
{
    /// <summary>
    /// Returns a WSTrustChannelSecurityTokenProvider to obtain token Saml
    /// </summary>
    public class WsTrustChannelSecurityTokenManager : ClientCredentialsSecurityTokenManager
    {
        private WsTrustChannelClientCredentials _wsTrustChannelClientCredentials;
        private SecurityTokenManager _clientCredentialsSecurityTokenManager;

        public WsTrustChannelSecurityTokenManager(WsTrustChannelClientCredentials clientCredentials)
            : base(clientCredentials)
        {
            _wsTrustChannelClientCredentials = clientCredentials;
            if (_wsTrustChannelClientCredentials.ClientCredentials != null)
            {
                _clientCredentialsSecurityTokenManager = _wsTrustChannelClientCredentials.ClientCredentials.CreateSecurityTokenManager();
            }
        }

        /// <summary>
        /// Make use of this extensibility point for returning a custom SecurityTokenProvider when SAML tokens are specified in the tokenRequirement
        /// </summary>
        /// <param name="tokenRequirement">A SecurityTokenRequirement  </param>
        /// <returns>The appropriate SecurityTokenProvider</returns>
        public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
        {
            // If token requirement matches SAML token return the custom SAML token provider
            // that performs custom work to serve up the token
            if (Saml2Constants.OasisWssSaml2TokenProfile11.Equals(tokenRequirement.TokenType) ||
                Saml2Constants.Saml2TokenProfile11.Equals(tokenRequirement.TokenType) ||
                SamlConstants.OasisWssSamlTokenProfile11.Equals(tokenRequirement.TokenType))
            {
                return new WSTrustChannelSecurityTokenProvider(tokenRequirement, _wsTrustChannelClientCredentials.RequestContext)
                {
                    CacheIssuedTokens = _wsTrustChannelClientCredentials.CacheIssuedTokens,
                    MaxIssuedTokenCachingTime = _wsTrustChannelClientCredentials.MaxIssuedTokenCachingTime,
                    IssuedTokenRenewalThresholdPercentage = _wsTrustChannelClientCredentials.IssuedTokenRenewalThresholdPercentage
                };
            }
            // If the original ChannelFactory had a ClientCredentials instance, defer to that
            else if (_clientCredentialsSecurityTokenManager != null)
            {
                return _clientCredentialsSecurityTokenManager.CreateSecurityTokenProvider(tokenRequirement);
            }
            // This means ClientCredentials was replaced with WsTrustChannelClientCredentials in the ChannelFactory so defer
            // to base class to create other token providers.
            else
            {
                return base.CreateSecurityTokenProvider(tokenRequirement);
            }
        }
    }
}
