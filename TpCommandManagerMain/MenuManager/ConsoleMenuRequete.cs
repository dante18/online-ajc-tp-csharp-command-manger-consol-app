using Microsoft.EntityFrameworkCore;
using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuRequete
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
        Console.WriteLine("\nMenu des requêtes :");
        Console.WriteLine("1 : Lister les utilisateurs qui ont effectué une commande");
        Console.WriteLine("2 : Lister les commandes exclusivement végétariennes");
        Console.WriteLine("3 : Récupérer le nombre de calories d’une commande (Non implémenté)");
        Console.WriteLine("4 : Lister les produits qui contiennent un allergène (Non implémenté)");
        Console.WriteLine("5 : Lister toutes les commandes en cours");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                using (var context = new TpCommandManagerContext())
                {
                    var utilisateurAvecCommande = context.Commandes.GroupBy(commande => commande.Utilisateur);

                    Console.WriteLine("Voici la liste des utilisateurs ayant déjà passé une commande :\n");
                    foreach (var u in utilisateurAvecCommande)
                    {
                        Console.WriteLine($"- {u.Key.Nom.ToUpper()} {u.Key.Prenom}");
                    }
                }
                break;

            case 2:
                using (var context = new TpCommandManagerContext())
                {
                    Console.WriteLine("Voici la liste des commandes exclusivement végétariennes :");
                    var commandes = context.Commandes.Include(c => c.ProduitCommande).ThenInclude(pc => pc.Produit).ToList();
                    var nourrituresVege = context.Nourriture.Where(n => n.Vegetarien).ToList();
                    var boissons = context.Boissons.ToList();

                    foreach (var commande in commandes)
                    {
                        if (commande.ProduitCommande.All(pc => 
                                nourrituresVege.Any(nv => nv.Id == pc.Produit.Id) ||
                                boissons.Any(b => b.Id == pc.Produit.Id)))
                        {
                            Console.WriteLine($"#{commande.Id} - Commandée le : {commande.DateCommande} - {(commande.Status == 2 ? "Livrée le : " + commande.DateLivraison + " - " : "")} Status : {(commande.Status == 1 ? "En cours" : "Livrée")}");
                        }
                    }

                }
                break;

            case 3:
                //int id = GetUserEntry.GetEntier("Saisir le numéro de commande pour afficher son nombre de calories");
                //using (var context = new TpCommandManagerContext())
                //{
                //    var commande = context.Commandes.Include(c => c.ProduitCommande).ThenInclude(pc => pc.Produit).First(c => c.Id == id);
                //    var produitCommandes = commande.ProduitCommande;
                //    var produits = context.Produit.Include(p => p.ProduitCommande).Where(p => p.ProduitCommande.Any(pc => pc.Produit.Id == p.Id)).Select(p => p.Id).ToList();

                //    int totalCalorie = 0;


                //}
                break;

            case 4:
                //Console.WriteLine("Liste des produits contenant un allergène :");
                //using (var context = new TpCommandManagerContext())
                //{
                //    List<Pizza> pizzaAllergenes = new List<Pizza>();
                //    var produits = context.Produit;
                //    var pizzas = context.Pizzas.Include(p => p.Ingredients).GroupBy(p => p.Ingredients);

                //}
                break;

            case 5:
                using (var context = new TpCommandManagerContext())
                {
                    var commandeEnCours = context.Commandes.Where(commande => commande.Status == 1);
                    Console.WriteLine("Liste des commandes en cours :");
                    foreach (var commande in commandeEnCours)
                    {
                        Console.WriteLine($"#{commande.Id} - Commandée le : {commande.DateCommande} -" +
                            $" {(commande.Status == 2 ? "Livrée le : " + commande.DateLivraison + " - " : "")}" +
                            $" Status : {(commande.Status == 1 ? "En cours" : "Livrée")}");
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

