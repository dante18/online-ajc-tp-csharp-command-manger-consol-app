using Microsoft.EntityFrameworkCore;

namespace TpCommandManagerDbContext;

public class TpCommandManagerContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=EatDomicile;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}

