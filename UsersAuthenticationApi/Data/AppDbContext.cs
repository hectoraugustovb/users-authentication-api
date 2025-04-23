using Microsoft.EntityFrameworkCore;
using UsersAuthenticationApi.Models;

namespace UsersAuthenticationApi.Data
{
  public class AppDbContext : DbContext
  {
    public DbSet<UserModel> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
  }
}
