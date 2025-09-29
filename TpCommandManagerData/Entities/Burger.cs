using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerData.Entities;

[Table("Burger")]
public class Burger : Food
{
    [Required]
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}