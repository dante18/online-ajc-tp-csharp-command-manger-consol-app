namespace TpCommandManagerService.Dtos;

public class PastaDto
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Type { get; set; }

    public decimal KCal { get; set; }

    public bool Vegetarian { get; set; }
}