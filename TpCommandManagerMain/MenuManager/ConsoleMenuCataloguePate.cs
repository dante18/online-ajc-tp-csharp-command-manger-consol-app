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
        Console.WriteLine("\nGestion du menu");
        Console.WriteLine("1 : Liste des pâtes");
        Console.WriteLine("2 : Chercher une pâte");
        Console.WriteLine("3 : Créer une pâte");
        Console.WriteLine("4 : Mettre à jour une pâte");
        Console.WriteLine("5 : Supprimer une pâte");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                ObtenirListPate();
                break;
            case 2:
                ObtenirPate();
                break;
            case 3:
                AjouterPate();
                break;
            case 4:
                MiseAJourPate();
                break;
            case 5:
                SupprimerPate();
                break;
            case 6:
                break;
        }
    }

    private void AfficherPate(Pate pate)
    {
        if (pate != null)
        {
            Console.WriteLine($"#{pate.Id} - {pate.Nom}");
        }
        else
        {
            throw new Exception("Cette pâte n'existe pas");
        }
    }

    private void ObtenirListPate()
    {
        using var context = new TpCommandManagerContext();
        PateManager pateManager = new PateManager(context);
        List<Pate> pates = pateManager.ObtenirListPate();

        bool isEmpty = !pates.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de pâtes.");
        }
        else
        {
            foreach (var pate in pates)
            {
                AfficherPate(pate);
            }
        }
    }

    private void ObtenirPate()
    {
        using var context = new TpCommandManagerContext();
        PateManager pateManager = new PateManager(context);

        try
        {
            Pate pate = pateManager.ObtenirPate(GetUserEntry.GetEntier("Quelle pâte voulez vous regarder ?"));
            AfficherPate(pate);
        }
        catch
        {
            Console.WriteLine("Cette pâte n'existe pas");
        }
    }

    private void AjouterPate()
    {
        using var context = new TpCommandManagerContext();

        string nom = GetUserEntry.GetString("Quel nom voulez vous donner à la pâte ?");
        Pate pate = new Pate(nom);
        PateManager pateManager = new PateManager(context);
        pateManager.AjouterPate(pate);
    }

    private void MiseAJourPate()
    {
        using var context = new TpCommandManagerContext();
        PateManager pateManager = new PateManager(context);

        try
        {
            ObtenirListPate();

            Pate pate = pateManager.ObtenirPate(GetUserEntry.GetEntier("Quelle pâte voulez vous modifier ?"));
            AfficherPate(pate);

            string nom = GetUserEntry.GetString("Saisissez le nouveau nom de la pâte ?");

            pate.Nom = nom;
            pateManager.MiseAJourPate(pate);
        }
        catch
        {
            Console.WriteLine("Cette pâtes n'existe pas");
        }
    }

    private void SupprimerPate()
    {
        using var context = new TpCommandManagerContext();
        PateManager pateManager = new PateManager(context);

        try
        {
            ObtenirListPate();

            Pate pate = pateManager.ObtenirPate(GetUserEntry.GetEntier("Quelle pâte voulez vous supprimer ?"));
            AfficherPate(pate);

            string choix = GetUserEntry.GetString($"\nÊtes vous sûr de vouloir supprimer cette pâte (O/N) ?");
            if (choix.ToUpper() == "O")
            {
                pateManager.SupprimerPate(pate);
            }
        }
        catch
        {
            Console.WriteLine("Cette pâte n'existe pas");
        }
    }
}

