using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerDbContext.EntitiesManager;

public class UtilisateurManager
{
    private readonly TpCommandManagerContext _context;

    public UtilisateurManager(TpCommandManagerContext context)
    {
        _context = context;
    }

    public List<Utilisateur> ObtenirListUtilisateur()
    {
        return _context.Utilisateurs.Include(u => u.Adresse).ToList();
    }

    public Utilisateur ObtenirUtilisateur(int id)
    {
        return this._context.Utilisateurs.Include(u => u.Adresse).Where(utilisateur => utilisateur.Id == id).FirstOrDefault();
    }

    public void AjouterUtilisateur(Utilisateur utilisateur)
    {
        this._context.Utilisateurs.Add(utilisateur);
        this._context.SaveChanges();
    }

    public void MiseAJourUtilisateur(Utilisateur utilisateur)
    {
        this._context.Utilisateurs.Update(utilisateur);
        this._context.SaveChanges();
    }

    public void SupprimerUtilisateur(Utilisateur utilisateur)
    {
        this._context.Utilisateurs.Remove(utilisateur);
        this._context.SaveChanges();
    }
}