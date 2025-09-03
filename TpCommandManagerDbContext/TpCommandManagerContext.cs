using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext;

public class TpCommandManagerContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=EatDomicile;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>().UseTptMappingStrategy();
        modelBuilder.Entity<Burger>().UseTptMappingStrategy();
        modelBuilder.Entity<Pasta>().UseTptMappingStrategy();
        modelBuilder.Entity<Nourriture>().UseTptMappingStrategy();
        modelBuilder.Entity<Produit>().UseTptMappingStrategy();

        modelBuilder.Entity<Adresse>();
        modelBuilder.Entity<Boisson>();
        modelBuilder.Entity<Client>();
        modelBuilder.Entity<Commande>();
        modelBuilder.Entity<ProduitCommande>();
    }
}

