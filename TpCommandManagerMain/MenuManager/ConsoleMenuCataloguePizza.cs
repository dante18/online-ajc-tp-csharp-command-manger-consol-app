using System.IO;
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
                ObtenirListPizza();
                break;
            case 2:
                ObtenirPizza();
                break;
            case 3:
                try
                {
                    AjouterPizza();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                MiseAJourPizza();
                break;
            case 5:
                SupprimerPizza();
                break;
            case 6:
                break;
        }
    }


    private void AfficherPizza(Pizza pizza)
    {
        string vegetarien = pizza.Vegetarien ? "Oui" : "Non";
        Console.WriteLine($"#{pizza.Id} - {pizza.Nom} - {pizza.Prix} - vegetarien: {vegetarien} - :");

        Console.WriteLine(String.Join(", ", pizza.Ingredients.Select(i => i.Nom).ToList()));
        Console.WriteLine("");
    }


    private void ObtenirListPizza()
    {
        using var context = new TpCommandManagerContext();
        PizzaManager pizzaManager = new PizzaManager(context);
        List<Pizza> pizzas = pizzaManager.ObtenirListPizza();

        bool isEmpty = !pizzas.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de pizzas.");
        }
        else
        {
            foreach (var pizza in pizzas)
            {
                AfficherPizza(pizza);
            }
        }
    }

    private void ObtenirPizza()
    {
        using var context = new TpCommandManagerContext();
        PizzaManager pizzaManager = new PizzaManager(context);

        try
        {
            Pizza pizza = pizzaManager.ObtenirPizza(GetUserEntry.GetEntier("Quelle pizza voulez vous regarder ?"));
            AfficherPizza(pizza);
        }

        catch
        {
            Console.WriteLine("Cette pizza n'existe pas");
        }
    }

    private void AjouterPizza()
    {
        Console.WriteLine("\nAjouter une pizza");

        using var context = new TpCommandManagerContext();

        PateManager pm = new PateManager(context);
        List<Pate> pates = pm.ObtenirListPate();

        if (pates.Count() == 0)
        {
            throw new Exception("Il n'y a pas de pâte en base de données, veuillez en créer une.");
        }

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

        List<Ingredient> ingredients = new List<Ingredient>();
        int choixIngredient = 0;
        bool choixAutreIngredient = true;
        do
        {
            Console.WriteLine("Ajouter un ingrédient :");

            string nomIngredient = GetUserEntry.GetString("Quel est le nom de l'ingrédient ?");
            float kcal = GetUserEntry.GetEntier("Combient de KCal vaut l'ingrédient ?");
            bool estAllergene = (GetUserEntry.GetString("Cet ingrédient est-il un allergène ? (Y/N)") == "Y") ? true : false;

            Ingredient ingredient = new Ingredient(nomIngredient, kcal, estAllergene);
            ingredients.Add(ingredient);

            if ((GetUserEntry.GetString("Souhaitez-vous ajouter un autre ingrédient ? Y/N").ToLower() == "n"))
            {
                choixAutreIngredient = false;
            }
        } while (choixAutreIngredient == true);

        PizzaManager pizzaManager = new PizzaManager(context);
        pizzaManager.AjouterPizza(new Pizza(nom, prix, vegetarien, ingredients, pate));
    }

    private void MiseAJourPizza()
    {
        using var context = new TpCommandManagerContext();
        PizzaManager pizzaManager = new PizzaManager(context);

        try
        {
            Pizza pizza = pizzaManager.ObtenirPizza(GetUserEntry.GetEntier("Quelle pizza voulez vous modifier ?"));
            AfficherPizza(pizza);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine($"1 : Nom");
            Console.WriteLine($"2 : Prix");
            Console.WriteLine($"3 : Pâte");
            Console.WriteLine("4 : Revenir au menu précédent");
            int choix = GetUserEntry.GetEntier("\n");

            switch (choix)
            {
                case 1:
                    string nouveauNom = GetUserEntry.GetString("Quel sera le nouveau nom de la pizza ?");
                    pizza.Nom = nouveauNom;
                    pizzaManager.MiseAJourPizza(pizza);
                    AfficherPizza(pizza);
                    break;

                case 2:
                    float nouveauPrix = GetUserEntry.GetEntier("Quel sera le nouveau prix de la pizza ?");
                    pizza.Prix = nouveauPrix;
                    pizzaManager.MiseAJourPizza(pizza);
                    AfficherPizza(pizza);
                    break;

                case 3:
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

                    pizza.Pate = pate;

                    pizzaManager.MiseAJourPizza(pizza);
                    AfficherPizza(pizza);
                    break;

                default:
                    break;
            }
        }

        catch
        {
            Console.WriteLine("Cette pizza n'existe pas");
        }


    }

    private void SupprimerPizza()
    {
        using var context = new TpCommandManagerContext();
        PizzaManager pizzaManager = new PizzaManager(context);

        try
        {
            Pizza pizza = pizzaManager.ObtenirPizza(GetUserEntry.GetEntier("Quelle pizza souhaitez vous supprimer ?"));
            AfficherPizza(pizza);
            Console.WriteLine($"\nÊtes vous sûr de vouloir supprimer cette pizza ?");
            string choix = GetUserEntry.GetString("(Y/N");
            if (choix.ToUpper() == "Y")
            {
                pizzaManager.SupprimerPizza(pizza);
            }
        }

        catch
        {
            Console.WriteLine("Cette pizza n'existe pas");
        }

    }
}
