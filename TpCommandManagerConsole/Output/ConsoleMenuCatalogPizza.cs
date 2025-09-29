using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuCatalogPizza
{
    public int StartMenu()
    {
        Console.Clear();

        int choix;

        do
        {
            ShowMenu();
            choix = GetUserEntry.GetEntier("Que souhaitez-vous faire ?");
            Console.Clear();

            TreatChoice(choix);
        } while (choix != 6);

        return choix;
    }

    private void ShowMenu()
    {
        Console.WriteLine("\nGestion du menu");
        Console.WriteLine("1 : Liste des pizzas");
        Console.WriteLine("2 : Chercher une pizza");
        Console.WriteLine("3 : Créer une pizza");
        Console.WriteLine("4 : Mettre à jour une pizza");
        Console.WriteLine("5 : Supprimer une pizza");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choix)
    {
        switch (choix)
        {
            case 1:
                GetAllPizzas();
                break;
            case 2:
                GetPizza();
                break;
            case 3:
                try
                {
                    CreatePizza();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                UpdatePizza();
                break;
            case 5:
                DeletePizza();
                break;
            case 6:
                break;
        }
    }

    private void ShowPizza(PizzaDto pizza)
    {
        if (pizza == null)
        {
            throw new Exception("Cette pizza n'existe pas");
        }
        else
        {
            Console.WriteLine($"#{pizza.Id} - {pizza.Name} - {pizza.Price} euro - {(pizza.Vegetarian ? "Végétarien" : "Non végétarien")}");
            Console.WriteLine(string.Join(", ", pizza.Ingredients.Select(i => i.Name).ToList()));
            Console.WriteLine("");
        }
    }

    private void GetAllPizzas()
    {
        using var context = new CommandStoreContext();
        PizzaService pizzaService = new PizzaService(context);
        List<PizzaDto> pizzas = pizzaService.GetAllPizzas();

        bool isEmpty = !pizzas.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de pizzas.");
        }
        else
        {
            foreach (var pizza in pizzas)
            {
                ShowPizza(pizza);
            }
        }
    }

    private void GetPizza()
    {
        using var context = new CommandStoreContext();
        PizzaService pizzaService = new PizzaService(context);

        List<PizzaDto> pizzas = pizzaService.GetAllPizzas();

        bool isEmpty = !pizzas.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de pizzas.");
        }
        else
        {
            try
            {
                GetAllPizzas();

                PizzaDto pizza = pizzaService.GetPizza(GetUserEntry.GetEntier("Quelle pizza voulez vous regarder ?"));
                ShowPizza(pizza);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void CreatePizza()
    {
        Console.WriteLine("\nAjouter une pizza");

        using var context = new CommandStoreContext();

        DoughsService doughsService  = new DoughsService(context);
        List<DoughsDto> doughsList = doughsService.GetAllDoughs();

        if (doughsList.Count() == 0)
        {
            throw new Exception("Il n'y a pas de pâte en base de données, veuillez en créer une.");
        }

        DoughsDto doughs = null;
        do
        {
            foreach (DoughsDto d in doughsList)
            {
                Console.WriteLine($"{d.Id} : {d.Name}");
            }
            int doughsChoice = GetUserEntry.GetEntier("\nChoisissez une pâte pour la pizza");
            try
            {
                doughs = doughsService.GetDoughs(doughsChoice);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Cette pâte n'existe pas.");
                break;
            }
        } while (doughs is null);

        string name = GetUserEntry.GetString("Saissez le nom de la pizza");
        decimal price = GetUserEntry.GetDecimal("Quel sera le prix de la pizza ?");
        bool vegetarian = GetUserEntry.GetBool("Cette pizza est-elle végétarienne ? (O/N)");

        List<IngredientDto> ingredients = new List<IngredientDto>();
        int ingredientChoice = 0;
        bool ingredientOtherChoice = true;
        do
        {
            Console.WriteLine("Ajouter un ingrédient :");

            string ingredientName = GetUserEntry.GetString("Quel est le nom de l'ingrédient ?");
            decimal kCal = GetUserEntry.GetDecimal("Combient de KCal vaut l'ingrédient ?");
            bool isAllergen = GetUserEntry.GetBool("Cet ingrédient est-il un allergène ? (O/N)");

            IngredientDto ingredient = new IngredientDto()
            {
                Name = ingredientName,
                KCal = kCal,
                IsAllergen = isAllergen
            };
            ingredients.Add(ingredient);

            if (!GetUserEntry.GetBool("Souhaitez-vous ajouter un autre ingrédient ? (O/N)"))
            {
                ingredientOtherChoice = false;
            }
        } while (ingredientOtherChoice == true);

        PizzaService pizzaService = new PizzaService(context);
        pizzaService.CreatePizza(new PizzaDto()
        {
            Name = name,
            Price = price,
            Vegetarian = vegetarian,
            Ingredients = ingredients,
            Doughs = doughs
        });
    }

    private void UpdatePizza()
    {
        using var context = new CommandStoreContext();
        PizzaService pizzaService = new PizzaService(context);
        GetAllPizzas();

        try
        {
            int pizzaId = GetUserEntry.GetEntier("Quelle pizza voulez vous modifier ?");
            PizzaDto pizza = pizzaService.GetPizza(pizzaId);
            ShowPizza(pizza);

            Console.WriteLine("Quel champ voulez-vous modifier ?");
            Console.WriteLine("1 : Nom");
            Console.WriteLine("2 : Prix");
            Console.WriteLine("3 : Pâte");
            Console.WriteLine("4 : Revenir au menu précédent");
            int choice = GetUserEntry.GetEntier("\n");

            switch (choice)
            {
                case 1:
                    string newName = GetUserEntry.GetString("Quel sera le nouveau nom de la pizza ?");
                    pizza.Name = newName;
                    break;
                case 2:
                    decimal newPrice = GetUserEntry.GetDecimal("Quel sera le nouveau prix de la pizza ?");
                    pizza.Price = newPrice;
                    break;
                case 3:
                    DoughsService doughsService = new DoughsService(context);
                    List<DoughsDto> doughsList = doughsService.GetAllDoughs();
                    DoughsDto doughs = null;

                    do
                    {
                        foreach (DoughsDto d in doughsList)
                        {
                            Console.WriteLine($"{d.Id} : {d.Name}");
                        }
                        int doughsChoice = GetUserEntry.GetEntier("\nChoisissez une pâte pour la pizza");

                        try
                        {
                            doughs = doughsService.GetDoughs(doughsChoice);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Console.WriteLine("Cette pâte n'existe pas.");
                            break;
                        }
                    } while (doughs is null);

                    pizza.Doughs = doughs;
                    break;

                default:
                    break;
            }

            pizzaService.UpdatePizza(pizzaId, pizza);
            ShowPizza(pizza);
        }
        catch
        {
            Console.WriteLine("Cette pizza n'existe pas");
        }
    }

    private void DeletePizza()
    {
        using var context = new CommandStoreContext();
        PizzaService pizzaService = new PizzaService(context);
        GetAllPizzas();

        try
        {
            int pizzaId = GetUserEntry.GetEntier("Quelle pizza souhaitez vous supprimer ?");
            PizzaDto pizza = pizzaService.GetPizza(pizzaId);
            ShowPizza(pizza);

            bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer cette pizza ? (O/N) ");
            if (choice)
            {
                pizzaService.DeletePizza(pizzaId);
            }
        }
        catch
        {
            Console.WriteLine("Cette pizza n'existe pas");
        }
    }
}
