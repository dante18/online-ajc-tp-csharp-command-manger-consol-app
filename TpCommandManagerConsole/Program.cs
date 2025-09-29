using TpCommandManagerConsole.Input;
using TpCommandManagerConsole.Output;
using TpCommandManagerData.Context;
using TpCommandManagerMain.MenuManager;
using TpCommandManagerService.Seeders;

using (var context = new CommandStoreContext())
{
    DatabaseSeeder.SeedDevData(context);
}

int choiceMenu;

do
{
    Console.WriteLine("Bienvenue dans l'application Eat domicile !\n");
    Console.WriteLine("1 : Gestion du menu");
    Console.WriteLine("2 : Gestion des utilisateurs");
    Console.WriteLine("3 : Gestion des commandes");
    Console.WriteLine("4 : Requêtes");
    Console.WriteLine("5 : Quitter l'application");

    choiceMenu = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");

    switch (choiceMenu)
    {
        case 1:
            ConsoleMenuCatalog catalogManager = new ConsoleMenuCatalog();
            choiceMenu = catalogManager.StartMenu();
            break;

        case 2:
            ConsoleMenuUser userMenu = new ConsoleMenuUser();
            choiceMenu = userMenu.StartMenu();
            break;

        case 3:
            ConsoleMenuOrder orderMenu = new ConsoleMenuOrder();
            choiceMenu = orderMenu.StartMenu();
            break;

        case 4:
            ConsoleMenuQuery queryMenu = new ConsoleMenuQuery();
            choiceMenu = queryMenu.StartMenu();
            break;

        case 5:
            break;

        default:
            Console.WriteLine("Action non prise en charge ou non autorisée");
            break;
    }
} while (choiceMenu != 4);