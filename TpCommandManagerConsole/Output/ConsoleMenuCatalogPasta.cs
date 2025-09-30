using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuCatalogPasta
{
    public int StartMenu()
    {
        Console.Clear();
        
        int choice;

        do
        {
            ShowMenu();
            choice = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");
            Console.Clear();

            TreatChoice(choice);
        } while (choice != 6);

        return choice;
    }

    private void ShowMenu()
    {
        Console.WriteLine("\nGestion du menu");
        Console.WriteLine("1 : Liste des pastas");
        Console.WriteLine("2 : Chercher une pasta");
        Console.WriteLine("3 : Créer une pasta");
        Console.WriteLine("4 : Mettre à jour une pasta");
        Console.WriteLine("5 : Supprimer une pasta");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                GetAllPastas();
                break;
            case 2:
                GetPasta();
                break;
            case 3:
                CreatePasta();
                break;
            case 4:
                UpdatePasta();
                break;
            case 5:
                DeletePasta();
                break;
            case 6:
                break;
        }
    }

    private void ShowPasta(PastaDto pasta)
    {
        if(pasta is null)
        {
            throw new Exception("Cette pasta n'existe pas");
        }
        else
        {
            string vegetarian = pasta.Vegetarian ? "Oui" : "Non";

            Console.WriteLine($"#{pasta.Id} - {pasta.Name} - {pasta.Price} - {pasta.Type} - vegetarien: {vegetarian}");
            Console.WriteLine("");
        }
    }

    private void GetAllPastas()
    {
        using var context = new CommandStoreContext();
        PastaService pastaService = new PastaService(context);
        List<PastaDto> pastas = pastaService.GetAllPastas();

        bool isEmpty = !pastas.Any();
        if(isEmpty)
        {
            Console.WriteLine("Pas de pastas.");
        }
        else
        {
            foreach (PastaDto pasta in pastas)
            {
                ShowPasta(pasta);
            }
        }
    }

    private void GetPasta()
    {
        using var context = new CommandStoreContext();
        PastaService pastaService = new PastaService(context);

        List<PastaDto> pastas = pastaService.GetAllPastas();

        bool isEmpty = !pastas.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de pastas.");
        }
        else
        {
            try
            {
                GetAllPastas();

                PastaDto pasta = pastaService.GetPasta(GetUserEntry.GetEntier("Quelle pasta voulez vous regarder ?"));
                ShowPasta(pasta);
            }
            catch
            {
                Console.WriteLine("Cette pasta n'existe pas");
            }
        }
    }

    private void CreatePasta()
    {
        using var context = new CommandStoreContext();

        string name = GetUserEntry.GetString("Quel nom voulez vous donner à la pasta ?");
        decimal price = GetUserEntry.GetDecimal("Quel prix voulez vous donner à la pasta ?");
        bool isVegetarian = GetUserEntry.GetBool("Cette pasta est végétarienne ? (O/N)");
        int type = GetUserEntry.GetEntier("Quel type voulez vous donner à la pasta ?");
        int kCal = GetUserEntry.GetEntier("Combien de kCal y'a t-il dans ce plat ?");

        PastaDto pasta = new PastaDto()
        {
            Name = name,
            Price = price,
            Vegetarian = isVegetarian,
            Type = type,
            KCal = kCal
        };
        PastaService pastaService = new PastaService(context);
        pastaService.CreatePasta(pasta);
    }

    private void UpdatePasta()
    {
        using var context = new CommandStoreContext();
        PastaService pastaService = new PastaService(context);

        try
        {
            GetAllPastas();

            int pastaId = GetUserEntry.GetEntier("Quelle pasta voulez vous modifier ?");
            PastaDto pasta = pastaService.GetPasta(pastaId);
            ShowPasta(pasta);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine("1 : Nom");
            Console.WriteLine("2 : Prix");
            Console.WriteLine("3 : Végétarien");
            Console.WriteLine("4 : Revenir au menu précédent");
            int choice = GetUserEntry.GetEntier("");

            switch (choice)
            {
                case 1:
                    string newName = GetUserEntry.GetString("Quel sera le nouveau nom de la pasta ?");
                    pasta.Name = newName;
                    break;

                case 2:
                    decimal newPrice = GetUserEntry.GetDecimal("Quel sera le nouveau prix de la pasta ?");
                    pasta.Price = newPrice;
                    break;

                case 3:
                    bool newVegetarian = GetUserEntry.GetBool("\nEst-ce que la pasta est végétarienne ? (O/N) ");
                    pasta.Vegetarian = newVegetarian;
                    break;

                default:
                    break;
            }

            pastaService.UpdatePasta(pastaId, pasta);
            ShowPasta(pasta);
        }
        catch
        {
            Console.WriteLine("Cette pasta n'existe pas");
        }
    }

    private void DeletePasta()
    {
        using var context = new CommandStoreContext();
        PastaService pastaService = new PastaService(context);

        GetAllPastas();

        try
        {
            int pastaId = GetUserEntry.GetEntier("Quelle pizza souhaitez vous supprimer ?");
            PastaDto pasta = pastaService.GetPasta(pastaId);
            ShowPasta(pasta);

            bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer cette pizza ? (O/N) ");
            if (choice)
            {
                pastaService.DeletePasta(pastaId);
            }
        }
        catch
        {
            Console.WriteLine("Cette pasta n'existe pas");
        }
    }
}