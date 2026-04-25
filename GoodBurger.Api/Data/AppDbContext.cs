using Microsoft.EntityFrameworkCore;
using GoodBurger.Api.Models;

namespace GoodBurger.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Pedidos { get; set; }
    public DbSet<Menu> Cardapios {get; set;}
    public DbSet<User> usuarios { get; set; }
}