namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCommande
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
        Console.WriteLine("\nGestion des commandes");
        Console.WriteLine("1 : Lister les commandes");
        Console.WriteLine("2 : Consulter une commande");
        Console.WriteLine("3 : Ajouter une commande");
        Console.WriteLine("4 : Modifier une commande");
        Console.WriteLine("5 : Supprimer une commande");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                Console.WriteLine("Lister les commandes");
                break;
            case 2:
                Console.WriteLine("Consulter une commande");
                break;
            case 3:
                Console.WriteLine("Ajouter une commande");
                break;
            case 4:
                Console.WriteLine("Modifier une commande");
                break;
            case 5:
                Console.WriteLine("Supprimer une commande");
                break;
            case 6:
                break;
        }
    }
}

