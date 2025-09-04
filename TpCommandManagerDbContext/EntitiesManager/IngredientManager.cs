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
        using (this._context)
        {
            return _context.Ingredients.ToList();
        }
    }

    public Ingredient ObtenirIngredient(int id)
    {
        using (this._context)
        {
            return this._context.Ingredients.Where(Ingredient => Ingredient.Id == id).FirstOrDefault();
        }
    }

    public void AjouterIngredient(Ingredient Ingredient)
    {
        using (this._context)
        {
            this._context.Ingredients.Add(Ingredient);
            this._context.SaveChanges();
        }
    }

    public void MiseAJourIngredient(Ingredient Ingredient)
    {
        using (this._context)
        {
            this._context.Ingredients.Update(Ingredient);
            this._context.SaveChanges();
        }
    }

    public void SupprimerIngredient(Ingredient Ingredient)
    {
        using (this._context)
        {
            this._context.Ingredients.Remove(Ingredient);
            this._context.SaveChanges();
        }
    }
}