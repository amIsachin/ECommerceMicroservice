using Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Data;

public class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
}
