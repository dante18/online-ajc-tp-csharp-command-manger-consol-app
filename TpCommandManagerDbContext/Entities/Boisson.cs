using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerDbContext.Entities;

public class Boisson : Produit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public bool Petillant { get; set; }

    [Required]
    public float Kcal {  get; set; }

    public Boisson() { }

}