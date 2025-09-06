using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class BurgerManager
{
    private readonly TpCommandManagerContext _context;

    public BurgerManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Burger> ObtenirListBurger()
    {
        return this._context.Burger.Include(p => p.Ingredients).ToList();
    }

    public Burger ObtenirBurger(int id)
    {
        return this._context.Burger.Include(p => p.Ingredients).Where(burger => burger.Id == id).FirstOrDefault();
    }

    public void AjouterBurger(Burger burger)
    {
        this._context.Burger.Add(burger);
        this._context.SaveChanges();
    }

    public void MiseAJourBurger(Burger burger)
    {
        this._context.Burger.Update(burger);
        this._context.SaveChanges();
    }

    public void SupprimerBurger(Burger burger)
    {
        this._context.Burger.Remove(burger);
        this._context.SaveChanges();
    }
}