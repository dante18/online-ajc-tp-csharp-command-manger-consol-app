using TpCommandManagerDbContext.Entities;
using TpCommandManagerMain.Abstractions;

namespace TpCommandManagerMain.Models;

public class BurgerModel : AbstractNourritureModel
{
    public List<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

    public BurgerModel(int id, string nom, float prix, bool vegetarien, List<Ingredient> ingredients) : base(id, nom, prix, vegetarien)
    {
        this.Ingredients = ingredients;
    }
}