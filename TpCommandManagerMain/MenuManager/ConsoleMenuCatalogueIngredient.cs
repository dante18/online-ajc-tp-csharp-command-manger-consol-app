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
                ObtenirListIngredient();
                break;
            case 2:
                ObtenirIngredient();
                break;
            case 3:
                AjouterIngredient();
                break;
            case 4:
                MiseAJourIngredient();
                break;
            case 5:
                SupprimerIngredient();
                break;
            case 6:
                break;
        }
    }

    private void AfficherIngredient(Ingredient ingredient)
    {
        string allergene = ingredient.EstAllergene ? "Oui" : "Non";

        Console.WriteLine($"#{ingredient.Id} - {ingredient.Nom} - {ingredient.Kcal} - allergene: {allergene}");
        Console.WriteLine("");
    }

    private void ObtenirListIngredient()
    {
        using var context = new TpCommandManagerContext();
        IngredientManager ingredientManager = new IngredientManager(context);
        List<Ingredient> ingredients = ingredientManager.ObtenirListIngredient();

        bool isEmpty = !ingredients.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas d'ingrédients.");
        }
        else
        {
            foreach (var ingredient in ingredients)
            {
                AfficherIngredient(ingredient);
            }
        }
    }

    private void ObtenirIngredient()
    {
        using var context = new TpCommandManagerContext();
        IngredientManager ingredientManager = new IngredientManager(context);

        try
        {
            Ingredient ingredient = ingredientManager.ObtenirIngredient(GetUserEntry.GetEntier("Quelle ingrédient voulez vous regarder ?"));
            AfficherIngredient(ingredient);
        }
        catch
        {
            Console.WriteLine("Cette ingrédient n'existe pas");
        }
    }

    private void AjouterIngredient()
    {
        using var context = new TpCommandManagerContext();

        Console.WriteLine("\nAjouter une ingrédient");

        string nom = GetUserEntry.GetString("Quel est le nom de l'ingrédient ?");
        float kcal = GetUserEntry.GetEntier("Combient de KCal vaut l'ingrédient ?");
        bool estAllergene = (GetUserEntry.GetString("Cet ingrédient est-il un allergène ? (Y/N)") == "Y") ? true : false;

        Ingredient i = new Ingredient(nom, kcal, estAllergene);
        IngredientManager im = new IngredientManager(context);
        im.AjouterIngredient(i);
    }

    private void MiseAJourIngredient()
    {
        using var context = new TpCommandManagerContext();
        IngredientManager ingredientManager = new IngredientManager(context);

        try
        {
            Ingredient ingredient = ingredientManager.ObtenirIngredient(GetUserEntry.GetEntier("Quelle ingrédient voulez vous modifier ?"));
            AfficherIngredient(ingredient);

            string nom = GetUserEntry.GetString("Saisissez le nouveau nom de l'ingédient ?");
            int kCal = GetUserEntry.GetEntier("Saisissez le nombre de KCal ?");

            ingredient.Nom = nom;
            ingredient.Kcal = kCal;
            ingredientManager.MiseAJourIngredient(ingredient);
        }
        catch
        {
            Console.WriteLine("Cette pizza n'existe pas");
        }
    }

    private void SupprimerIngredient()
    {
        using var context = new TpCommandManagerContext();
        IngredientManager ingredientManager = new IngredientManager(context);

        try
        {
            Ingredient ingredient = ingredientManager.ObtenirIngredient(GetUserEntry.GetEntier("Quelle ingrédient voulez vous supprimer ?"));
            AfficherIngredient(ingredient);

            Console.WriteLine($"\nÊtes vous sûr de vouloir supprimer cette ingrédient ?");

            string choix = GetUserEntry.GetString("(Y/N");
            if (choix.ToUpper() == "Y")
            {
                ingredientManager.SupprimerIngredient(ingredient);
            }
        }
        catch
        {
            Console.WriteLine("Cette ingrédient n'existe pas");
        }
    }
}

