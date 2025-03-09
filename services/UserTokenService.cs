using vueChain.Interfaces;
using vueChain.Models;
using vueChain.Dtos;
using vueChain.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace vueChain.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly ApplicationDbContext _context;

        public UserTokenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserToken> CreateUserToken(UserTokenDto userTokenDto)
        {
            var userToken = new UserToken
            {
                Username = userTokenDto.Username,
                Token = userTokenDto.Token,
                Email = userTokenDto.Email,
                created_at = DateTime.UtcNow,
                expires_at = DateTime.UtcNow.AddDays(7)
            };

            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();

            return userToken;
        }

        public async Task<bool> DeleteUserTokenByToken(string token)
        {
            var userToken = await _context.UserTokens.FirstOrDefaultAsync(ut => ut.Token == token);
            if (userToken == null)
            {
                return false;
            }

            _context.UserTokens.Remove(userToken);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}