using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Pizza : Nourriture
{
    [Required]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    [Required]
    public Pate Pate { get; set; }

    public Pizza() { }

    public Pizza(string nom, float prix, bool vegetarien, List<Ingredient> ingredients, Pate pate) : base(nom, prix, vegetarien)
    {
        this.Ingredients = ingredients;
        this.Pate = pate;
    }
}

