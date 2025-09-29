using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class DrinkService
{
    private readonly DrinkRepository _repository;

    public DrinkService(CommandStoreContext context)
    {
        this._repository = new DrinkRepository(context);
    }

    public List<DrinkDto> GetAllDrinks()
    {
        List<DrinkDto> drinks = this._repository.GetAllDrinks().Select(d => new DrinkDto()
        {
            Id = d.Id,
            Name = d.Name,
            Fizzy = d.Fizzy,
            KCal = d.KCal,
            Price = d.Price
        }).ToList();

        return drinks;
    }

    public DrinkDto GetDrink(int id)
    {
        Drink drink = this._repository.GetDrink(id);

        if (drink is null)
        {
            return null;
        }

        DrinkDto drinkDto = new DrinkDto()
        {
            Id = drink.Id,
            Name = drink.Name,
            Fizzy = drink.Fizzy,
            KCal = drink.KCal,
            Price = drink.Price
        };

        return drinkDto;
    }

    public void CreateDrink(DrinkDto drink)
    {
        Drink newDrink = new Drink()
        {
            Fizzy = drink.Fizzy,
            Price = drink.Price,
            KCal = drink.KCal,
            Name = drink.Name
        };

        this._repository.CreateDrink(newDrink);
    }

    public void UpdateDrink(int id, DrinkDto data)
    {
       Drink existingDrink = this._repository.GetDrink(id);

       existingDrink.Name = data.Name;
       existingDrink.Price = data.Price;
       existingDrink.Fizzy = data.Fizzy;
       existingDrink.KCal = data.KCal;

       this._repository.UpdateDrink(existingDrink);
    }

    public void DeleteDrink(int id)
    {
        Drink existingDrink = this._repository.GetDrink(id);
        this._repository.DeleteDrink(existingDrink);
    }
}
