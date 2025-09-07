using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCataloguePasta
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
        Console.WriteLine("1 : Liste des pastas");
        Console.WriteLine("2 : Chercher une pasta");
        Console.WriteLine("3 : Créer une pasta");
        Console.WriteLine("4 : Mettre à jour une pasta");
        Console.WriteLine("5 : Supprimer une pasta");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                ObtenirListPasta();
                break;
            case 2:
                ObtenirPasta();
                break;
            case 3:
                AjouterPasta();
                break;
            case 4:
                MiseAJourPasta();
                break;
            case 5:
                SupprimerPasta();
                break;
            case 6:
                break;
        }
    }

    private void Afficher(Pasta pasta)
    {
        if(pasta is null)
        {
            throw new Exception("Cette pasta n'existe pas");
        }
        else
        {
            string vegetarien = pasta.Vegetarien ? "Oui" : "Non";

            Console.WriteLine($"#{pasta.Id} - {pasta.Nom} - {pasta.Prix} - {pasta.Type} - vegetarien: {vegetarien}: ");
            Console.WriteLine("");
        }
    }

    private void ObtenirListPasta()
    {
        using var context = new TpCommandManagerContext();
        PastaManager pastaManager = new PastaManager(context);
        List<Pasta> pastas = pastaManager.ObtenirListPasta();

        bool isEmpty = !pastas.Any();
        if(isEmpty)
        {
            Console.WriteLine("Pas de pastas.");
        }
        else
        {
            foreach (var pasta in pastas)
            {
                Afficher(pasta);
            }
        }
    }

    private void ObtenirPasta()
    {
        using var context = new TpCommandManagerContext();
        PastaManager pastaManager = new PastaManager(context);

        try
        {
            ObtenirListPasta();
            Pasta pasta = pastaManager.ObtenirPasta(GetUserEntry.GetEntier("Quelle pasta voulez vous regarder ?"));
            Afficher(pasta);
        }
        catch 
        {
            Console.WriteLine("Cette pasta n'existe pas");
        }
    }

    private void AjouterPasta()
    {
        using var context = new TpCommandManagerContext();

        string nom = GetUserEntry.GetString("Quel nom voulez vous donner à la pasta ?");
        float prix = GetUserEntry.GetEntier("Quel prix voulez vous donner à la pasta ?");
        bool estVegetarien = GetUserEntry.GetBool("Cette pasta est végétarienne ? (O/N)");
        int type = GetUserEntry.GetEntier("Quel type voulez vous donner à la pasta ?");
        int kCal = GetUserEntry.GetEntier("Combien de kCal y'a t-il dans ce plat ?");
        Pasta pasta = new Pasta(nom, prix, estVegetarien, type, kCal);
        PastaManager pastaManager = new PastaManager(context);
        pastaManager.AjouterPasta(pasta);
    }

    private void MiseAJourPasta()
    {
        using var context = new TpCommandManagerContext();
        PastaManager pastaManager = new PastaManager(context);

        try
        {
            ObtenirListPasta();

            Pasta pasta = pastaManager.ObtenirPasta(GetUserEntry.GetEntier("Quelle pasta voulez vous modifier ?"));
            Afficher(pasta);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine($"1 : Nom");
            Console.WriteLine($"2 : Prix");
            Console.WriteLine($"3 : Végétarien");
            int choix = GetUserEntry.GetEntier("");

            switch (choix)
            {
                case 1:
                    string nouveauNom = GetUserEntry.GetString("Quel sera le nouveau nom de la pasta ?");
                    pasta.Nom = nouveauNom;
                    pastaManager.MiseAJourPasta(pasta);
                    Afficher(pasta);
                    break;

                case 2:
                    float nouveauPrix = GetUserEntry.GetEntier("Quel sera le nouveau prix de la pasta ?");
                    pasta.Prix = nouveauPrix;
                    pastaManager.MiseAJourPasta(pasta);
                    Afficher(pasta);
                    break;

                case 3:
                    int nouvVegetarienAsInt = GetUserEntry.GetEntier("Est-ce que la pasta est végétarienne ?");
                    bool nouvVegetarien = Convert.ToBoolean(nouvVegetarienAsInt);
                    pasta.Vegetarien = nouvVegetarien;
                    pastaManager.MiseAJourPasta(pasta);
                    Afficher(pasta);
                    break;

                default:
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Cette pasta n'existe pas");
        }
    }

    private void SupprimerPasta()
    {
        using var context = new TpCommandManagerContext();
        PastaManager pastaManager = new PastaManager(context);

        ObtenirListPasta();

        try
        {
            Pasta pasta = pastaManager.ObtenirPasta(GetUserEntry.GetEntier("Quelle pizza souhaitez vous supprimer ?"));
            Afficher(pasta);
            bool choix = GetUserEntry.GetBool($"\nÊtes vous sûr de vouloir supprimer cette pizza ? (O/N) ");
            if (choix)
            {
                pastaManager.SupprimerPasta(pasta);
            }
        }

        catch
        {
            Console.WriteLine("Cette pasta n'existe pas");
        }
    }
}