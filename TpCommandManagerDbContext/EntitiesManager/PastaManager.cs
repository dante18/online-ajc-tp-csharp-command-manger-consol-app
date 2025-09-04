using System;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class PastaManager
{
    private readonly TpCommandManagerContext _context;

    public PastaManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Pasta> ObtenirListPasta()
    {
        using (this._context)
        {
            return this._context.Pastas.ToList();
        }
    }

    public List<Pasta> ObtenirPasta(int id)
    {
        using (this._context)
        {
            return this._context.Pastas.Where(pasta => pasta.Id == id).ToList();
        }
    }

    public void ajouterPizza(Pasta pasta)
    {
        using (this._context)
        {
            this._context.Pastas.Add(pasta);
            this._context.SaveChanges();
        }
    }

    public void miseAJourPizza(Pasta pasta)
    {
        using (this._context)
        {
            this._context.Pastas.Update(pasta);
            this._context.SaveChanges();
        }
    }

    public void supprimerPasta(Pasta pasta)
    {
        using (this._context)
        {
            this._context.Pastas.Remove(pasta);
            this._context.SaveChanges();
        }
    }
}
