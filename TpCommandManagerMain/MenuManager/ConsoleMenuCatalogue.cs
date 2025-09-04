namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCatalogue
{
    public int DemarrerLeMemu()
    {
        int choix;

        do
        {
            AfficherMenu();
            choix = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");
            Console.Clear();

            this.TraiterChoix(choix);
        } while (choix != 6);

        return choix;
    }

    private void AfficherMenu()
    {
        Console.WriteLine("\nGestion du catalogue");
        Console.WriteLine("1 : Gestion des Ingrédients");
        Console.WriteLine("2 : Gestion des Boissons");
        Console.WriteLine("3 : Gestion des Pâtes");
        Console.WriteLine("4 : Gestion des Burgers");
        Console.WriteLine("5 : Gestion des Pizzas");
        Console.WriteLine("6 : Gestion des Pastas");
        Console.WriteLine("7 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                ConsoleMenuCatalogueIngredient cmIngredient = new ConsoleMenuCatalogueIngredient();
                cmIngredient.DemarrerLeMemu();
                break;
            case 2:
                Console.WriteLine("Lister des boissons");
                break;
            case 3:
                ConsoleMenuCataloguePate cmPate = new ConsoleMenuCataloguePate();
                cmPate.DemarrerLeMemu();
                break;
            case 4:
                Console.WriteLine("Lister des burgers");
                break;
            case 5:
                ConsoleMenuCataloguePizza cmPizza = new ConsoleMenuCataloguePizza();
                cmPizza.DemarrerLeMemu();
                break;
            case 6:
                Console.WriteLine("Lister des pastas");
                break;
            case 7:
                break;
        }
    }
}

