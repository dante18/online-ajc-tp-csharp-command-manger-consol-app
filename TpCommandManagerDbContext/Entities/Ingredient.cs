using System.ComponentModel.DataAnnotations;

namespace TpCommandManagerDbContext.Entities;

public class Ingredient
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    public float Kcal { get; set; }

    [Required]
    public bool EstAllergene { get; set; }

    [Required]
    public bool EstVegetarien { get; set; }
}