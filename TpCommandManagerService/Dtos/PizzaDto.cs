namespace TpCommandManagerService.Dtos;

public class PizzaDto
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public DoughsDto Doughs { get; set; }

    public bool Vegetarian { get; set; }

    public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
}
