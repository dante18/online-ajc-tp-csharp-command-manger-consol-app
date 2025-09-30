using System.ComponentModel.DataAnnotations;

namespace TpCommandManagerData.Entities;

public class Food : Product
{
    [Required]
    public bool Vegetarian { get; set; }
}