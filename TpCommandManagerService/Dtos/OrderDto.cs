namespace TpCommandManagerService.Dtos;

public class OrderDto
{
    public int? Id { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int Status { get; set; }

    public UserDto User { get; set; }

    public AddressDto DeliveryAddress { get; set; }

    public List<OrderProductDto> Products { get; set; } = new List<OrderProductDto>();
}