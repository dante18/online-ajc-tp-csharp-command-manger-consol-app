using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Ingredient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    public float Kcal { get; set; }

    [Required]
    public bool EstAllergene { get; set; }

    public Ingredient() {}

    public Ingredient(int id, string nom, float kcal, bool estAllergene)
    {
        this.Id = id;
        this.Nom = nom;
        this.Kcal = kcal;
        this.EstAllergene = estAllergene;
    }
}