using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class NourritureManager
{
    private readonly TpCommandManagerContext _context;

    public NourritureManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Nourriture> ObtenirListNourriture()
    {
        return this._context.Nourriture.ToList();
    }

    public Nourriture ObtenirNourriture(int id)
    {
        return this._context.Nourriture.Where(Nourriture => Nourriture.Id == id).FirstOrDefault();
    }
}
