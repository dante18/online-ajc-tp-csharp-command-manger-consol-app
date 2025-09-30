using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuCatalogIngredient
{
    public int StartMenu()
    {
        Console.Clear();

        int choice;

        do
        {
            ShowMenu();
            choice = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");
            Console.Clear();

            TreatChoice(choice);
        } while (choice != 6);

        return choice;
    }

    private void ShowMenu()
    {
        Console.WriteLine("\nGestion du menu");
        Console.WriteLine("1 : Liste des ingrédients");
        Console.WriteLine("2 : Chercher un ingrédient");
        Console.WriteLine("3 : Créer un ingrédient");
        Console.WriteLine("4 : Mettre à jour un ingrédient");
        Console.WriteLine("5 : Supprimer un ingrédient");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                GetAllIngredients();
                break;
            case 2:
                GetIngredient();
                break;
            case 3:
                CreateIngredient();
                break;
            case 4:
                UpdateIngredient();
                break;
            case 5:
                DeleteIngredient();
                break;
            case 6:
                break;
        }
    }

    private void ShowIngredient(IngredientDto ingredient)
    {
        if (ingredient != null)
        {
            string allergen = ingredient.IsAllergen ? "Oui" : "Non";
            Console.WriteLine($"#{ingredient.Id} - {ingredient.Name} - {ingredient.KCal} - allergene: {allergen}");
        }
        else
        {
            throw new Exception("Cette ingrédient n'existe pas");
        }
    }

    private void GetAllIngredients()
    {
        using var context = new CommandStoreContext();
        IngredientService ingredientService = new IngredientService(context);
        List<IngredientDto> ingredients = ingredientService.GetAllIngredients();

        bool isEmpty = !ingredients.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas d'ingrédients.");
        }
        else
        {
            foreach (var ingredient in ingredients)
            {
                ShowIngredient(ingredient);
            }
        }
    }

    private void GetIngredient()
    {
        using var context = new CommandStoreContext();
        IngredientService ingredientService = new IngredientService(context);

        List<IngredientDto> ingredients = ingredientService.GetAllIngredients();

        bool isEmpty = !ingredients.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas d'ingrédients.");
        }
        else
        {
            try
            {
                GetAllIngredients();

                IngredientDto ingredient = ingredientService.GetIngredient(GetUserEntry.GetEntier("Quelle ingrédient voulez vous regarder ?"));
                ShowIngredient(ingredient);
            }
            catch
            {
                Console.WriteLine("Cette ingrédient n'existe pas");
            }
        }
    }

    private void CreateIngredient()
    {
        using var context = new CommandStoreContext();

        Console.WriteLine("\nAjouter un ingrédient");

        string name = GetUserEntry.GetString("Quel est le nom de l'ingrédient ?");
        decimal kCal = GetUserEntry.GetDecimal("Combient de KCal vaut l'ingrédient ?");
        bool isAllergen = GetUserEntry.GetBool("Cet ingrédient est-il un allergène ? (O/N)");

        IngredientDto ingredient = new IngredientDto()
        {
            Name = name,
            KCal = kCal,
            IsAllergen = isAllergen
        };
        IngredientService ingredientService = new IngredientService(context);
        ingredientService.CreateIngredient(ingredient);
    }

    private void UpdateIngredient()
    {
        using var context = new CommandStoreContext();
        IngredientService ingredientService = new IngredientService(context);

        try
        {
            GetAllIngredients();

            int ingredientId = GetUserEntry.GetEntier("Quelle ingrédient voulez vous modifier ?");
            IngredientDto ingredient = ingredientService.GetIngredient(ingredientId);
            ShowIngredient(ingredient);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine("1 : Nom");
            Console.WriteLine("2 : KCal");
            Console.WriteLine("3 : Revenir au menu précédent");
            int choice = GetUserEntry.GetEntier("\n");

            switch (choice)
            {
                case 1:
                    string newName = GetUserEntry.GetString("Saisissez le nouveau nom de l'ingédient ?");
                    ingredient.Name = newName;
                    break;
                case 2:
                    decimal newKCal = GetUserEntry.GetDecimal("Saisissez le nombre de KCal ?");
                    ingredient.KCal = newKCal;
                    break;
                default:
                    break;
            }

            ingredientService.UpdateIngredient(ingredientId, ingredient);
            ShowIngredient(ingredient);
        }
        catch
        {
            Console.WriteLine("Cette ingrédient n'existe pas");
        }
    }

    private void DeleteIngredient()
    {
        using var context = new CommandStoreContext();
        IngredientService ingredientService = new IngredientService(context);

        try
        {
            GetAllIngredients();

            int ingredientId = GetUserEntry.GetEntier("Quelle ingrédient voulez vous supprimer ?");
            IngredientDto ingredient = ingredientService.GetIngredient(ingredientId);
            ShowIngredient(ingredient);

            bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer cette ingrédient (O/N) ?");
            if (choice)
            {
                ingredientService.DeleteIngredient(ingredientId);
            }
        }
        catch
        {
            Console.WriteLine("Cette ingrédient n'existe pas");
        }
    }
}
