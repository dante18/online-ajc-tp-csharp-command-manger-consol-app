using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Pasta : Nourriture
{
    [Required]
    public int Type { get; set; }

    [Required]
    public float Kcal { get; set; }

    public Pasta() { }

    public Pasta(string nom, float prix, bool vegetarien, string type) : base(nom, prix, vegetarien)
    {
    }
}