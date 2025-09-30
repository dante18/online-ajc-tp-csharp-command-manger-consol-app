using Microsoft.EntityFrameworkCore;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Context;

public sealed class CommandStoreContext : DbContext
{
    public DbSet<Pizza> Pizzas => this.Set<Pizza>();

    public DbSet<Pasta> Pastas => this.Set<Pasta>();

    public DbSet<Doughs> Doughs => this.Set<Doughs>();

    public DbSet<Ingredient> Ingredients => this.Set<Ingredient>();

    public DbSet<User> Users => this.Set<User>();

    public DbSet<Order> Orders => this.Set<Order>();

    public DbSet<Product> Product => this.Set<Product>();

    public DbSet<Food> Food => this.Set<Food>();

    public DbSet<Burger> Burger => this.Set<Burger>();
    
    public DbSet<Drink> Drinks => this.Set<Drink>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=EatDomicile;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>()
            .HasMany(p => p.Ingredients)
            .WithOne(i => i.Pizza)
            .HasForeignKey(i => i.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Burger>()
            .HasMany(b => b.Ingredients)
            .WithOne(i => i.Burger)
            .HasForeignKey(i => i.BurgerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pizza>().UseTptMappingStrategy();
        modelBuilder.Entity<Burger>().UseTptMappingStrategy();
        modelBuilder.Entity<Pasta>().UseTptMappingStrategy();
        modelBuilder.Entity<Food>().UseTptMappingStrategy();
        modelBuilder.Entity<Drink>().UseTptMappingStrategy();
        modelBuilder.Entity<Product>().UseTptMappingStrategy();

        modelBuilder.Entity<Address>();
        modelBuilder.Entity<Drink>();
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Order>();
        modelBuilder.Entity<OrderProduct>();
    }
}

