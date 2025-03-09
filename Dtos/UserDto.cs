using System.ComponentModel.DataAnnotations;

namespace vueChain.Dtos
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Role { get; set; }

    }
}