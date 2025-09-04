using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Burger : Nourriture
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {  get; set; }


    [Required]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public Burger() { }

}