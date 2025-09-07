using TpCommandManagerMain.MenuManager;

int menuChoix;

do
{
    Console.WriteLine($"Bienvenue dans l'application Eat domicile !\n");
    Console.WriteLine("1 : Gestion du menu");
    Console.WriteLine("2 : Gestion des utilisateurs");
    Console.WriteLine("3 : Gestion des commandes");
    Console.WriteLine("4 : Requêtes");
    Console.WriteLine("5 : Quitter l'application");

    menuChoix = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");

    switch (menuChoix)
    {
        case 1:
            ConsoleMenuCatalogue catalogueManager = new ConsoleMenuCatalogue();
            menuChoix = catalogueManager.DemarrerLeMemu();
            break;

        case 2:
            ConsoleMenuUtilisateur utilisateurManager = new ConsoleMenuUtilisateur();
            menuChoix = utilisateurManager.DemarrerLeMemu();
            break;

        case 3:
            ConsoleMenuCommande commandeManager = new ConsoleMenuCommande();
            menuChoix = commandeManager.DemarrerLeMemu();
            break;

        case 4:
            ConsoleMenuRequete requeteMenu = new ConsoleMenuRequete();
            menuChoix = requeteMenu.DemarrerLeMemu();
            break;

        case 5:
            break;

        default:
            Console.WriteLine("Action non prise en charge ou non autorisée");
            break;
    }
} while (menuChoix != 4);