using Core.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }
}