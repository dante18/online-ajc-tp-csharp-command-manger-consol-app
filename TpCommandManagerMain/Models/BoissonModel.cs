namespace TpCommandManagerMain.Models;

public class BoissonModel
{
    public int Id { get; private set; }

    public bool Petillant { get; private set; }

    public float Kcal { get; private set; }

    public BoissonModel(int id, bool petillant, float kcal)
    {
        this.Id = id;
        this.Petillant = petillant;
        this.Kcal = kcal;
    }
}