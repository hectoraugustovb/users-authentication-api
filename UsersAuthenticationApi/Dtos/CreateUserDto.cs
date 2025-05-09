﻿using System.ComponentModel.DataAnnotations;

namespace UsersAuthenticationApi.Dtos
{
  public class CreateUserDto
  {
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(11)]
    public string Phone { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
  }
}
