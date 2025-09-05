using TpCommandManagerMain.MenuManager;

int menuChoix;

do
{
    Console.WriteLine($"Bienvenue dans l'application Eat domicile !\n");
    Console.WriteLine("1 : Gestion du menu");
    Console.WriteLine("2 : Gestion des commandes");
    Console.WriteLine("3 : Quitter l'application");

    menuChoix = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");

    switch (menuChoix)
    {
        case 1:
            ConsoleMenuCatalogue catalogueManager = new ConsoleMenuCatalogue();
            menuChoix = catalogueManager.DemarrerLeMemu();
            break;

        case 2:
            ConsoleMenuCommande commandeManager = new ConsoleMenuCommande();
            menuChoix = commandeManager.DemarrerLeMemu();
            break;

        case 3:
            break;

        default:
            Console.WriteLine("Action non prise en charge ou non autorisée");
            break;
    }
} while (menuChoix != 3);