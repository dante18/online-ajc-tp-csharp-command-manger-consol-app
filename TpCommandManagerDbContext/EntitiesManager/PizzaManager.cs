using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class PizzaManager
{
    private readonly TpCommandManagerContext _context;

    public PizzaManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Pizza> ObtenirListPizza()
    {
        return this._context.Pizzas.Include(p => p.Ingredients).ToList();
    }

    public Pizza ObtenirPizza(int id)
    {
        return this._context.Pizzas.Include(p => p.Ingredients).Where(pizza => pizza.Id == id).FirstOrDefault();
    }

    public void AjouterPizza(Pizza pizza)
    {
        this._context.Pizzas.Add(pizza);
        this._context.SaveChanges();
    }

    public void MiseAJourPizza(Pizza pizza)
    {
        this._context.Pizzas.Update(pizza);
        this._context.SaveChanges();
    }

    public void SupprimerPizza(Pizza pizza)
    {
        this._context.Pizzas.Remove(pizza);
        this._context.SaveChanges();
    }
}
