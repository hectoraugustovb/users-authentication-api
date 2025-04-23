using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

      var response = users.Select(user => new AuthResponseDto
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
  }
}
