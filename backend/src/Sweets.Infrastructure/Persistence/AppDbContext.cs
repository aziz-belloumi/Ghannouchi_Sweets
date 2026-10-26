using Microsoft.EntityFrameworkCore;
using Sweets.Domain.Entities;

namespace Sweets.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Sweet> Sweets => Set<Sweet>();
    public DbSet<Box> Boxes => Set<Box>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}