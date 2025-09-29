using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class DrinkRepository
{
    private readonly CommandStoreContext _context;

    public DrinkRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Drink> GetAllDrinks()
    {
        return this._context.Drinks.ToList();
    }

    public Drink GetDrink(int id)
    {
        return this._context.Drinks.Where(d => d.Id == id).FirstOrDefault();
    }

    public void CreateDrink(Drink drink)
    {
        this._context.Drinks.Add(drink);
        this._context.SaveChanges();
    }

    public void UpdateDrink(Drink drink)
    {
        this._context.Drinks.Update(drink);
        this._context.SaveChanges();
    }

    public void DeleteDrink(Drink drink)
    {
        this._context.Drinks.Remove(drink);
        this._context.SaveChanges();
    }
}