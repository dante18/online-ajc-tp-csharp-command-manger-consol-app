using System.ComponentModel.DataAnnotations;

namespace TpCommandManagerDbContext.Entities;

public class Pate
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; }

}