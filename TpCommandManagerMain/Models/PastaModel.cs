
using TpCommandManagerMain.Abstractions;

namespace TpCommandManagerMain.Models;


public class PastaModel : AbstractNourritureModel
{
    public int Type { get; private set; }

    public float Kcal { get; private set; }

    public PastaModel(int id, string nom, float prix, bool vegetarien, int type, float kcal) : base(id, nom, prix, vegetarien)
    {
        this.Type = type;
        this.Kcal = kcal;
    }
}