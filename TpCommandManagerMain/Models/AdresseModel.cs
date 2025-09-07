using System.ComponentModel.DataAnnotations;

namespace TpCommandManagerMain.Models;

public class AdresseModel
{
    public int Id { get; private set; }

    public string Rue { get; private set; }

    public string CodePostal { get; private set; }

    public string Ville { get; private set; }

    public string Region { get; private set; }

    public string Pays { get; private set; }

    public AdresseModel(int id, string rue, string codePostal, string ville, string region, string pays)
    {
        this.Id = id;
        this.Rue = rue;
        this.CodePostal = codePostal;
        this.Ville = ville;
        this.Region = region;
        this.Pays = pays;
    }
}