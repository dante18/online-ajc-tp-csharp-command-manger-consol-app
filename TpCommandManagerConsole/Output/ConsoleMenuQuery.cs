using Microsoft.EntityFrameworkCore;
using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuQuery
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
        Console.WriteLine("\nMenu des requêtes :");
        Console.WriteLine("1 : Lister les utilisateurs qui ont effectué une commande");
        Console.WriteLine("2 : Lister les commandes exclusivement végétariennes");
        Console.WriteLine("3 : Récupérer le nombre de calories d’une commande (Non implémenté)");
        Console.WriteLine("4 : Lister les produits qui contiennent un allergène (Non implémenté)");
        Console.WriteLine("5 : Lister toutes les commandes en cours");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                using (var context = new CommandStoreContext())
                {
                    var userWithOrder = context.Orders.GroupBy(order => order.User);

                    Console.WriteLine("Voici la liste des utilisateurs ayant déjà passé une commande :\n");
                    foreach (var u in userWithOrder)
                    {
                        Console.WriteLine($"- {u.Key.FirstName.ToUpper()} {u.Key.LastName}");
                    }
                }
                break;

            case 2:
                using (var context = new CommandStoreContext())
                {
                    Console.WriteLine("Voici la liste des commandes exclusivement végétariennes :");
                    var orders = context.Orders.Include(o => o.OrderProduct).ThenInclude(op => op.Product).ToList();
                    var foodVege = context.Food.Where(f => f.Vegetarian).ToList();
                    var drinks = context.Drinks.ToList();

                    foreach (var order in orders)
                    {
                        if (order.OrderProduct.All(op =>
                                foodVege.Any(fv => fv.Id == op.Product.Id) ||
                                drinks.Any(d => d.Id == op.Product.Id)))
                        {
                            Console.WriteLine($"#{order.Id} - Commandée le : {order.OrderDate} - {(order.Status == 2 ? "Livrée le : " + order.DeliveryDate + " - " : "")} Status : {(order.Status == 1 ? "En cours" : "Livrée")}");
                        }
                    }

                }
                break;

            case 3:
                //int id = GetUserEntry.GetEntier("Saisir le numéro de commande pour afficher son nombre de calories");
                //using (var context = new CommandStoreContext())
                //{
                //    var commande = context.Commandes.Include(c => c.ProduitCommande).ThenInclude(pc => pc.Produit).First(c => c.Id == id);
                //    var produitCommandes = commande.ProduitCommande;
                //    var produits = context.Produit.Include(p => p.ProduitCommande).Where(p => p.ProduitCommande.Any(pc => pc.Produit.Id == p.Id)).Select(p => p.Id).ToList();

                //    int totalCalorie = 0;


                //}
                break;

            case 4:
                //Console.WriteLine("Liste des produits contenant un allergène :");
                //using (var context = new CommandStoreContext())
                //{
                //    List<Pizza> pizzaAllergenes = new List<Pizza>();
                //    var produits = context.Produit;
                //    var pizzas = context.Pizzas.Include(p => p.Ingredients).GroupBy(p => p.Ingredients);

                //}
                break;

            case 5:
                using (var context = new CommandStoreContext())
                {
                    var orderInProgress = context.Orders.Where(o => o.Status == 1);

                    Console.WriteLine("Liste des commandes en cours :");
                    foreach (var o in orderInProgress)
                    {
                        Console.WriteLine($"#{o.Id} - Commandée le : {o.OrderDate} -" +
                            $" {(o.Status == 2 ? "Livrée le : " + o.DeliveryDate + " - " : "")}" +
                            $" Status : {(o.Status == 1 ? "En cours" : "Livrée")}");
                    }
                }
                break;

            case 6:
                break;

            default:
                Console.WriteLine("Ce choix n'existe pas");
                break;
        }
    }
}

