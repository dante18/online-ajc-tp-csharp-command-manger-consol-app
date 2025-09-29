using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuCatalogDrink
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
        Console.WriteLine("1 : Liste des boissons");
        Console.WriteLine("2 : Chercher une boisson");
        Console.WriteLine("3 : Créer une boisson");
        Console.WriteLine("4 : Mettre à jour une boisson");
        Console.WriteLine("5 : Supprimer une boisson");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                GetAllDrinks();
                break;
            case 2:
                GetDrink();
                break;
            case 3:
                CreateDrink();
                break;
            case 4:
                UpdateDrink();
                break;
            case 5:
                DeleteDrink();
                break;
            case 6:
                break;
        }
    }

    private void ShowDrink(DrinkDto drink)
    {
        if (drink is null)
        {
            throw new Exception("Cette boisson n'existe pas");
        }
        else
        {
            string fizzy = drink.Fizzy ? "Pétillant" : "Non pétillant";

            Console.WriteLine($"#{drink.Id} : {drink.Name} - {fizzy} - {drink.Price} Euros - {drink.KCal} kCal");
            Console.WriteLine("");
        }
    }

    private void GetAllDrinks()
    {
        using var context = new CommandStoreContext();
        DrinkService drinkService = new DrinkService(context);
        List<DrinkDto> drinks = drinkService.GetAllDrinks();

        bool isEmpty = !drinks.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de boissons.");
        }
        else
        {
            foreach (var drink in drinks)
            {
                ShowDrink(drink);
            }
        }
    }

    private void GetDrink()
    {
        using var context = new CommandStoreContext();
        DrinkService drinkService = new DrinkService(context);

        List<DrinkDto> drinks = drinkService.GetAllDrinks();

        bool isEmpty = !drinks.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de boissons.");
        }
        else
        {
            try
            {
                GetAllDrinks();

                DrinkDto drink = drinkService.GetDrink(GetUserEntry.GetEntier("Quelle boisson voulez vous regarder ?"));
                ShowDrink(drink);
            }
            catch
            {
                Console.WriteLine("Cette boisson n'existe pas");
            }
        }
    }

    private void CreateDrink()
    {
        using var context = new CommandStoreContext();

        string name = GetUserEntry.GetString("Quel nom voulez vous donner à la boisson ?");
        decimal price = GetUserEntry.GetDecimal("Quel prix voulez vous donner à la boisson ?");
        bool isFizzy = GetUserEntry.GetBool("Est-ce que la boisson est pétillante ? (O/N)");
        decimal kCal = GetUserEntry.GetDecimal("Combien de kCal contient cette boisson ?");

        DrinkDto drink = new DrinkDto()
        {
            Name = name,
            Price = price,
            Fizzy = isFizzy,
            KCal = kCal
        };
        DrinkService drinkService = new DrinkService(context);
        drinkService.CreateDrink(drink);
    }

    private void UpdateDrink()
    {
        using var context = new CommandStoreContext();
        DrinkService drinkService = new DrinkService(context);

        try
        {
            GetAllDrinks();

            int drinkId = GetUserEntry.GetEntier("Quelle boisson voulez vous modifier ?");
            DrinkDto drink = drinkService.GetDrink(drinkId);
            ShowDrink(drink);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine("1 : Nom");
            Console.WriteLine("2 : Prix");
            Console.WriteLine("3 : Pétillant");
            Console.WriteLine("4 : kCal");
            Console.WriteLine("5 : Revenir au menu précédent");
            int choice = GetUserEntry.GetEntier("");

            switch (choice)
            {
                case 1:
                    string newName = GetUserEntry.GetString("Quel sera le nouveau nom de la boisson ?");
                    drink.Name = newName;
                    break;

                case 2:
                    decimal newPrice = GetUserEntry.GetDecimal("Quel sera le nouveau prix de la boisson ?");
                    drink.Price = newPrice;
                    break;

                case 3:
                    bool isFizzy = GetUserEntry.GetBool("Est-ce que la boisson est pétillante ? (O/N)");
                    drink.Fizzy = isFizzy;
                    break;

                case 4:
                    decimal newKCal = GetUserEntry.GetDecimal("Combien de kCal contient cette boisson ?");
                    drink.KCal = newKCal;
                    break;

                default:
                    break;
            }

            drinkService.UpdateDrink(drinkId, drink);
            ShowDrink(drink);
        }
        catch
        {
            Console.WriteLine("Cette boisson n'existe pas");
        }
    }

    private void DeleteDrink()
    {
        using var context = new CommandStoreContext();
        DrinkService drinkService = new DrinkService(context);

        GetAllDrinks();

        try
        {
            int drinkId = GetUserEntry.GetEntier("Quelle boisson souhaitez vous supprimer ?");
            DrinkDto drink = drinkService.GetDrink(drinkId);
            ShowDrink(drink);

            bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer cette boisson ? (O/N) ?");

            if (choice)
            {
                drinkService.DeleteDrink(drinkId);
            }
        }
        catch
        {
            Console.WriteLine("Cette boisson n'existe pas");
        }
    }
}
