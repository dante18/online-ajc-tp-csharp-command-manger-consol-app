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
        return this._context.Pastas.ToList();
    }

    public Pasta ObtenirPasta(int id)
    {
        return this._context.Pastas.Where(pasta => pasta.Id == id).FirstOrDefault();
    }

    public void AjouterPasta(Pasta pasta)
    {
        this._context.Pastas.Add(pasta);
        this._context.SaveChanges();
    }

    public void MiseAJourPasta(Pasta pasta)
    {
        this._context.Pastas.Update(pasta);
        this._context.SaveChanges();
    }

    public void SupprimerPasta(Pasta pasta)
    {
        this._context.Pastas.Remove(pasta);
        this._context.SaveChanges();
    }
}
