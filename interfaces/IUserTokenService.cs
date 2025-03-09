using vueChain.Dtos;
using vueChain.Models;
namespace vueChain.Interfaces
{
    public interface IUserTokenService
    {
        Task<UserToken> CreateUserToken(UserTokenDto userToken );
        Task<bool> DeleteUserTokenByToken(string token);

    }
}