using System.Net.Mail;
using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuUser
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
        Console.WriteLine("\nGestion des utilisateurs");
        Console.WriteLine("1 : Lister les utilisateurs");
        Console.WriteLine("2 : Consulter un utilisateur");
        Console.WriteLine("3 : Ajouter un utilisateur");
        Console.WriteLine("4 : Modifier un utilisateur");
        Console.WriteLine("5 : Supprimer un utilisateur");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                GetAllUsers();
                break;
            case 2:
                GetUser();
                break;
            case 3:
                try
                {
                    CreateUser();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                UpdateUser();
                break;
            case 5:
                DeleteUser();
                break;
            case 6:
                break;
        }
    }

    private void ShowUser(UserDto user)
    {
        if (user == null)
        {
            throw new Exception("Cette utilisateur n'existe pas");
        }
        else
        {
            Console.WriteLine($"#{user.Id} - {user.FirstName.ToUpper()} {user.LastName} - {user.Phone}");
            Console.WriteLine($"{user.Address.Street} - {user.Address.Zip} - {user.Address.City} - {user.Address.State} - {user.Address.Country}");

            Console.WriteLine("");
        }
    }

    private void GetAllUsers()
    {
        using var context = new CommandStoreContext();
        UserService userService = new UserService(context);
        List<UserDto> users = userService.GetAllUsers();

        bool isEmpty = !users.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas d'utilisateurs.");
        }
        else
        {
            foreach (UserDto user in users)
            {
                ShowUser(user);
            }
        }
    }

    private void GetUser()
    {
        using var context = new CommandStoreContext();
        UserService userService = new UserService(context);

        List<UserDto> users = userService.GetAllUsers();
        
        bool isEmpty = !users.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas d'utilisateur.");
        }
        else
        {
            try
            {
                GetAllUsers();

                UserDto user = userService.GetUser(GetUserEntry.GetEntier("Quelle utilisateur voulez vous regarder ?"));
                ShowUser(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void CreateUser()
    {
        Console.WriteLine("\nAjouter un utilisateur");

        try
        {
            using var context = new CommandStoreContext();
            UserService userService = new UserService(context);

            string lastname = GetUserEntry.GetString("Saissez le nom de l'utilisateur");
            string firstname = GetUserEntry.GetString("Saissez le prenom de l'utilisateur");
            string phone = GetUserEntry.GetString("Saissez le numero de téléphone de l'utilisateur");
            string mail = GetUserEntry.GetString("Saissez l'adresse email de l'utilisateur");

            var mailIsValide = new MailAddress(mail);

            if (mailIsValide == null)
            {
                throw new Exception("L'adresse email de l'utilisateur pas valide");
            }

            string street = GetUserEntry.GetString("Saissez le nom de la rue de l'utilisateur");
            string city = GetUserEntry.GetString("Saissez la ville de l'utilisateur");
            string state = GetUserEntry.GetString("Saissez la région de l'utilisateur");
            string zip = GetUserEntry.GetString("Saissez le code postal de l'utilisateur");
            string country = GetUserEntry.GetString("Saissez le pays de l'utilisateur");

            AddressDto adresse = new AddressDto()
            {
                Street = street,
                Zip = zip,
                City = city,
                State = state,
                Country = country
            };

            UserDto user = new UserDto()
            {
                FirstName = firstname,
                LastName = lastname,
                Phone = phone,
                Mail = mail,
                Address = adresse
            };

            userService.CreateUser(user);
        }
        catch (Exception e)
        {
            throw new Exception("Une ou plusieurs données de l'utilisateur ne sont pas valide");
        }
    }

    private void UpdateUser()
    {
        using var context = new CommandStoreContext();
        UserService userService = new UserService(context);
        
        List<UserDto> users = userService.GetAllUsers();

        bool isEmpty = !users.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de d'utilisateur.");
        }
        else
        {
            try
            {
                GetAllUsers();

                int userId = GetUserEntry.GetEntier("Quelle utilisateur voulez vous modifier ?");
                UserDto user = userService.GetUser(userId);
                ShowUser(user);

                Console.WriteLine("Quel voulez-vous faire ?");
                Console.WriteLine("1 : Information générale (nom, prenom, ...");
                Console.WriteLine("2 : Adresse");
                Console.WriteLine("3 : Revenir au menu précédent");
                int choice = GetUserEntry.GetEntier("\n");

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Quel champ voulez-vous modifier ?");
                        Console.WriteLine("1 : Nom");
                        Console.WriteLine("2 : Prenom");
                        Console.WriteLine("3 : Téléphone");
                        Console.WriteLine("4 : Email");
                        Console.WriteLine("5 : Revenir au menu précédent");
                        int choice2 = GetUserEntry.GetEntier("\n");

                        switch (choice2)
                        {
                            case 1:
                                string firstname = GetUserEntry.GetString("Saissez le nom de l'utilisateur");
                                user.FirstName = firstname;
                                break;
                            case 2:
                                string lastname = GetUserEntry.GetString("Saissez le prenom de l'utilisateur");
                                user.LastName = lastname;
                                break;
                            case 3:
                                string phone = GetUserEntry.GetString("Saissez le numero de téléphone de l'utilisateur");
                                user.Phone = phone;
                                break;
                            case 4:
                                string mail = GetUserEntry.GetString("Saissez l'adresse email de l'utilisateur");
                                user.Mail = mail;
                                break;
                            default:
                                break;
                        }
                        break;

                    case 2:
                        Console.WriteLine("Quel champ voulez-vous modifier ?");
                        Console.WriteLine("1 : Rue");
                        Console.WriteLine("2 : Ville");
                        Console.WriteLine("3 : Région");
                        Console.WriteLine("4 : Code Postal");
                        Console.WriteLine("5 : Pays");
                        Console.WriteLine("6 : Revenir au menu précédent");
                        int choice3 = GetUserEntry.GetEntier("\n");

                        switch (choice3)
                        {
                            case 1:
                                string street = GetUserEntry.GetString("Saissez le nom de la rue de l'utilisateur");
                                user.Address.Street = street;
                                break;
                            case 2:
                                string city = GetUserEntry.GetString("Saissez la ville de l'utilisateur");
                                user.Address.City = city;
                                break;
                            case 3:
                                string state = GetUserEntry.GetString("Saissez la région de l'utilisateur");
                                user.Address.State = state;
                                break;
                            case 4:
                                string zip = GetUserEntry.GetString("Saissez le code postal de l'utilisateur");
                                user.Address.Zip = zip;
                                break;
                            case 5:
                                string country = GetUserEntry.GetString("Saissez le pays de l'utilisateur");
                                user.Address.Country = country;
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }

                userService.UpdateUser(userId, user);
                ShowUser(user);
            }
            catch
            {
                Console.WriteLine("Cette utilisateur n'existe pas");
            }
        }
    }

    private void DeleteUser()
    {
        using var context = new CommandStoreContext();
        UserService userService = new UserService(context);
        
        List<UserDto> users = userService.GetAllUsers();

        bool isEmpty = !users.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de d'utilisateur.");
        }
        else
        {
            try
            {
                GetAllUsers();

                int userId = GetUserEntry.GetEntier("Quelle utilisateur souhaitez vous supprimer ?");
                UserDto user = userService.GetUser(userId);
                ShowUser(user);

                bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer cette utilisateur ? (O/N) ");
                if (choice)
                {
                    userService.DeleteUser(userId);
                }
            }
            catch
            {
                Console.WriteLine("Cette utilisateur n'existe pas");
            }
        }
    }
}
