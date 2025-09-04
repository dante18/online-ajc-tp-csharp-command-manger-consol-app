using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCataloguePizza
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
        Console.WriteLine("1 : Liste des pizzas");
        Console.WriteLine("2 : Chercher une pizza");
        Console.WriteLine("3 : Créer une pizza");
        Console.WriteLine("4 : Mettre à jour une pizza");
        Console.WriteLine("5 : Supprimer une pizza");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                AjouterPizza();
                break;
            case 2:
                //ObtenirPizza();
                Console.WriteLine();
                break;
            case 3:
                //miseAJourPizza();
                Console.WriteLine();
                break;
            case 4:
                //supprimerPizza();
                Console.WriteLine();
                break;
            case 5:
                break;
        }
    }

    private void AjouterPizza()
    {
        Console.WriteLine("\nAjouter une pizza");

        using var context = new TpCommandManagerContext();

        PateManager pm = new PateManager(context);
        List<Pate> pates = pm.ObtenirListPate();
        Pate pate = null;
        do
        {
            foreach (Pate p in pates)
            {
                Console.WriteLine($"{p.Id} : {p.Nom}");
            }
            int choixPate = GetUserEntry.GetEntier($"\nChoisissez une pâte pour la pizza");
            try
            {
                pate = pm.ObtenirPate(choixPate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Cette pâte n'existe pas.");
                break;
            }
        } while (pate is null);

        string nom = GetUserEntry.GetString("Saissez le nom de la pizza");

        float prix = GetUserEntry.GetEntier("Quel sera le prix de la pizza ?");
        bool vegetarien = (GetUserEntry.GetString("Cette pizza est-elle végétarienne ? Y/N") == "y") ? true : false;

        IngredientManager im = new IngredientManager(context);
        List<Ingredient> listeIngredients = im.ObtenirListIngredient();
        List<Ingredient> ingredients = new List<Ingredient>();
        int choixIngredient = 0;
        do
        {
            Console.WriteLine("Ajouter un ingrédient :");
            foreach (Ingredient ingredient in listeIngredients)
            {
                Console.WriteLine($"{ingredient.Id} : {ingredient.Nom}");
            }

            Console.WriteLine("0 : Terminer.");

            choixIngredient = GetUserEntry.GetEntier("\nChoisissez un ingrédient");
            try
            {
                ingredients.Add(im.ObtenirIngredient(choixIngredient));
            }
            catch (Exception e)
            {
                Console.WriteLine("Cet ingrédient n'existe pas.");
            }

        } while (choixIngredient != 0);

        PizzaManager pizzaManager = new PizzaManager(context);
        pizzaManager.AjouterPizza(new Pizza(0, nom, prix, vegetarien, listeIngredients, pate));
    }
}
