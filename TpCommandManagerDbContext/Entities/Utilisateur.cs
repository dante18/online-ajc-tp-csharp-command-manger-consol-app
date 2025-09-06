using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TpCommandManagerDbContext.Entities;

public class Utilisateur
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

    [Required]
    public string Email { get; set; }

    [Required]
    public Adresse Adresse { get; set; }

    public Utilisateur() { }

    public Utilisateur(string nom, string prenom, string telephone, string email, Adresse adresse)
    {
        this.Nom = nom;
        this.Prenom = prenom;
        this.Telephone = telephone;
        this.Email = email;
        this.Adresse = adresse;
    }

}