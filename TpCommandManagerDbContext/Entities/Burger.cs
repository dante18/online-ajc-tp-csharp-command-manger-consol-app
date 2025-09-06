using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Burger : Nourriture
{
    [Required]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public Burger() { }

    public Burger(string nom, float prix, bool vegetarien, List<Ingredient> ingredients) : base(nom, prix, vegetarien)
    {
        this.Ingredients = ingredients;
    }
}