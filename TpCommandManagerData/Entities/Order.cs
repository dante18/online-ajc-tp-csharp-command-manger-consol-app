using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TpCommandManagerData.Entities;

[Table("Orders")]
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    [Required]
    public int Status { get; set; }

    public int UserId { get; set; }

    [Required]
    public User User {  get; set; }

    public int DeliveryAddressId { get; set; }

    [Required]
    public Address DeliveryAddress { get; set; }

    [Required]
    public List<OrderProduct> OrderProduct { get; set; } = new List<OrderProduct>();
}
