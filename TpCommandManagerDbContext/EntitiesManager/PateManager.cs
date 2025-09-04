using System;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class PateManager
{
    private readonly TpCommandManagerContext _context;

    public PateManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Pate> ObtenirListPate()
    {
        using (this._context)
        {
            return this._context.Pates.ToList();
        }
    }

    public Pate ObtenirPate(int id)
    {
        using (this._context)
        {
            return this._context.Pates.FirstOrDefault(pate => pate.Id == id);
        }
    }

    public void AjouterPate(Pate pate)
    {
        using (this._context)
        {
            this._context.Pates.Add(pate);
            this._context.SaveChanges();
        }
    }

    public void MiseAJourPate(Pate pate)
    {
        using (this._context)
        {
            this._context.Pates.Update(pate);
            this._context.SaveChanges();
        }
    }

    public void SupprimerPate(Pate pate)
    {
        using (this._context)
        {
            this._context.Pates.Remove(pate);
            this._context.SaveChanges();
        }
    }
}
