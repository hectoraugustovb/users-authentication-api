namespace UsersAuthenticationApi.Dtos
{
  public class UpdateUserDto
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }
  }
}
