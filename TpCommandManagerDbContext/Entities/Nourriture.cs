using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Nourriture : Produit
{
    [Required]
    public bool Vegetarien { get; set; }

    public Nourriture() { }

    public Nourriture(string nom, float prix, bool vegetarien) : base(nom, prix)
    {
        this.Vegetarien = vegetarien;
    }
}