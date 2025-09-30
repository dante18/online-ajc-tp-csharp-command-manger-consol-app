namespace TpCommandManagerService.Dtos;

public class BurgerDto
{
    public int? Id { get; set; }

    public bool Vegetarian { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
}