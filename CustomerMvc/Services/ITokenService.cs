using IdentityModel.Client;
using System.Threading.Tasks;

namespace CustomerMvc.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);

    }
}
