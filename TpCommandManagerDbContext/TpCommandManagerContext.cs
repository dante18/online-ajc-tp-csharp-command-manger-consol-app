using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext;

public class TpCommandManagerContext : DbContext
{
    public DbSet<Pizza> Pizzas => this.Set<Pizza>();

    public DbSet<Pasta> Pastas => this.Set<Pasta>();

    public DbSet<Pate> Pates => this.Set<Pate>();

    public DbSet<Ingredient> Ingredients => this.Set<Ingredient>();

    public DbSet<Boisson> Boissons => this.Set<Boisson>();

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
        modelBuilder.Entity<Boisson>().UseTpcMappingStrategy();

        modelBuilder.Entity<Adresse>();
        modelBuilder.Entity<Client>();
        modelBuilder.Entity<Commande>();
        modelBuilder.Entity<ProduitCommande>();
    }
}

