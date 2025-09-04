using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Produit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Nom { get; set; }

    public float Prix { get; set; }

    [Required]
    public List<ProduitCommande> ProduitCommande { get; set; } = new List<ProduitCommande>();

    public Produit() { }

}