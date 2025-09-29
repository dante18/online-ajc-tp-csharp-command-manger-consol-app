using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpCommandManagerData.Entities;

public class OrderProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OrderId { get; set; }

    [Required]
    public Order Order {  get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }
}