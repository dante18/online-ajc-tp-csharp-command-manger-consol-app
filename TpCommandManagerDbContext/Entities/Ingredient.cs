using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Ingredient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    public float Kcal { get; set; }

    [Required]
    public bool EstAllergene { get; set; }

    [Required]
    public bool EstVegetarien { get; set; }

    public Ingredient() {}

}