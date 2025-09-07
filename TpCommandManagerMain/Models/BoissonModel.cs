using TpCommandManagerMain.Abstractions;

namespace TpCommandManagerMain.Models;

public class BoissonModel : AbstractProduitModel
{
    public int Id { get; private set; }

    public bool Petillant { get; private set; }

    public float Kcal { get; private set; }

    public BoissonModel(int id, string nom, float prix, bool petillant, float kcal) : base(id, nom, prix)
    {
        this.Id = id;
        this.Petillant = petillant;
        this.Kcal = kcal;
    }
}