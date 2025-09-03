using System.ComponentModel.DataAnnotations;

namespace TpCommandManagerMain.Models;

public class AdresseModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string rue { get; set; }

}