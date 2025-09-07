using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Boisson : Produit
{

    [Required]
    public bool Petillant { get; set; }

    [Required]
    public float Kcal {  get; set; }

    public Boisson() { }

    public Boisson(string nom, float prix, bool estPetillante, int kCal) : base(nom, prix)
    {
        this.Petillant = estPetillante;
        this.Kcal = kCal;
    }
}