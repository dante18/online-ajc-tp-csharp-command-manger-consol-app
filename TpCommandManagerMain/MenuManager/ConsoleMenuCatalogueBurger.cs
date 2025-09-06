using System.IO;
using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCatalogueBurger
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
        Console.WriteLine("1 : Liste des burgers");
        Console.WriteLine("2 : Chercher un burger");
        Console.WriteLine("3 : Créer un burger");
        Console.WriteLine("4 : Mettre à jour un burger");
        Console.WriteLine("5 : Supprimer un burger");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                ObtenirListBurger();
                break;
            case 2:
                ObtenirBurger();
                break;
            case 3:
                try
                {
                    AjouterBurger();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                MiseAJourBurger();
                break;
            case 5:
                SupprimerBurger();
                break;
            case 6:
                break;
        }
    }


    private void AfficherBurger(Burger burger)
    {
        if (burger == null)
        {
            throw new Exception("Ce burger n'existe pas");
        }
        else
        {
            Console.WriteLine($"#{burger.Id} - {burger.Nom} - {burger.Prix} euro - {(burger.Vegetarien ? "Végétarien" : "Non végétarien")}");
            Console.WriteLine(String.Join(", ", burger.Ingredients.Select(i => i.Nom).ToList()));
            Console.WriteLine("");
        }
    }


    private void ObtenirListBurger()
    {
        using var context = new TpCommandManagerContext();
        BurgerManager burgerManager = new BurgerManager(context);
        List<Burger> burgers = burgerManager.ObtenirListBurger();

        bool isEmpty = !burgers.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de burgers.");
        }
        else
        {
            foreach (var burger in burgers)
            {
                AfficherBurger(burger);
            }
        }
    }

    private void ObtenirBurger()
    {
        using var context = new TpCommandManagerContext();
        BurgerManager burgerManager = new BurgerManager(context);

        try
        {
            ObtenirListBurger();

            Burger burger = burgerManager.ObtenirBurger(GetUserEntry.GetEntier("Quelle burger voulez vous regarder ?"));
            AfficherBurger(burger);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void AjouterBurger()
    {
        Console.WriteLine("\nAjouter un burger");

        using var context = new TpCommandManagerContext();

        string nom = GetUserEntry.GetString("Saissez le nom de la burger");
        float prix = GetUserEntry.GetEntier("Quel sera le prix de la burger ?");
        bool vegetarien = (GetUserEntry.GetString("Cette burger est-elle végétarienne ? (O/N)").ToUpper() == "O") ? true : false;

        List<Ingredient> ingredients = new List<Ingredient>();
        int choixIngredient = 0;
        bool choixAutreIngredient = true;
        do
        {
            Console.WriteLine("Ajouter un ingrédient :");

            string nomIngredient = GetUserEntry.GetString("Quel est le nom de l'ingrédient ?");
            float kcal = GetUserEntry.GetEntier("Combient de KCal vaut l'ingrédient ?");
            bool estAllergene = (GetUserEntry.GetString("Cet ingrédient est-il un allergène ? (O/N)").ToUpper() == "O") ? true : false;

            Ingredient ingredient = new Ingredient(nomIngredient, kcal, estAllergene);
            ingredients.Add(ingredient);

            if ((GetUserEntry.GetString("Souhaitez-vous ajouter un autre ingrédient ? (O/N)").ToUpper() == "N"))
            {
                choixAutreIngredient = false;
            }
        } while (choixAutreIngredient == true);

        BurgerManager burgerManager = new BurgerManager(context);
        burgerManager.AjouterBurger(new Burger(nom, prix, vegetarien, ingredients));
    }

    private void MiseAJourBurger()
    {
        using var context = new TpCommandManagerContext();
        BurgerManager burgerManager = new BurgerManager(context);
        
        try
        {
            ObtenirListBurger();

            Burger burger = burgerManager.ObtenirBurger(GetUserEntry.GetEntier("Quelle burger voulez vous modifier ?"));
            AfficherBurger(burger);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine($"1 : Nom");
            Console.WriteLine($"2 : Prix");
            Console.WriteLine("3 : Revenir au menu précédent");
            int choix = GetUserEntry.GetEntier("\n");

            switch (choix)
            {
                case 1:
                    string nouveauNom = GetUserEntry.GetString("Quel sera le nouveau nom de la burger ?");
                    burger.Nom = nouveauNom;
                    burgerManager.MiseAJourBurger(burger);
                    AfficherBurger(burger);
                    break;

                case 2:
                    float nouveauPrix = GetUserEntry.GetEntier("Quel sera le nouveau prix de la burger ?");
                    burger.Prix = nouveauPrix;
                    burgerManager.MiseAJourBurger(burger);
                    AfficherBurger(burger);
                    break;

                default:
                    break;
            }
        }

        catch
        {
            Console.WriteLine("Ce burger n'existe pas");
        }
    }

    private void SupprimerBurger()
    {
        using var context = new TpCommandManagerContext();
        BurgerManager burgerManager = new BurgerManager(context);
        
        try
        {
            ObtenirListBurger();

            Burger burger = burgerManager.ObtenirBurger(GetUserEntry.GetEntier("Quelle burger souhaitez vous supprimer ?"));
            AfficherBurger(burger);
            string choix = GetUserEntry.GetString($"\nÊtes vous sûr de vouloir supprimer cette burger ? (O/N) ");
            if (choix.ToUpper() == "O" || choix.ToUpper() == "OUI")
            {
                burgerManager.SupprimerBurger(burger);
            }
        }

        catch
        {
            Console.WriteLine("Ce burger n'existe pas");
        }

    }
}
