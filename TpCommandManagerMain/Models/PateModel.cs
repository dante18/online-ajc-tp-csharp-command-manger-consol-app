namespace TpCommandManagerMain.Models;

public class PateModel
{
    public int? Id { get; private set; }

    public string Nom { get; private set; }

    public PateModel(int id, string nom)
    {
        this.Id = id;
        this.Nom = nom;
    }
}