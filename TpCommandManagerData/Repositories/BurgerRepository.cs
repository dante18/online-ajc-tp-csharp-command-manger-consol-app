using Microsoft.EntityFrameworkCore;
using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class BurgerRepository
{
    private readonly CommandStoreContext _context;

    public BurgerRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Burger> GetAllBurgers()
    {
        return this._context.Burger.Include(p => p.Ingredients).ToList();
    }

    public Burger GetBurger(int id)
    {
        return this._context.Burger.Include(p => p.Ingredients).Where(burger => burger.Id == id).FirstOrDefault();
    }

    public void CreateBurger(Burger burger)
    {
        this._context.Burger.Add(burger);
        this._context.SaveChanges();
    }

    public void UpdateBurger(Burger burger)
    {
        this._context.Burger.Update(burger);
        this._context.SaveChanges();
    }

    public void DeleteBurger(Burger burger)
    {
        this._context.Burger.Remove(burger);
        this._context.SaveChanges();
    }
}