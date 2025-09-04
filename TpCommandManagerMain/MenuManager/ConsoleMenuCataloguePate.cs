using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCataloguePate
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
        Console.WriteLine("1 : Liste des pates");
        Console.WriteLine("2 : Chercher une pate");
        Console.WriteLine("3 : Créer une pate");
        Console.WriteLine("4 : Mettre à jour une pate");
        Console.WriteLine("5 : Supprimer une pate");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                //
                break;
            case 2:
                Console.WriteLine();
                //ObtenirPate();
                break;
            case 3:
                AjouterPate();
                break;
            case 4:
                Console.WriteLine();
                //supprimerPate();
                break;
            case 5:
                break;
        }
    }

    private void AjouterPate()
    {
        using var context = new TpCommandManagerContext();

        string nom = GetUserEntry.GetString("Quel nom voulez vous donner à la pâte ?");
        Pate pate = new Pate(0, nom);
        PateManager pateManager = new PateManager(context);
        pateManager.AjouterPate(pate);
    }
}

