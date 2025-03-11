using Duende.IdentityServer.Models;

namespace Identity.Service
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
                new ApiScope("api.scope"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AllowedScopes = { "scope1" },
                    AccessTokenType=AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true
                },
                // interactive client using code flow + pkce
                new Client
                    {
                        ClientId = "interactive",
                        ClientSecrets = { new Secret("your-client-secret".Sha256()) },
                        RedirectUris = { "https://localhost:7156/signin-oidc" },
                        PostLogoutRedirectUris = { "https://localhost:7156/signout-callback-oidc" },
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                       // RequireConsent = false,
                        AllowOfflineAccess = true,
                        AllowedScopes = { "openid", "profile", "scope2" },
                         AccessTokenType=AccessTokenType.Jwt,
                         AllowAccessTokensViaBrowser = true
                    },
                //login client
                new Client
                {
                    ClientId = "client-app",
                    ClientSecrets = { new Secret("your-secret-key".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "api.scope","scope2" },
                    AccessTokenLifetime = 3600,
                    AccessTokenType=AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
