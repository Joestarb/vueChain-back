using vueChain.Dtos;
using vueChain.Models;

namespace vueChain.Interfaces
{
    public interface IUserService
    {
        Task<IResult> Register(UserDto userDto);
        
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
        
        Task<User> DeleteUser(int  id);
        
        Task<IEnumerable<User>> GetAllUsers();
 
         Task<User> EditUser(int id, UserDto userDto);
        
    }
} 
        