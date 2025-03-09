using System.ComponentModel.DataAnnotations;

namespace vueChain.Dtos;

public class UserTokenDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}