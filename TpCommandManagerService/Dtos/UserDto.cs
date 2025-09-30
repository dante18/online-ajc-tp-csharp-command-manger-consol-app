namespace TpCommandManagerService.Dtos;

public class UserDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Mail { get; set; }

    public AddressDto Address { get; set; }
}