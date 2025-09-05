using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class IngredientManager
{
    private readonly TpCommandManagerContext _context;

    public IngredientManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Ingredient> ObtenirListIngredient()
    {
        return _context.Ingredients.ToList();
    }

    public Ingredient ObtenirIngredient(int id)
    {
        return this._context.Ingredients.Where(Ingredient => Ingredient.Id == id).FirstOrDefault();
    }

    public void AjouterIngredient(Ingredient Ingredient)
    {
        this._context.Ingredients.Add(Ingredient);
        this._context.SaveChanges();
    }

    public void MiseAJourIngredient(Ingredient Ingredient)
    {
        this._context.Ingredients.Update(Ingredient);
        this._context.SaveChanges();
    }

    public void SupprimerIngredient(Ingredient Ingredient)
    {
        this._context.Ingredients.Remove(Ingredient);
        this._context.SaveChanges();
    }
}