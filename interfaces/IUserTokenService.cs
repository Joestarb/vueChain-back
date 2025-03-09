using vueChain.Dtos;
using vueChain.Models;
namespace vueChain.Interfaces
{
    // interfaces/IUserTokenService.cs
    public interface IUserTokenService
    {
        Task<UserToken> CreateUserToken(UserTokenDto userToken);
        Task<bool> DeleteUserTokenByToken(string token);
        Task<UserToken> GetUserTokenByToken(string token); // Nuevo método
    }
}