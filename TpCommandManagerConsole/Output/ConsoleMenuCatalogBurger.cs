using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuCatalogBurger
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
        Console.WriteLine("1 : Liste des burgers");
        Console.WriteLine("2 : Chercher un burger");
        Console.WriteLine("3 : Créer un burger");
        Console.WriteLine("4 : Mettre à jour un burger");
        Console.WriteLine("5 : Supprimer un burger");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                GetAllBurgers();
                break;
            case 2:
                GetBurger();
                break;
            case 3:
                try
                {
                    CreateBurger();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                UpdateBurger();
                break;
            case 5:
                DeleteBurger();
                break;
            case 6:
                break;
        }
    }

    private void ShowBurger(BurgerDto burger)
    {
        if (burger == null)
        {
            throw new Exception("Ce burger n'existe pas");
        }
        else
        {
            Console.WriteLine($"#{burger.Id} - {burger.Name} - {burger.Price} euro - {(burger.Vegetarian ? "Végétarien" : "Non végétarien")}");
            Console.WriteLine(string.Join(", ", burger.Ingredients.Select(i => i.Name).ToList()));
            Console.WriteLine("");
        }
    }

    private void GetAllBurgers()
    {
        using var context = new CommandStoreContext();
        BurgerService burgerService = new BurgerService(context);
        List<BurgerDto> burgers = burgerService.GetAllBurgers();

        bool isEmpty = !burgers.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de burgers.");
        }
        else
        {
            foreach (var burger in burgers)
            {
                ShowBurger(burger);
            }
        }
    }

    private void GetBurger()
    {
        using var context = new CommandStoreContext();
        BurgerService burgerService = new BurgerService(context);

        List<BurgerDto> burgers = burgerService.GetAllBurgers();

        bool isEmpty = !burgers.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de burgers.");
        }
        else
        {
            try
            {
                GetAllBurgers();

                BurgerDto burger = burgerService.GetBurger(GetUserEntry.GetEntier("Quelle burger voulez vous regarder ?"));
                ShowBurger(burger);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void CreateBurger()
    {
        Console.WriteLine("\nAjouter un burger");

        using var context = new CommandStoreContext();

        string name = GetUserEntry.GetString("Saissez le nom du burger");
        decimal price = GetUserEntry.GetDecimal("Quel sera le prix du burger ?");
        bool vegetarian = GetUserEntry.GetBool("Ce burger est-il végétarien ? (O/N)");

        List<IngredientDto> ingredients = new List<IngredientDto>();
        int ingredientChoice = 0;
        bool ingredientOtherChoice = true;
        do
        {
            Console.WriteLine("Ajouter un ingrédient :");

            string ingredientName = GetUserEntry.GetString("Quel est le nom de l'ingrédient ?");
            decimal kCal = GetUserEntry.GetDecimal("Combient de KCal vaut l'ingrédient ?");
            bool isAllergen = GetUserEntry.GetBool("Cet ingrédient est-il un allergène ? (O/N)");

            IngredientDto ingredient = new IngredientDto()
            {
                Name = ingredientName,
                KCal = kCal,
                IsAllergen = isAllergen
            };
            ingredients.Add(ingredient);

            if (!GetUserEntry.GetBool("Souhaitez-vous ajouter un autre ingrédient ? (O/N)"))
            {
                ingredientOtherChoice = false;
            }
        } while (ingredientOtherChoice == true);

        BurgerService burgerService = new BurgerService(context);
        burgerService.CreateBurger(new BurgerDto()
        {
            Name = name,
            Price = price,
            Vegetarian = vegetarian,
            Ingredients = ingredients
        });
    }

    private void UpdateBurger()
    {
        using var context = new CommandStoreContext();
        BurgerService burgerService = new BurgerService(context);
        
        try
        {
            GetAllBurgers();

            int burgerId = GetUserEntry.GetEntier("Quelle burger voulez vous modifier ?");
            BurgerDto burger = burgerService.GetBurger(burgerId);
            ShowBurger(burger);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine("1 : Nom");
            Console.WriteLine("2 : Prix");
            Console.WriteLine("3 : Revenir au menu précédent");
            int choice = GetUserEntry.GetEntier("\n");

            switch (choice)
            {
                case 1:
                    string newName = GetUserEntry.GetString("Quel sera le nouveau nom du burger ?");
                    burger.Name = newName;
                    break;

                case 2:
                    decimal newPrice = GetUserEntry.GetDecimal("Quel sera le nouveau prix du burger ?");
                    burger.Price = newPrice;
                    break;

                default:
                    break;
            }

            burgerService.UpdateBurger(burgerId, burger);
            ShowBurger(burger);
        }
        catch
        {
            Console.WriteLine("Ce burger n'existe pas");
        }
    }

    private void DeleteBurger()
    {
        using var context = new CommandStoreContext();
        BurgerService burgerService = new BurgerService(context);
        
        try
        {
            GetAllBurgers();

            int burgerId = GetUserEntry.GetEntier("Quelle burger souhaitez vous supprimer ?");
            BurgerDto burger = burgerService.GetBurger(burgerId);
            ShowBurger(burger);

            bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer ce burger ? (O/N) ");
            if (choice)
            {
                burgerService.DeleteBurger(burgerId);
            }
        }
        catch
        {
            Console.WriteLine("Ce burger n'existe pas");
        }
    }
}
