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
        return this._context.Pates.ToList();
    }

    public Pate ObtenirPate(int id)
    {
        return this._context.Pates.FirstOrDefault(pate => pate.Id == id);
    }

    public void AjouterPate(Pate pate)
    {
        this._context.Pates.Add(pate);
        this._context.SaveChanges();
    }

    public void MiseAJourPate(Pate pate)
    {
        this._context.Pates.Update(pate);
        this._context.SaveChanges();
    }

    public void SupprimerPate(Pate pate)
    {
        this._context.Pates.Remove(pate); 
        this._context.SaveChanges();
    }
}
