using vueChain.Interfaces;
using vueChain.Dtos;
using vueChain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vueChain.interfaces;

namespace vueChain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogService _logService; // Inyectamos el servicio de logs

        public AuthService(IUserService userService, IConfiguration configuration, ILogService logService)
        {
            _userService = userService;
            _configuration = configuration;
            _logService = logService;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            try
            {
                // Intento de obtener al usuario por su correo
                var user = await _userService.GetUserByEmail(loginDto.Email);
                if (user == null || !VerifyPasswordHash(loginDto.Password, user.PasswordHash))
                {
                    // Registrar el intento fallido de inicio de sesión
                    await _logService.SaveLogAsync(new LogDto
                    {
                        Level = "Warning",
                        Message = "Inicio de sesión fallido",
                        Source = "AuthService",
                        Details = $"Credenciales inválidas: Email: {loginDto.Email}"
                    });

                    return null;
                }

                // Generar el token JWT
                var token = GenerateJwtToken(user);

                // Registrar el inicio de sesión exitoso
                await _logService.SaveLogAsync(new LogDto
                {
                    Level = "Info",
                    Message = "Inicio de sesión exitoso",
                    Source = "AuthService",
                    Details = $"Usuario: {user.Username}, Email: {user.Email}, TokenGeneratedAt: {DateTime.UtcNow}"
                });

                return token;
            }
            catch (Exception ex)
            {
                // Registrar errores inesperados
                await _logService.SaveLogAsync(new LogDto
                {
                    Level = "Error",
                    Message = "Ocurrió un error durante el inicio de sesión",
                    Source = "AuthService",
                    Details = $"Email: {loginDto.Email}, Error: {ex.Message}, StackTrace: {ex.StackTrace}"
                });

                throw; // Relanzar la excepción
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
