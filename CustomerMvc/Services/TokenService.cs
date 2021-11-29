
using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
// using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace CustomerMvc.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;

        public TokenService(ILogger<TokenService> logger, IOptions<IdentityServerSettings> identityServerSettings)
        {
            _logger = logger;
            _identityServerSettings = identityServerSettings;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)

            using var httpClient = new HttpClient(clientHandler);
            _discoveryDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscoveryUrl).Result;
            if (_discoveryDocument.IsError)
            {
                logger.LogError($"Unable to get discovery document. Error is: {_discoveryDocument.Error}");
                throw new Exception("Unable to get discovery document", _discoveryDocument.Exception);
            }
        }
        public async Task<TokenResponse> GetToken(string scope)
        {
            using var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,//connect/token in identity server

                ClientId = _identityServerSettings.Value.ClientName,
                ClientSecret = _identityServerSettings.Value.ClientPassword,
                Scope = scope
            });


            if (tokenResponse.IsError)
            {
                _logger.LogError($"Unable to get token. Error is: {tokenResponse.Error}");
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }

            return tokenResponse;
        }

    }
}
    

