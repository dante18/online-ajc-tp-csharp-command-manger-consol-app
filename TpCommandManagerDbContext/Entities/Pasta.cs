using System.ComponentModel.DataAnnotations;

namespace TpCommandManagerDbContext.Entities;

public class Pasta
{
    public int Id { get; set; }

    [Required]
    public string Nom {  get; set; }

    [Required]
    public int Type { get; set; }

    [Required]
    public float Kcal { get; set; }
}