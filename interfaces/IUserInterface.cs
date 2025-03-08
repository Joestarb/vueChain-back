using vueChain.Dtos;
using vueChain.Models;

namespace vueChain.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(UserDto userDto);
        Task<User> GetUserByUsername(string username);
    }
}