using System.Net.Mail;
using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager;

public class ConsoleMenuUtilisateur
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
        Console.WriteLine("\nGestion des utilisateurs");
        Console.WriteLine("1 : Lister les utilisateurs");
        Console.WriteLine("2 : Consulter un utilisateur");
        Console.WriteLine("3 : Ajouter un utilisateur");
        Console.WriteLine("4 : Modifier un utilisateur");
        Console.WriteLine("5 : Supprimer un utilisateur");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TraiterChoix(int choix)
    {
        switch (choix)
        {
            case 1:
                ObtenirListUtilisateur();
                break;
            case 2:
                ObtenirUtilisateur();
                break;
            case 3:
                try
                {
                    AjouterUtilisateur();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                MiseAJourUtilisateur();
                break;
            case 5:
                SupprimerUtilisateur();
                break;
            case 6:
                break;
        }
    }

    private void AfficherUtilisateur(Utilisateur utilisateur)
    {
        if (utilisateur == null)
        {
            throw new Exception("Cette utilisateur n'existe pas");
        }
        else
        {
            Console.WriteLine($"#{utilisateur.Id} - {utilisateur.Nom.ToUpper()} {utilisateur.Prenom} - {utilisateur.Telephone}");
            Console.WriteLine($"{utilisateur.Adresse.Rue} - {utilisateur.Adresse.CodePostal} - {utilisateur.Adresse.Ville} - {utilisateur.Adresse.Region} - {utilisateur.Adresse.Pays}");

            Console.WriteLine("");
        }
    }


    private void ObtenirListUtilisateur()
    {
        using var context = new TpCommandManagerContext();
        UtilisateurManager UtilisateurManager = new UtilisateurManager(context);
        List<Utilisateur> Utilisateurs = UtilisateurManager.ObtenirListUtilisateur();

        bool isEmpty = !Utilisateurs.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas d'utilisateurs.");
        }
        else
        {
            foreach (var Utilisateur in Utilisateurs)
            {
                AfficherUtilisateur(Utilisateur);
            }
        }
    }

    private void ObtenirUtilisateur()
    {
        using var context = new TpCommandManagerContext();
        UtilisateurManager utilisateurManager = new UtilisateurManager(context);

        List<Utilisateur> utilisateurs = utilisateurManager.ObtenirListUtilisateur();
        
        bool isEmpty = !utilisateurs.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de d'utilisateur.");
        }
        else
        {
            try
            {
                ObtenirListUtilisateur();

                Utilisateur utilisateur = utilisateurManager.ObtenirUtilisateur(GetUserEntry.GetEntier("Quelle utilisateur voulez vous regarder ?"));
                AfficherUtilisateur(utilisateur);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void AjouterUtilisateur()
    {
        Console.WriteLine("\nAjouter un utilisateur");

        try
        {
            using var context = new TpCommandManagerContext();

            UtilisateurManager utilisateurManager = new UtilisateurManager(context);

            string nom = GetUserEntry.GetString("Saissez le nom de l'utilisateur");
            string prenom = GetUserEntry.GetString("Saissez le prenom de l'utilisateur");
            string telephone = GetUserEntry.GetString("Saissez le numero de téléphone de l'utilisateur");
            string email = GetUserEntry.GetString("Saissez l'adresse email de l'utilisateur");

            var emailEstValide = new MailAddress(email);

            if (emailEstValide == null)
            {
                throw new Exception("L'adresse email de l'utilisateur pas valide");
            }

            string rue = GetUserEntry.GetString("Saissez le nom de la rue de l'utilisateur");
            string ville = GetUserEntry.GetString("Saissez la ville de l'utilisateur");
            string region = GetUserEntry.GetString("Saissez la région de l'utilisateur");
            string codePostal = GetUserEntry.GetString("Saissez le code postal de l'utilisateur");
            string pays = GetUserEntry.GetString("Saissez le pays de l'utilisateur");

            Adresse adresse = new Adresse(rue, codePostal, ville, region, pays);
            Utilisateur utilisateur = new Utilisateur(nom, prenom, telephone, email, adresse);

            utilisateurManager.AjouterUtilisateur(utilisateur);
        }
        catch (Exception e)
        {
            throw new Exception("Une ou plusieurs données de l'utilisateur ne sont pas valide");
        }
    }

    private void MiseAJourUtilisateur()
    {
        using var context = new TpCommandManagerContext();
        UtilisateurManager utilisateurManager = new UtilisateurManager(context);
        
        List<Utilisateur> utilisateurs = utilisateurManager.ObtenirListUtilisateur();

        bool isEmpty = !utilisateurs.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de d'utilisateur.");
        }
        else
        {
            try
            {
                ObtenirListUtilisateur();

                Utilisateur utilisateur = utilisateurManager.ObtenirUtilisateur(GetUserEntry.GetEntier("Quelle utilisateur voulez vous modifier ?"));
                AfficherUtilisateur(utilisateur);

                Console.WriteLine("Quel voulez-vous faire ?");
                Console.WriteLine("1 : Information générale (nom, prenom, ...");
                Console.WriteLine("2 : Adresse");
                Console.WriteLine("3 : Revenir au menu précédent");
                int choix = GetUserEntry.GetEntier("\n");

                switch (choix)
                {
                    case 1:
                        Console.WriteLine("Quel champ voulez-vous modifier ?");
                        Console.WriteLine("1 : Nom");
                        Console.WriteLine("2 : Prenom");
                        Console.WriteLine("3 : Téléphone");
                        Console.WriteLine("4 : Email");
                        Console.WriteLine("5 : Quitter");
                        int choix2 = GetUserEntry.GetEntier("\n");

                        switch (choix2)
                        {
                            case 1:
                                string nom = GetUserEntry.GetString("Saissez le nom de l'utilisateur");
                                utilisateur.Nom = nom;
                                break;
                            case 2:
                                string prenom = GetUserEntry.GetString("Saissez le prenom de l'utilisateur");
                                utilisateur.Prenom = prenom;
                                break;
                            case 3:
                                string telephone = GetUserEntry.GetString("Saissez le numero de téléphone de l'utilisateur");
                                utilisateur.Telephone = telephone;
                                break;
                            case 4:
                                string email = GetUserEntry.GetString("Saissez l'adresse email de l'utilisateur");
                                utilisateur.Email = email;
                                break;
                            default:
                                break;
                        }

                        utilisateurManager.MiseAJourUtilisateur(utilisateur);
                        AfficherUtilisateur(utilisateur);
                        break;

                    case 2:
                        Console.WriteLine("Quel champ voulez-vous modifier ?");
                        Console.WriteLine("1 : Rue");
                        Console.WriteLine("2 : Ville");
                        Console.WriteLine("3 : Région");
                        Console.WriteLine("4 : Code Postal");
                        Console.WriteLine("5 : Pays");
                        Console.WriteLine("6 : Quitter");
                        int choix3 = GetUserEntry.GetEntier("\n");

                        switch (choix3)
                        {
                            case 1:
                                string rue = GetUserEntry.GetString("Saissez le nom de la rue de l'utilisateur");
                                utilisateur.Adresse.Rue = rue;
                                break;
                            case 2:
                                string ville = GetUserEntry.GetString("Saissez la ville de l'utilisateur");
                                utilisateur.Adresse.Ville = ville;
                                break;
                            case 3:
                                string region = GetUserEntry.GetString("Saissez la région de l'utilisateur");
                                utilisateur.Adresse.Region = region;
                                break;
                            case 4:
                                string codePostal = GetUserEntry.GetString("Saissez le code postal de l'utilisateur");
                                utilisateur.Adresse.CodePostal = codePostal;
                                break;
                            case 5:
                                string pays = GetUserEntry.GetString("Saissez le pays de l'utilisateur");
                                utilisateur.Adresse.Pays = pays;
                                break;
                            default:
                                break;
                        }

                        utilisateurManager.MiseAJourUtilisateur(utilisateur);
                        AfficherUtilisateur(utilisateur);
                        break;

                    default:
                        break;
                }
            }

            catch
            {
                Console.WriteLine("Cette utilisateur n'existe pas");
            }
        }
    }

    private void SupprimerUtilisateur()
    {
        using var context = new TpCommandManagerContext();
        UtilisateurManager utilisateurManager = new UtilisateurManager(context);
        
        List<Utilisateur> utilisateurs = utilisateurManager.ObtenirListUtilisateur();

        bool isEmpty = !utilisateurs.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de d'utilisateur.");
        }
        else
        {
            try
            {
                ObtenirListUtilisateur();

                Utilisateur utilisateur = utilisateurManager.ObtenirUtilisateur(GetUserEntry.GetEntier("Quelle utilisateur souhaitez vous supprimer ?"));
                AfficherUtilisateur(utilisateur);
                bool choix = GetUserEntry.GetBool($"\nÊtes vous sûr de vouloir supprimer cette utilisateur ? (O/N) ");
                if (choix)
                {
                    utilisateurManager.SupprimerUtilisateur(utilisateur);
                }
            }

            catch
            {
                Console.WriteLine("Cette utilisateur n'existe pas");
            }
        }
    }
}
