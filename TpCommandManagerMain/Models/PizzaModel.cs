using TpCommandManagerMain.Abstractions;

namespace TpCommandManagerMain.Models;

public class PizzaModel : AbstractNourritureModel
{
    public List<IngredientModel> Ingredients { get; private set; } = new List<IngredientModel>();

    public PateModel Pate { get; private set; }

    public PizzaModel(int id, string nom, float prix, bool vegetarien, List<IngredientModel> ingredients, PateModel pate) : base(id, nom, prix, vegetarien)
    {
        this.Ingredients = ingredients;
        this.Pate = pate;
    }
}
