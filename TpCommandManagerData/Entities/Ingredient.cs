using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerData.Entities;

[Table("Ingredients")]
public class Ingredient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal KCal { get; set; }

    [Required]
    public bool IsAllergen { get; set; }

    public int? PizzaId { get; set; }

    public Pizza? Pizza { get; set; }

    public int? BurgerId { get; set; }

    public Burger? Burger { get; set; }
}