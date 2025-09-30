using Microsoft.EntityFrameworkCore;
using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class PizzaRepository
{
    private readonly CommandStoreContext _context;

    public PizzaRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Pizza> GetAllPizzas()
    {
        return this._context.Pizzas
            .Include(p => p.Ingredients)
            .Include(p => p.Doughs)
            .ToList();
    }

    public Pizza GetPizza(int id)
    {
        return this._context.Pizzas
            .Include(p => p.Ingredients)
            .Include(p => p.Doughs)
            .Where(pizza => pizza.Id == id).FirstOrDefault();
    }

    public void CreatePizza(Pizza pizza)
    {
        this._context.Pizzas.Add(pizza);
        this._context.SaveChanges();
    }

    public void UpdatePizza(Pizza pizza)
    {
        this._context.Pizzas.Update(pizza);
        this._context.SaveChanges();
    }

    public void DeletePizza(Pizza pizza)
    {
        this._context.Pizzas.Remove(pizza);
        this._context.SaveChanges();
    }
}
