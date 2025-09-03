using System.ComponentModel.DataAnnotations;

namespace TpCommandManagerDbContext.Entities;

public class Pizza
{
    public int id;

    [Required]
    public string nom { get; set; }

    [Required]
    public List<Ingredient> ingredients { get; set; } = new List<Ingredient>();

    [Required]
    public Pate pate { get; set; }
}

