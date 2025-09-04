namespace TpCommandManagerMain.Models;

public class IngredientModel
{
    public int? Id { get; private set; }

    public string Nom { get; private set; }

    public float Kcal { get; private set; }

    public bool EstAllergene { get; private set; }

    public IngredientModel(int id, string nom, float kcal, bool estAllergene)
    {
        this.Id = id;
        this.Nom = nom;
        this.Kcal = kcal;
        this.EstAllergene = estAllergene;
    }
}