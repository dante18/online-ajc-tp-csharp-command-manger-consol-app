namespace TpCommandManagerService.Dtos;

public class IngredientDto
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public decimal KCal { get; set; }

    public bool IsAllergen { get; set; }
}