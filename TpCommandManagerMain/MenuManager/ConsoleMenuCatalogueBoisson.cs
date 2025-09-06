using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpCommandManagerDbContext;
using TpCommandManagerDbContext.Entities;
using TpCommandManagerDbContext.EntitiesManager;

namespace TpCommandManagerMain.MenuManager
{
    internal class ConsoleMenuCatalogueBoisson
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
            Console.WriteLine("1 : Liste des boissons");
            Console.WriteLine("2 : Chercher une boisson");
            Console.WriteLine("3 : Créer une boisson");
            Console.WriteLine("4 : Mettre à jour une boisson");
            Console.WriteLine("5 : Supprimer une boisson");
            Console.WriteLine("6 : Retourner sur le menu principal");
        }

        private void TraiterChoix(int choix)
        {
            switch (choix)
            {
                case 1:
                    ObtenirListBoisson();
                    break;
                case 2:
                    ObtenirBoisson();
                    break;
                case 3:
                    AjouterBoisson();
                    break;
                case 4:
                    MiseAJourBoisson();
                    break;
                case 5:
                    SupprimerBoisson();
                    break;
                case 6:
                    break;
            }
        }

        private void AfficherBoisson(Boisson boisson)
        {
            if (boisson is null)
            {
                throw new Exception("Cette boisson n'existe pas");
            }
            else
            {
                string petillant = boisson.Petillant ? "Oui" : "Non";

                Console.WriteLine($"#{boisson.Id} - {boisson.Nom} - {petillant} - {boisson.Prix}: ");
                Console.WriteLine("");
            }
        }

        private void ObtenirListBoisson()
        {
            using var context = new TpCommandManagerContext();
            BoissonManager boissonManager = new BoissonManager(context);
            List<Boisson> boissons = boissonManager.ObtenirListBoisson();

            bool isEmpty = !boissons.Any();
            if (isEmpty)
            {
                Console.WriteLine("Pas de boissons.");
            }
            else
            {
                foreach (var boisson in boissons)
                {
                    AfficherBoisson(boisson);
                }
            }
        }

        private void ObtenirBoisson()
        {
            using var context = new TpCommandManagerContext();
            BoissonManager boissonManager = new BoissonManager(context);

            try
            {
                ObtenirListBoisson();
                Boisson boisson = boissonManager.ObtenirBoisson(GetUserEntry.GetEntier("Quelle boisson voulez vous regarder ?"));
                AfficherBoisson(boisson);
            }
            catch
            {
                Console.WriteLine("Cette boisson n'existe pas");
            }
        }

        private void AjouterBoisson()
        {
            using var context = new TpCommandManagerContext();

            string nom = GetUserEntry.GetString("Quel nom voulez vous donner à la boisson ?");
            float prix = GetUserEntry.GetEntier("Quel prix voulez vous donner à la boisson ?");
            bool estPetillante = GetUserEntry.GetBool("Cette boisson est pétillante ?");
            string type = GetUserEntry.GetString("Quel type voulez vous donner à la boisson ?");
            Boisson boisson = new Boisson(nom, prix, estPetillante, type);
            BoissonManager boissonManager = new BoissonManager(context);
            boissonManager.AjouterBoisson(boisson);
        }

        private void MiseAJourBoisson()
        {
            using var context = new TpCommandManagerContext();
            BoissonManager boissonManager = new BoissonManager(context);

            try
            {
                ObtenirListBoisson();

                Boisson boisson = boissonManager.ObtenirBoisson(GetUserEntry.GetEntier("Quelle boisson voulez vous modifier ?"));
                AfficherBoisson(boisson);

                Console.WriteLine("Quel champ voulez-vous modifier ?");
                Console.WriteLine($"1 : Nom");
                Console.WriteLine($"2 : Prix");
                Console.WriteLine($"3 : Kcal");
                int choix = GetUserEntry.GetEntier("");

                switch (choix)
                {
                    case 1:
                        string nouveauNom = GetUserEntry.GetString("Quel sera le nouveau nom de la boisson ?");
                        boisson.Nom = nouveauNom;
                        boissonManager.MiseAJourBoisson(boisson);
                        AfficherBoisson(boisson);
                        break;

                    case 2:
                        float nouveauPrix = GetUserEntry.GetEntier("Quel sera le nouveau prix de la boisson ?");
                        boisson.Prix = nouveauPrix;
                        boissonManager.MiseAJourBoisson(boisson);
                        AfficherBoisson(boisson);
                        break;

                    case 3:
                        int nouvPetillanteAsInt = GetUserEntry.GetEntier("Est-ce que la boisson est pétillante ?");
                        bool nouvPetillante = Convert.ToBoolean(nouvPetillanteAsInt);
                        boisson.Petillant = nouvPetillante;
                        boissonManager.MiseAJourBoisson(boisson);
                        AfficherBoisson(boisson);
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Cette boisson n'existe pas");
            }
        }

        private void SupprimerBoisson()
        {
            using var context = new TpCommandManagerContext();
            BoissonManager boissonManager = new BoissonManager(context);

            ObtenirListBoisson();

            try
            {
                Boisson boisson = boissonManager.ObtenirBoisson(GetUserEntry.GetEntier("Quelle boisson souhaitez vous supprimer ?"));
                AfficherBoisson(boisson);

                string choix = GetUserEntry.GetString($"\nÊtes vous sûr de vouloir supprimer cette boisson ? (O/N) ?");

                if (choix.ToUpper() == "O")
                {
                    boissonManager.SupprimerBoisson(boisson);
                }
            }

            catch
            {
                Console.WriteLine("Cette boisson n'existe pas");
            }
        }
    }
}
