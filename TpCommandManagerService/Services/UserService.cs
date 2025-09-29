using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class UserService
{
    private readonly UserRepository _repository;

    public UserService(CommandStoreContext context)
    {
        this._repository = new UserRepository(context);
    }

    public List<UserDto> GetAllUsers()
    {
        List<UserDto> users = this._repository.GetAllUsers().Select(u => new UserDto()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Phone = u.Phone,
            Mail = u.Mail,
            Address = new AddressDto()
            {
                Id = u.Address.Id,
                Street = u.Address.Street,
                City = u.Address.City,
                State = u.Address.State,
                Zip = u.Address.Zip,
                Country = u.Address.Country
            }
        }).ToList();

        return users;
    }

    public UserDto GetUser(int id)
    {
        User user = this._repository.GetUser(id);

        if (user is null)
        {
            return null;
        }

        UserDto userDto = new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Mail = user.Mail,
            Address = new AddressDto()
            {
                Id = user.Address.Id,
                Street = user.Address.Street,
                City = user.Address.City,
                State = user.Address.State,
                Zip = user.Address.Zip,
                Country = user.Address.Country
            }
        };

        return userDto;
    }

    public void CreateUser(UserDto user)
    {
        User newUser = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Mail = user.Mail,
            Address = new Address()
            {
                Street = user.Address.Street,
                City = user.Address.City,
                State = user.Address.State,
                Zip = user.Address.Zip,
                Country = user.Address.Country
            }
        };

        this._repository.CreateUser(newUser);
    }

    public void UpdateUser(int id, UserDto data)
    {
        User existingUser = this._repository.GetUser(id);

        existingUser.FirstName = data.FirstName;
        existingUser.LastName = data.LastName;
        existingUser.Phone = data.Phone;
        existingUser.Mail = data.Mail;
        existingUser.Address.Street = data.Address.Street;
        existingUser.Address.City = data.Address.City;
        existingUser.Address.State = data.Address.State;
        existingUser.Address.Zip = data.Address.Zip;
        existingUser.Address.Country = data.Address.Country;

        this._repository.UpdateUser(existingUser);
    }

    public void DeleteUser(int id)
    {
        User existingUser = this._repository.GetUser(id);
        this._repository.DeleteUser(existingUser);
    }
}
