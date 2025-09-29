using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerData.Entities;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    [Required]
    public List<OrderProduct> OrderProduct { get; set; } = new List<OrderProduct>();
}