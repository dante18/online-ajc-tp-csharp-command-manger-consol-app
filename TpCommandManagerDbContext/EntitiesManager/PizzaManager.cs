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
        using (this._context)
        {
            return this._context.Pizzas.ToList();
        }
    }

    public Pizza ObtenirPizza(int id)
    {
        using (this._context)
        {
            return this._context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
        }
    }

    public void AjouterPizza(Pizza pizza)
    {
        using (this._context)
        {
            this._context.Pizzas.Add(pizza);
            this._context.SaveChanges();
        }
    }

    public void MiseAJourPizza(Pizza pizza)
    {
        using (this._context)
        {
            this._context.Pizzas.Update(pizza);
            this._context.SaveChanges();
        }
    }

    public void SupprimerPizza(Pizza pizza)
    {
        using (this._context)
        {
            this._context.Pizzas.Remove(pizza);
            this._context.SaveChanges();
        }
    }
}
