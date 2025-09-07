using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerMain.Models;

public class CommandeModel
{
    public int Id { get; private set; }

    public DateTime DateCommande { get; private set; }

    public DateTime? DateLivraison { get; private set; }

    public int Status { get; private set; }

    public Utilisateur Utilisateur { get; private set; }

    public Adresse Adresse { get; private set; }

    public List<ProduitCommande> ProduitCommandes { get; private set; } = new List<ProduitCommande>();


    public CommandeModel(int id, int status, Utilisateur utilisateur,
        Adresse adresse, List<ProduitCommande> produitCommandes)
    {
        this.Id = id;
        this.DateCommande = DateTime.Now;
        this.DateLivraison = null;
        this.Status = status;
        this.Utilisateur = utilisateur;
        this.Adresse = adresse;
        this.ProduitCommandes = produitCommandes;
    }
}