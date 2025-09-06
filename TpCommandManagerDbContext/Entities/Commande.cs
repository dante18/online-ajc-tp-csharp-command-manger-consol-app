using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TpCommandManagerDbContext.Entities;

public class Commande
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime DateCommande { get; set; }

    [Required]
    public DateTime DateLivraison { get; set; }

    [Required]
    public bool Status { get; set; }

    [Required]
    public Utilisateur Utilisateur {  get; set; }

    [Required]
    public Adresse Adresse { get; set; }

    [Required]
    public List<ProduitCommande> ProduitCommande { get; set; } = new List<ProduitCommande>();

    public Commande() { }

    public Commande(DateTime dateCommande, DateTime dateLivraison, bool status, Utilisateur utilisateur,
        Adresse adresse, List<ProduitCommande> produitCommande)
    {
        this.DateCommande = dateCommande;
        this.DateLivraison = dateLivraison;
        this.Status = status;
        this.Utilisateur = utilisateur;
        this.Adresse = adresse;
        this.ProduitCommande = produitCommande;
    }
}
