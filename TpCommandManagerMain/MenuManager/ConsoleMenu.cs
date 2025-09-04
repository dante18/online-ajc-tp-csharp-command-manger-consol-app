namespace TpCommandManagerMain.MenuManager;

public static class ConsoleMenu
{
    public static void AfficherMenu()
    {
        Console.WriteLine($"Bienvenue dans l'application Eat domicile !\n");
        Console.WriteLine("1 : Gestion du catalogue");
        Console.WriteLine("2 : Gestion des commandes");
        Console.WriteLine("3 : Quitter l'application");
    }

    public static void AfficherGestionCatalogueMainMenu()
    {
        Console.WriteLine("\n1 : Ingrédient");
        Console.WriteLine("2 : Pate");
        Console.WriteLine("3 : Pizza");
        Console.WriteLine("4 : Pasta");
    }

    /*public static void AfficherGestionCatalogueSubMenu(string typeProduit)
    {
        Console.WriteLine("\nIngrédient");

        switch (typeProduit)
        {
            case "1":
                Console.WriteLine("2 : Pate");
                Console.WriteLine("3 : Pizza");
                Console.WriteLine("4 : Pasta");
                break;
            case "1":
                Console.WriteLine("2 : Pate");
                Console.WriteLine("3 : Pizza");
                Console.WriteLine("4 : Pasta");
                break;
            default:
                Console.WriteLine("6 : Retourner sur le menu principal");
        }
    }*/

    public static void AfficherGestionCommandeMenu()
    {
        Console.WriteLine("\nGestion des commandes");
        Console.WriteLine("1 : Lister les commandes");
        Console.WriteLine("2 : Consulter une commande");
        Console.WriteLine("3 : Ajouter une commande");
        Console.WriteLine("4 : Modifier une commande");
        Console.WriteLine("5 : Supprimer une commande"); 
        Console.WriteLine("6 : Retourner sur le menu principal");
    }
}
