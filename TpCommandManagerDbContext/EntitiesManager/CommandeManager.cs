using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class CommandeManager
{
    private readonly TpCommandManagerContext _context;

    public CommandeManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Commande> ObtenirListCommande()
    {
        return _context.Commandes
            .Include(c => c.Utilisateur)
            .Include(c => c.Adresse)
            .Include(pc => pc.ProduitCommande).ThenInclude(pc => pc.Produit)
            .ToList();
    }

    public Commande ObtenirCommande(int id)
    {
        return _context.Commandes
            .Include(c => c.Utilisateur)
            .Include(c => c.Adresse)
            .Include(pc => pc.ProduitCommande).ThenInclude(pc => pc.Produit).Where(commande => commande.Id == id).FirstOrDefault();
    }

    public void AjouterCommande(Commande commande)
    {
        this._context.Commandes.Add(commande);
        this._context.SaveChanges();
    }

    public void MiseAJourCommande(Commande commande)
    {
        this._context.Commandes.Update(commande);
        this._context.SaveChanges();
    }

    public void SupprimerCommande(Commande commande)
    {
        this._context.Commandes.Remove(commande);
        this._context.SaveChanges();
    }
}