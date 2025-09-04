using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Nourriture : Produit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; private set; }

    [Required]
    public bool Vegetarien { get; set; }

    public Nourriture() { }

    public Nourriture(int id, string nom, float prix, bool vegetarien) : base(id, nom, prix)
    {
        this.Vegetarien = vegetarien;
    }
}