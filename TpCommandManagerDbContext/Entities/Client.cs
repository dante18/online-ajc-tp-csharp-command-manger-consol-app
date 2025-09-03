using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TpCommandManagerDbContext.Entities;

public class Client
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Prenom {  get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    public string Telephone { get; set; }

    public Client() { }

}