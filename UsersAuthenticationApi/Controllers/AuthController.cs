using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersAuthenticationApi.Data;
using UsersAuthenticationApi.Dtos;
using UsersAuthenticationApi.Models;
using UsersAuthenticationApi.Services;

namespace UsersAuthenticationApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly AppDbContext _context;
    private readonly PasswordService passwordService;

    public AuthController(AppDbContext context)
    {
      _context = context;
      passwordService = new PasswordService();
    }

    [HttpGet]
    public ActionResult<List<AuthResponseDto>> GetAllUsers()
    {
      var users = _context.Users.ToList();

      List<AuthResponseDto> response = users.Select(user => new AuthResponseDto
      {
         Id = user.Id,
         Name = user.Name,
         Email = user.Email,
         Phone = user.Phone
      }).ToList();

      return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<UserModel> GetById(int id)
    {
      var user = _context.Users.Find(id);

      if (user == null)
      {
        return BadRequest("User not found");
      }

      return Ok(user);
    }

    [HttpPost]
    public ActionResult<AuthResponseDto> CreateUser([FromBody] CreateUserDto userDto)
    {
      if (userDto == null)
      {
        return BadRequest();
      }

      if (
        string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email) ||
        string.IsNullOrEmpty(userDto.Phone) || string.IsNullOrEmpty(userDto.Password)
      )
      {
        return BadRequest();
      }

      var user = new UserModel
      {
        Name = userDto.Name,
        Email = userDto.Email,
        Phone = userDto.Phone,
        Password = userDto.Password,
      };

      var hashedPassword = passwordService.HashPassword(user, userDto.Password);
      user.Password = hashedPassword;

      _context.Users.Add(user);
      _context.SaveChanges();

      var response = new AuthResponseDto
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Phone = user.Phone,
      };

      return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<AuthResponseDto>> UpdateUser([FromBody] UpdateUserDto userDto)
    {
      if (userDto == null || string.IsNullOrEmpty(userDto.Password))
      {
        return BadRequest();
      }

      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);

      if (user == null)
      {
        return NotFound("User not found.");
      }

      bool isValidPassword = passwordService.VerifyPassword(user, user.Password, userDto.Password);

      if (!isValidPassword)
      {
        return BadRequest("Wrong password");
      }

      user.Name = userDto.Name ?? user.Name;
      user.Email = userDto.Email ?? user.Email;
      user.Phone = userDto.Phone ?? user.Phone;

      if (userDto.NewPassword != null && userDto.Password != userDto.NewPassword)
      {
        var newPassword = passwordService.HashPassword(user, userDto.NewPassword);

        user.Password = newPassword;
      }

      _context.SaveChanges();

      AuthResponseDto response = new()
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Phone = user.Phone,
      };

      return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
      var user = _context.Users.Find(id);

      if (user == null)
      {
        return NotFound("User not found");
      }

      _context.Users.Remove(user);
      _context.SaveChanges();

      return NoContent();
    }
  }
}
