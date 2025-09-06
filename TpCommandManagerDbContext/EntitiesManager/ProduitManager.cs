using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class ProduitManager
{
    private readonly TpCommandManagerContext _context;

    public ProduitManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Produit> ObtenirListProduit()
    {
        return this._context.Produit.Include(p => p.ProduitCommande).ToList();
    }

    public Produit ObtenirProduit(int id)
    {
        return this._context.Produit.Include(p => p.ProduitCommande).Where(Produit => Produit.Id == id).FirstOrDefault();
    }
}
