using System;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class BoissonManager
{
    private readonly TpCommandManagerContext _context;

    public BoissonManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Boisson> ObtenirListBoisson()
    {
        return this._context.Boissons.ToList();
    }

    public Boisson ObtenirBoisson(int id)
    {
        return this._context.Boissons.Where(boisson => boisson.Id == id).FirstOrDefault();
    }

    public void AjouterPasta(Boisson boisson)
    {
        this._context.Boissons.Add(boisson);
        this._context.SaveChanges();
    }

    public void MiseAJourBoisson(Boisson boisson)
    {
        this._context.Boissons.Update(boisson);
        this._context.SaveChanges();
    }

    public void SupprimerBoisson(Boisson boisson)
    {
        this._context.Boissons.Remove(boisson);
        this._context.SaveChanges();
    }
}