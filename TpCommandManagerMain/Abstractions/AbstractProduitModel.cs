namespace TpCommandManagerMain.Abstractions;

public abstract class AbstractProduitModel
{
    public int Id { get; private set; }

    public string Nom { get; private set; }

    public float Prix { get; private set; }

    public AbstractProduitModel(int id, string nom, float prix)
    {
        this.Id = id;
        this.Nom = nom;
        this.Prix = prix;
    }

}