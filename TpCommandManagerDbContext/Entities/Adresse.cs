using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TpCommandManagerDbContext.Entities;

public class Adresse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Rue {  get; set; }

    [Required]
    public string Ville { get; set; }

    [Required]
    public string Region { get; set; }

    [Required]
    public string CodePostal { get; set; }

    [Required]
    public string Pays { get; set; }

    public Adresse() { }

    public Adresse(string rue, string codePostal, string ville, string region, string pays)
    {
        this.Rue = rue;
        this.CodePostal = codePostal;
        this.Ville = ville;
        this.Region = region;
        this.Pays = pays;
    }

}