namespace UsersAuthenticationApi.Dtos
{
  public class CreateUserDto
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
  }
}
