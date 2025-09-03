using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Pizza : Nourriture
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    [Required]
    public Pate Pate { get; set; }

    public Pizza() { }

}

