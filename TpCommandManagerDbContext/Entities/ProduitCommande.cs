using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class ProduitCommande
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public Commande Commande {  get; set; }

    public Produit Produit { get; set; }

    public ProduitCommande() { }

    public ProduitCommande(Commande commande, Produit produit)
    {
        this.Commande = commande;
        this.Produit = produit;
    }
}