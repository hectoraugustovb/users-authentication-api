using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAuthenticationApi.Data;
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
    public ActionResult<List<UserModel>> GetAllUsers()
    {
      var users = _context.Users.ToList();

      return Ok(users);
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
    public ActionResult<UserModel> CreateUser([FromBody] UserModel user)
    {
      if (user == null)
      {
        return BadRequest();
      }

      if (user.Name == null || user.Email == null || user.Phone == null || user.Password == null)
      {
        return BadRequest();
      }

      var hashedPassword = passwordService.HashPassword(user, user.Password);
      user.Password = hashedPassword;

      _context.Users.Add(user);
      _context.SaveChanges();

      return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
  }
}
