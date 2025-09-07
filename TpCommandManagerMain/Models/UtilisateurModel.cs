using System.ComponentModel.DataAnnotations;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerMain.Models;

public class UtilisateurModel
{
    public int Id { get; private set; }

    public string Prenom { get; private set; }

    public string Nom { get; private set; }

    public string Telephone { get; private set; }

    public string Email { get; private set; }

    public Adresse Adresse { get; private set; }


    public UtilisateurModel(int id, string nom, string prenom, string telephone, string email, Adresse adresse)
    {
        this.Id = id;
        this.Nom = nom;
        this.Prenom = prenom;
        this.Telephone = telephone;
        this.Email = email;
        this.Adresse = adresse;
    }

}