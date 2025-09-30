using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class IngredientRepository
{
    private readonly CommandStoreContext _context;

    public IngredientRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Ingredient> GetAllIngredients()
    {
        return _context.Ingredients.ToList();
    }

    public Ingredient GetIngredient(int id)
    {
        return this._context.Ingredients.Where(i => i.Id == id).FirstOrDefault();
    }

    public void CreateIngredient(Ingredient ingredient)
    {
        this._context.Ingredients.Add(ingredient);
        this._context.SaveChanges();
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        this._context.Ingredients.Update(ingredient);
        this._context.SaveChanges();
    }

    public void DeleteIngredient(Ingredient ingredient)
    {
        this._context.Ingredients.Remove(ingredient);
        this._context.SaveChanges();
    }
}