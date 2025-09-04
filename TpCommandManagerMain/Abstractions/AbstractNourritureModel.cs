namespace TpCommandManagerMain.Abstractions;

public abstract class AbstractNourritureModel : AbstractProduitModel
{
    public bool Vegetarien { get; private set; }

    public AbstractNourritureModel(int id, string nom, float prix, bool vegetarien) : base(id, nom, prix)
    {
        this.Vegetarien = vegetarien;
    }
}