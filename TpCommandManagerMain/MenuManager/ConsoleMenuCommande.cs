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
            throw new Exception("Cette Commande n'existe pas");
        }
        else
        {
            Console.WriteLine("### Information générale");
            Console.WriteLine($"#{commande.Id} - {commande.DateCommande} - {commande.DateLivraison} - {(commande.Status ? "valider" : "" +
                "non valider")}");
            Console.WriteLine("");

            Console.WriteLine("### Propriétaire de la commande");
            Console.WriteLine($"#{commande.Utilisateur.Id} - {commande.Utilisateur.Nom} - {commande.Utilisateur.Prenom} - {commande.Utilisateur.Telephone}");
            Console.WriteLine("");

            Console.WriteLine("### Information de livraison de la commande");
            Console.WriteLine($"#{commande.Utilisateur.Adresse.Rue} - {commande.Utilisateur.Adresse.CodePostal} - {commande.Utilisateur.Adresse.Ville} - {commande.Utilisateur.Adresse.Region} - {commande.Utilisateur.Adresse.Pays}");
            Console.WriteLine("");

            Console.WriteLine("### Contenu de la commande");
            foreach (var produit in commande.ProduitCommande)
            {
                Console.WriteLine($"#{produit.Produit.Id} - {produit.Produit.Nom} - {produit.Produit.Prix} euro");
            }

            Console.WriteLine("");
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
                Console.WriteLine($"{u.Id} - {u.Nom} - {u.Prenom}");
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

        DateTime dateCreation = GetUserEntry.GetDate("Saisissez la date de création de la commande");
        DateTime dateLivraison = GetUserEntry.GetDate("Saisissez la date de livraison de la commande");

        string status = GetUserEntry.GetString("Saissez le statut de la commande (non valider/valdier)");

        List<Produit> produits = produitManager.ObtenirListProduit();
        List<ProduitCommande> produitCommandes = new List<ProduitCommande>();

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
                produitCommandes.Add(new ProduitCommande(produit.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Ce produit n'existe pas.");
                break;
            }

            if ((GetUserEntry.GetString("Souhaitez-vous ajouter un autre produit ? (O/N)").ToUpper() == "N"))
            {
                choixAutreProduit = false;
            }
        } while (choixAutreProduit == true);

        Commande commande = new Commande(dateCreation, dateLivraison, (status == "valider" ? true : false), utilisateur, utilisateur.Adresse, produitCommandes);
        commandeManager.AjouterCommande(commande);
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
                        Console.WriteLine("1 : Date de livraison");
                        Console.WriteLine("2 : Statut de la commande");
                        Console.WriteLine("3 : Quitter");

                        int choix2 = GetUserEntry.GetEntier("\n");

                        switch (choix2)
                        {
                            case 1:
                                DateTime dateLivraison = GetUserEntry.GetDate("Saisissez la date de livraison de la commande");
                                commande.DateLivraison = dateLivraison;
                                break;
                            case 2:
                                string status = GetUserEntry.GetString("Saisissez le statut de la commande (non valider/valdier)");
                                commande.Status = status == "valider" ? true : false;
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
                                produitCommandes.Remove(produitCommandes.Where(p => p.ProduitId == choixUtilisateur)
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
                                        produitCommandes.Add(new ProduitCommande(produit.Id));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        Console.WriteLine("Ce produit n'existe pas.");
                                        break;
                                    }

                                    if ((GetUserEntry.GetString("Souhaitez-vous ajouter un autre produit ? (O/N)").ToUpper() == "N"))
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

                string choix = GetUserEntry.GetString($"\nÊtes vous sûr de vouloir supprimer cette Commande ? (O/N) ");
                if (choix.ToUpper() == "O" || choix.ToUpper() == "OUI")
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
