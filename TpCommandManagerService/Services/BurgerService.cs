using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class BurgerService
{
    private readonly BurgerRepository _repository;

    public BurgerService(CommandStoreContext context)
    {
        this._repository = new BurgerRepository(context);
    }

    public List<BurgerDto> GetAllBurgers()
    {
        List<BurgerDto> burgers = this._repository.GetAllBurgers().Select(b => new BurgerDto()
        { 
            Id=b.Id,
            Vegetarian= b.Vegetarian,
            Name=b.Name,
            Price=b.Price,
            Ingredients = b.Ingredients.Select(i => new IngredientDto()
            {
                Id = i.Id,
                Name = i.Name,
                KCal = i.KCal,
                IsAllergen = i.IsAllergen
            }).ToList()
        }).ToList();

        return burgers;
    }

    public BurgerDto GetBurger(int id)
    {
        Burger burger = this._repository.GetBurger(id);

        if (burger is null)
        {
            return null;
        }

        BurgerDto burgerDto = new BurgerDto()
        {
            Id = burger.Id,
            Vegetarian = burger.Vegetarian,
            Name = burger.Name,
            Price = burger.Price,
            Ingredients = burger.Ingredients.Select(i => new IngredientDto()
            {
                Id = i.Id,
                Name = i.Name,
                KCal = i.KCal,
                IsAllergen = i.IsAllergen
            }).ToList()
        };

        return burgerDto;
    }

    public void CreateBurger(BurgerDto burger)
    {
        Burger newBurger = new Burger()
        {
            Id = burger.Id,
            Vegetarian = burger.Vegetarian,
            Name = burger.Name,
            Price = burger.Price,
            Ingredients = burger.Ingredients.Select(i => new Ingredient()
            {
                Id = i.Id,
                Name = i.Name,
                KCal = i.KCal,
                IsAllergen = i.IsAllergen
            }).ToList()
        };

        this._repository.CreateBurger(newBurger);
    }

    public void UpdateBurger(int id, BurgerDto data)
    {
        Burger existingBurger = this._repository.GetBurger(id);

        existingBurger.Name = data.Name;
        existingBurger.Price = data.Price;
        
        this._repository.UpdateBurger(existingBurger);
    }

    public void DeleteBurger(int id)
    {
        Burger existingBurger = this._repository.GetBurger(id);
        this._repository.DeleteBurger(existingBurger);
    }
}
