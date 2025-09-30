using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuCatalogDoughs
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
        Console.WriteLine("1 : Liste des pâtes");
        Console.WriteLine("2 : Chercher une pâte");
        Console.WriteLine("3 : Créer une pâte");
        Console.WriteLine("4 : Mettre à jour une pâte");
        Console.WriteLine("5 : Supprimer une pâte");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                GetAllDoughs();
                break;
            case 2:
                GetDoughs();
                break;
            case 3:
                CreateDoughs();
                break;
            case 4:
                UpdateDoughs();
                break;
            case 5:
                DeleteDoughs();
                break;
            case 6:
                break;
        }
    }

    private void ShowDoughs(DoughsDto doughs)
    {
        if (doughs != null)
        {
            Console.WriteLine($"#{doughs.Id} - {doughs.Name}");
        }
        else
        {
            throw new Exception("Cette pâte n'existe pas");
        }
    }

    private void GetAllDoughs()
    {
        using var context = new CommandStoreContext();
        DoughsService doughsService = new DoughsService(context);
        List<DoughsDto> doughsList = doughsService.GetAllDoughs();

        bool isEmpty = !doughsList.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de pâtes.");
        }
        else
        {
            foreach (var doughs in doughsList)
            {
                ShowDoughs(doughs);
            }
        }
    }

    private void GetDoughs()
    {
        using var context = new CommandStoreContext();
        DoughsService doughsService = new DoughsService(context);

        List<DoughsDto> doughsListe = doughsService.GetAllDoughs();

        bool isEmpty = !doughsListe.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de pâtes.");
        }
        else
        {
            try
            {
                GetAllDoughs();

                DoughsDto doughs = doughsService.GetDoughs(GetUserEntry.GetEntier("Quelle pâte voulez vous regarder ?"));
                ShowDoughs(doughs);
            }
            catch
            {
                Console.WriteLine("Cette pâte n'existe pas");
            }
        }
    }

    private void CreateDoughs()
    {
        using var context = new CommandStoreContext();

        string name = GetUserEntry.GetString("Quel nom voulez vous donner à la pâte ?");
        DoughsDto doughs = new DoughsDto()
        {
            Name = name
        };
        DoughsService doughsService = new DoughsService(context);
        doughsService.CreateDoughs(doughs);
    }

    private void UpdateDoughs()
    {
        using var context = new CommandStoreContext();
        DoughsService doughsService = new DoughsService(context);

        try
        {
            GetAllDoughs();

            int doughsId = GetUserEntry.GetEntier("Quelle pâte voulez vous modifier ?");
            DoughsDto doughs = doughsService.GetDoughs(doughsId);
            ShowDoughs(doughs);

            string name = GetUserEntry.GetString("Saisissez le nouveau nom de la pâte ?");

            doughs.Name = name;
            doughsService.UpdateDoughs(doughsId, doughs);
        }
        catch
        {
            Console.WriteLine("Cette pâtes n'existe pas");
        }
    }

    private void DeleteDoughs()
    {
        using var context = new CommandStoreContext();
        DoughsService doughsService = new DoughsService(context);

        try
        {
            GetAllDoughs();

            int doughsId = GetUserEntry.GetEntier("Quelle pâte voulez vous supprimer ?");
            DoughsDto doughs = doughsService.GetDoughs(doughsId);
            ShowDoughs(doughs);

            bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer cette pâte (O/N) ?");
            if (choice)
            {
                doughsService.DeleteDoughs(doughsId);
            }
        }
        catch
        {
            Console.WriteLine("Cette pâte n'existe pas");
        }
    }
}
