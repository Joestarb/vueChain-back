using vueChain.Dtos;
namespace vueChain.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(UserDto userDto);
    }
}