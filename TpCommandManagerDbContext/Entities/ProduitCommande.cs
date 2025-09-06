using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class ProduitCommande
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int? CommandeId { get; set; }

    [Required]
    public int? ProduitId {  get; set; }

    public Produit Produit { get; set; } = null!;

    public ProduitCommande() { }

    public ProduitCommande(int? produitId)
    {
        this.ProduitId = produitId;
    }
}