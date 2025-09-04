using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCatalogueIngredient
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
        Console.WriteLine("1 : Liste des ingredients");
        Console.WriteLine("2 : Chercher une ingredient");
        Console.WriteLine("3 : Créer une ingredient");
        Console.WriteLine("4 : Mettre à jour une ingredient");
        Console.WriteLine("5 : Supprimer une ingredient");
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
                //ObtenirIngredient();
                break;
            case 3:
                AjouterIngredient();
                break;
            case 4:
                Console.WriteLine();
                //supprimerIngredient();
                break;
            case 5:
                break;
        }
    }

    private void AjouterIngredient()
    {
        using var context = new TpCommandManagerContext();

        Console.WriteLine("\nAjouter une ingredient");

        string nom = GetUserEntry.GetString("Quel est le nom de l'ingrédient ?");
        float kcal = GetUserEntry.GetEntier("Combient de KCal vaut l'ingrédient ?");
        bool estAllergene = (GetUserEntry.GetString("Cet ingrédient est-il un allergène ? (Y/N)") == "Y") ? true : false;

        Ingredient i = new Ingredient(0, nom, kcal, estAllergene);
        IngredientManager im = new IngredientManager(context);
        im.AjouterIngredient(i);
    }
}

