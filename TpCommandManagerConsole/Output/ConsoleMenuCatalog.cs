using TpCommandManagerConsole.Input;
using TpCommandManagerConsole.Output;

namespace TpCommandManagerMain.MenuManager;

public sealed class ConsoleMenuCatalog
{
    public int StartMenu()
    {
        Console.Clear();

        int choix;

        do
        {
            ShowMenu();
            choix = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");
            Console.Clear();

            this.TreatChoice(choix);
        } while (choix != 7);

        return choix;
    }

    private void ShowMenu()
    {
        Console.WriteLine("\nGestion du menu");
        Console.WriteLine("1 : Gestion des Ingrédients");
        Console.WriteLine("2 : Gestion des Boissons");
        Console.WriteLine("3 : Gestion des Pâtes");
        Console.WriteLine("4 : Gestion des Burgers");
        Console.WriteLine("5 : Gestion des Pizzas");
        Console.WriteLine("6 : Gestion des Pastas");
        Console.WriteLine("7 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choix)
    {
        switch (choix)
        {
            case 1:
                ConsoleMenuCatalogIngredient cmIngredient = new ConsoleMenuCatalogIngredient();
                cmIngredient.StartMenu();
                break;
            case 2:
                ConsoleMenuCatalogDrink cmDrink = new ConsoleMenuCatalogDrink();
                cmDrink.StartMenu();
                break;
            case 3:
                ConsoleMenuCatalogDoughs cmDoughs = new ConsoleMenuCatalogDoughs();
                cmDoughs.StartMenu();
                break;
            case 4:
                ConsoleMenuCatalogBurger cmBurger = new ConsoleMenuCatalogBurger();
                cmBurger.StartMenu();
                break;
            case 5:
                ConsoleMenuCatalogPizza cmPizza = new ConsoleMenuCatalogPizza();
                cmPizza.StartMenu();
                break;
            case 6:
                ConsoleMenuCatalogPasta cmPasta = new ConsoleMenuCatalogPasta();
                cmPasta.StartMenu();
                break;
            case 7:
                break;
        }
    }
}

