using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuCommande
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
        Console.WriteLine("\nGestion des commandes");
        Console.WriteLine("1 : Lister les commandes");
        Console.WriteLine("2 : Consulter une commande");
        Console.WriteLine("3 : Ajouter une commande");
        Console.WriteLine("4 : Modifier une commande");
        Console.WriteLine("5 : Supprimer une commande");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                ObtenirListCommande();
                break;
            case 2:
                ObtenirCommande();
                break;
            case 3:
                try
                {
                    AjouterCommande();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                MiseAJourCommande();
                break;
            case 5:
                SupprimerCommande();
                break;
            case 6:
                break;
        }
    }

    private void AfficherCommande(Commande commande)
    {
        if (commande == null)
        {
            throw new Exception("Cette commande n'existe pas");
        }
        else  
        {
            Console.WriteLine($"#{commande.Id} - Commandée le : {commande.DateCommande} - {(commande.Status == 2 ? "Livrée le : " + commande.DateLivraison + " - " : "")} Status : {(commande.Status == 1 ? "En cours" : "Livrée")}");
            Console.WriteLine("\nPropriétaire de la commande");
            Console.WriteLine($"#{commande.Utilisateur.Id} - {commande.Utilisateur.Nom.ToUpper()} {commande.Utilisateur.Prenom} - {commande.Utilisateur.Telephone}");
            Console.WriteLine("\nContenu de la commande :");
            foreach (var produit in commande.ProduitCommande)
            {
                Console.WriteLine($"    - {produit.Produit.Nom}, {produit.Produit.Prix} euro");
            }

            Console.WriteLine("\n----------------------------------");
        }
    }

    private void ObtenirListCommande()
    {
        using var context = new TpCommandManagerContext();
        CommandeManager commandeManager = new CommandeManager(context);

        List<Commande> commandes = commandeManager.ObtenirListCommande();

        bool isEmpty = !commandes.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de Commandes.");
        }
        else
        {
            foreach (var commande in commandes)
            {
                AfficherCommande(commande);
            }
        }
    }

    private void ObtenirCommande()
    {
        using var context = new TpCommandManagerContext();
        CommandeManager commandeManager = new CommandeManager(context);

        List<Commande> commandes = commandeManager.ObtenirListCommande();

        bool isEmpty = !commandes.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de Commandes.");
        }
        else
        {
            try
            {
                ObtenirListCommande();

                Commande commande = commandeManager.ObtenirCommande(GetUserEntry.GetEntier("Quelle Commande voulez vous regarder ?"));
                AfficherCommande(commande);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void AjouterCommande()
    {
        Console.WriteLine("\nAjouter une commande");

        using var context = new TpCommandManagerContext();
        CommandeManager commandeManager = new CommandeManager(context);
        UtilisateurManager utilisateurManager = new UtilisateurManager(context);
        ProduitManager produitManager = new ProduitManager(context);

        List<Utilisateur> utilisateurs = utilisateurManager.ObtenirListUtilisateur();

        if (utilisateurs.Count() == 0)
        {
            throw new Exception("Il n'y a pas d'utilisateur en base de données, veuillez en créer une.");
        }

        Utilisateur utilisateur = null;
        do
        {
            foreach (Utilisateur u in utilisateurs)
            {
                Console.WriteLine($"{u.Id} - {u.Nom.ToUpper()} {u.Prenom}");
            }

            int choixUtilisateur = GetUserEntry.GetEntier($"\nChoisissez un utilisateur pour la commande");
            try
            {
                utilisateur = utilisateurManager.ObtenirUtilisateur(choixUtilisateur);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Cette utilisateur n'existe pas.");
                break;
            }
        } while (utilisateur is null);

        DateTime dateCreation = DateTime.Now;
        DateTime? dateLivraison = null;
        int status = 1;

        List<Produit> produits = produitManager.ObtenirListProduit();
        List<ProduitCommande> produitCommandes = new List<ProduitCommande>();
        Commande commande = new Commande(status, utilisateur, utilisateur.Adresse, produitCommandes);
        commandeManager.AjouterCommande(commande);

        bool choixAutreProduit = true;

        foreach (Produit p in produits)
        {
            Console.WriteLine($"#{p.Id} - {p.Nom} - {p.Prix}");
        }

        do
        {
            Console.WriteLine("Ajouter un produit :");

            int choixProduit = GetUserEntry.GetEntier($"\nChoisissez un produit pour la commande");
            Produit produit;

            try
            {
                produit = produitManager.ObtenirProduit(choixProduit);
                commande.ProduitCommande.Add(new ProduitCommande(commande, produit));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Ce produit n'existe pas.");
                break;
            }

            if (!GetUserEntry.GetBool("Souhaitez-vous ajouter un autre produit ? (O/N)"))
            {
                choixAutreProduit = false;
            }
        } while (choixAutreProduit == true);
        commandeManager.MiseAJourCommande(commande);
    }

    private void MiseAJourCommande()
    {
        using var context = new TpCommandManagerContext();
        CommandeManager commandeManager = new CommandeManager(context);
        UtilisateurManager utilisateurManager = new UtilisateurManager(context);
        ProduitManager produitManager = new ProduitManager(context);

        List<Commande> commandes = commandeManager.ObtenirListCommande();

        bool isEmpty = !commandes.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de Commandes.");
        }
        else
        {
            try
            {
                ObtenirListCommande();

                Commande commande = commandeManager.ObtenirCommande(GetUserEntry.GetEntier("Quelle commande voulez vous modifier ?"));
                AfficherCommande(commande);

                Console.WriteLine("Que voulez-vous modifier ?");
                Console.WriteLine("1 : Information générale (status, date de livraison, ...)");
                Console.WriteLine("2 : Le propriétaire de la commande");
                Console.WriteLine("3 : Le contenu de la commande");
                Console.WriteLine("4 : Revenir au menu précédent");
                int choix = GetUserEntry.GetEntier("\n");

                switch (choix)
                {
                    case 1:
                        Console.WriteLine("Quel champ voulez-vous modifier ?");
                        Console.WriteLine("1 : Statut de la commande");
                        Console.WriteLine("2 : Quitter");

                        int choix2 = GetUserEntry.GetEntier("\n");

                        switch (choix2)
                        {
                            case 1:
                                
                                bool status = GetUserEntry.GetBool("La commande a-t-elle été livrée ? (O/N)");
                                if (status)
                                {
                                    commande.Status = 2;
                                    DateTime dateLivraison = DateTime.Now;
                                    commande.DateLivraison = dateLivraison;
                                }
                                break;

                            default:
                                break;
                        }

                        commandeManager.MiseAJourCommande(commande);
                        AfficherCommande(commande);
                        break;

                    case 2:
                        List<Utilisateur> utilisateurs = utilisateurManager.ObtenirListUtilisateur();
                        Utilisateur utilisateur = null;

                        do
                        {
                            foreach (Utilisateur u in utilisateurs)
                            {
                                Console.WriteLine($"{u.Id} - {u.Nom} - {u.Prenom}");
                            }

                            int choixUtilisateur = GetUserEntry.GetEntier($"\nChoisissez un utilisateur dans la liste");
                            try
                            {
                                utilisateur = utilisateurManager.ObtenirUtilisateur(choixUtilisateur);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                Console.WriteLine("Cette utilisateur n'existe pas.");
                                break;
                            }
                        } while (utilisateur is null);

                        commande.Utilisateur = utilisateur;

                        commandeManager.MiseAJourCommande(commande);
                        AfficherCommande(commande);
                        break;

                    case 3:
                        Console.WriteLine("Que voulez-vous faire ?");
                        Console.WriteLine("1 : Retirer une produit");
                        Console.WriteLine("2 : Ajouter un produit");
                        Console.WriteLine("3 : Revenir au menu précédent");

                        int choix3 = GetUserEntry.GetEntier("\n");
                        List<ProduitCommande> produitCommandes = commande.ProduitCommande.ToList();

                        switch (choix3)
                        {
                            case 1:
                                foreach (var produit in produitCommandes)
                                {
                                    Console.WriteLine($"#{produit.Produit.Id} - {produit.Produit.Nom} - {produit.Produit.Prix} euro");
                                }

                                int choixUtilisateur = GetUserEntry.GetEntier($"\nChoisissez un produit dans la liste");
                                produitCommandes.Remove(produitCommandes.Where(p => p.Id == choixUtilisateur)
                                    .FirstOrDefault());

                                commande.ProduitCommande = produitCommandes;
                                commandeManager.MiseAJourCommande(commande);
                                AfficherCommande(commande);
                                break;
                            case 2:
                                List<Produit> produits = produitManager.ObtenirListProduit();
                                bool choixAutreProduit = true;

                                foreach (Produit p in produits)
                                {
                                    Console.WriteLine($"#{p.Id} - {p.Nom} - {p.Prix}");
                                }

                                do
                                {
                                    Console.WriteLine("Ajouter un produit :");

                                    int choixProduit = GetUserEntry.GetEntier($"\nChoisissez un produit pour la commande");
                                    Produit produit;

                                    try
                                    {
                                        produit = produitManager.ObtenirProduit(choixProduit);
                                        produitCommandes.Add(new ProduitCommande(commande, produit));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        Console.WriteLine("Ce produit n'existe pas.");
                                        break;
                                    }

                                    if (!GetUserEntry.GetBool("Souhaitez-vous ajouter un autre produit ? (O/N)"))
                                    {
                                        choixAutreProduit = false;
                                    }
                                } while (choixAutreProduit == true);

                                commande.ProduitCommande = produitCommandes;
                                commandeManager.MiseAJourCommande(commande);
                                AfficherCommande(commande);
                                break;
                            default:
                                break;
                        }

                        break;

                    default:
                        break;
                }
            }

            catch
            {
                Console.WriteLine("Cette commande n'existe pas");
            }
        }
    }

    private void SupprimerCommande()
    {
        using var context = new TpCommandManagerContext();
        CommandeManager commandeManager = new CommandeManager(context);

        List<Commande> commandes = commandeManager.ObtenirListCommande();

        bool isEmpty = !commandes.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de Commandes.");
        }
        else
        {
            try
            {
                ObtenirListCommande();

                Commande commande = commandeManager.ObtenirCommande(GetUserEntry.GetEntier("Quelle Commande souhaitez vous supprimer ?"));
                AfficherCommande(commande);

                bool choix = GetUserEntry.GetBool($"\nÊtes vous sûr de vouloir supprimer cette Commande ? (O/N) ");
                if (choix)
                {
                    commandeManager.SupprimerCommande(commande);
                }
            }
            catch
            {
                Console.WriteLine("Cette commande n'existe pas");
            }
        }
    }
}
