using System.ComponentModel.DataAnnotations;

namespace UsersAuthenticationApi.Dtos
{
  public class UpdateUserDto
  {
    [Required]
    [MinLength(1)]
    public int Id { get; set; }
    public string? Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(11)]
    public string? Phone { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    [MinLength(8)]
    public string? NewPassword { get; set; }
  }
}
