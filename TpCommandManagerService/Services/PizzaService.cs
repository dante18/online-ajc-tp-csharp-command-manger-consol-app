using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class PizzaService
{
    private readonly PizzaRepository _repository;

    public PizzaService(CommandStoreContext context)
    {
        this._repository = new PizzaRepository(context);
    }

    public List<PizzaDto> GetAllPizzas()
    {
        List<PizzaDto> pizzas = this._repository.GetAllPizzas().Select(p => new PizzaDto()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Doughs = new DoughsDto()
            {
                Id = p.Doughs.Id ?? 0,
                Name = p.Doughs.Name,
            },
            Vegetarian = p.Vegetarian,
            Ingredients = p.Ingredients.Select(i => new IngredientDto()
            {
                Id = i.Id,
                Name = i.Name,
                IsAllergen = i.IsAllergen,
                KCal = i.KCal
            }).ToList()
        }).ToList();

        return pizzas;
    }

    public PizzaDto GetPizza(int id)
    {
        Pizza pizza = this._repository.GetPizza(id);

        if (pizza is null)
        {
            return null;
        }

        PizzaDto pizzaDto = new PizzaDto()
        {
            Id = pizza.Id,
            Name = pizza.Name,
            Price = pizza.Price,
            Doughs = new DoughsDto()
            {
                Id = pizza.Doughs.Id ?? 0,
                Name = pizza.Doughs.Name,
            },
            Vegetarian = pizza.Vegetarian,
            Ingredients = pizza.Ingredients.Select(i => new IngredientDto()
            {
                Id = i.Id,
                Name = i.Name,
                IsAllergen = i.IsAllergen,
                KCal = i.KCal
            }).ToList()
        };

        return pizzaDto;
    }

    public void CreatePizza(PizzaDto pizza)
    {
        Pizza newPizza = new Pizza()
        {
            Id = pizza.Id,
            Name = pizza.Name,
            Price = pizza.Price,
            DoughsId = pizza.Doughs.Id,
            Vegetarian = pizza.Vegetarian,
            Ingredients = pizza.Ingredients.Select(i => new Ingredient()
            {
                Id = i.Id,
                Name = i.Name,
                IsAllergen = i.IsAllergen,
                KCal = i.KCal
            }).ToList()
        };

        this._repository.CreatePizza(newPizza);
    }

    public void UpdatePizza(int id, PizzaDto data)
    {
        Pizza existingPizza = this._repository.GetPizza(id);
        existingPizza.Name = data.Name;
        existingPizza.Price = data.Price;
        existingPizza.DoughsId = data.Doughs.Id;

        this._repository.UpdatePizza(existingPizza);
    }

    public void DeletePizza(int id)
    {
        Pizza existingPizza = this._repository.GetPizza(id);
        this._repository.DeletePizza(existingPizza);
    }
}
