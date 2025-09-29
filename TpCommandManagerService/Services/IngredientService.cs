using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class IngredientService
{
    private readonly IngredientRepository _repository;

    public IngredientService(CommandStoreContext context)
    {
        this._repository = new IngredientRepository(context);
    }

    public List<IngredientDto> GetAllIngredients()
    {
        List<IngredientDto> ingredients = this._repository.GetAllIngredients().Select(i => new IngredientDto()
        {
            Id = i.Id,
            Name = i.Name,
            KCal = i.KCal,
            IsAllergen = i.IsAllergen
        }).ToList();

        return ingredients;
    }

    public IngredientDto GetIngredient(int id)
    {
        Ingredient ingredient = this._repository.GetIngredient(id);

        if (ingredient is null)
        {
            return null;
        }

        IngredientDto ingredientDto = new IngredientDto()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            KCal = ingredient.KCal,
            IsAllergen = ingredient.IsAllergen
        };

        return ingredientDto;
    }

    public void CreateIngredient(IngredientDto ingredient)
    {
        Ingredient newIngredient = new Ingredient()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            KCal = ingredient.KCal,
            IsAllergen = ingredient.IsAllergen
        };

        this._repository.CreateIngredient(newIngredient);
    }

    public void UpdateIngredient(int id, IngredientDto data)
    {
        Ingredient existingIngredient = this._repository.GetIngredient(id);
        existingIngredient.Name = data.Name;
        existingIngredient.KCal = data.KCal;

        this._repository.UpdateIngredient(existingIngredient);
    }

    public void DeleteIngredient(int id)
    {
        Ingredient existingIngredient = this._repository.GetIngredient(id);
        this._repository.DeleteIngredient(existingIngredient);
    }
}

