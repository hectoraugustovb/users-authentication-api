using System.ComponentModel.DataAnnotations;

namespace UsersAuthenticationApi.Dtos
{
  public class LoginDto
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
  }
}
