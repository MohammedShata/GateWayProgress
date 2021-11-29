using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class IdentityConfiguration
    {

        public static List<TestUser> TestUsers =>
    new List<TestUser>
    {
                new TestUser
                {
                    SubjectId = "1144",
                    Username = "test",
                    Password = "test",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Mohamed"),
                        new Claim(JwtClaimTypes.GivenName, "Ahmed"),
                        new Claim(JwtClaimTypes.FamilyName, "Shata"),
                        new Claim(JwtClaimTypes.Email , "Shatams4@gmail.com"),
                    }
                }
};
        public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {   
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource
        {
          Name = "role",
          UserClaims = new List<string> {"role"}
        }
    };
        public static IEnumerable<ApiScope> ApiScopes =>
new ApiScope[]
{
                new ApiScope("CustomerApi.read"),
                new ApiScope("CustomerApi.write"),
};
        public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
                new ApiResource("myApi")
                {
                    Scopes = new List<string>{ "CustomerApi.read", "CustomerApi.write" },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }

        };
        public static IEnumerable<Client> Clients =>
    new Client[]
    {
                new Client
        {
          ClientId = "m2m.client",
          ClientName = "Client Credentials Client",

          AllowedGrantTypes = GrantTypes.ClientCredentials,
          ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

          AllowedScopes = { "CustomerApi.read", "CustomerApi.write" }
        },

        // interactive client using code flow + pkce
        new Client
        {
          ClientId = "interactive",
          ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

          AllowedGrantTypes = GrantTypes.Code,

          RedirectUris = {"https://localhost:5444/signin-oidc"},
          FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
          PostLogoutRedirectUris = {"https://localhost:5444s/signout-callback-oidc"},

          AllowOfflineAccess = true,
          AllowedScopes = {"openid", "profile", "CustomerApi.read"},
          RequirePkce = true,
          RequireConsent = true,
          AllowPlainTextPkce = false
        },
      };
    };
    }

