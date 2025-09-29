using TpCommandManagerConsole.Input;
using TpCommandManagerData.Context;
using TpCommandManagerService.Dtos;
using TpCommandManagerService.Services;

namespace TpCommandManagerConsole.Output;

public sealed class ConsoleMenuOrder
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
        Console.WriteLine("\nGestion des commandes");
        Console.WriteLine("1 : Lister les commandes");
        Console.WriteLine("2 : Consulter une commande");
        Console.WriteLine("3 : Ajouter une commande");
        Console.WriteLine("4 : Modifier une commande");
        Console.WriteLine("5 : Supprimer une commande");
        Console.WriteLine("6 : Retourner sur le menu principal");
    }

    private void TreatChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                GetAllOrders();
                break;
            case 2:
                GetOrder();
                break;
            case 3:
                try
                {
                    CreateOrder();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 4:
                UpdateOrder();
                break;
            case 5:
                DeleteOrder();
                break;
            case 6:
                break;
        }
    }

    private void ShowOrder(OrderDto order)
    {
        if (order == null)
        {
            throw new Exception("Cette commande n'existe pas");
        }
        else  
        {
            Console.WriteLine($"#{order.Id} - Commandée le : {order.OrderDate} - {(order.Status == 2 ? "Livrée le : " + order.DeliveryDate + " - " : "")} Status : {(order.Status == 1 ? "En cours" : "Livrée")}");
            Console.WriteLine("\nPropriétaire de la commande");
            Console.WriteLine($"#{order.User.Id} - {order.User.FirstName.ToUpper()} {order.User.LastName} - {order.User.Phone}");
            Console.WriteLine("\nContenu de la commande :");
            foreach (OrderProductDto product in order.Products)
            {
                Console.WriteLine($"    - {product.Product.Name}, {product.Product.Price} euro");
            }

            Console.WriteLine("\n----------------------------------");
        }
    }

    private void GetAllOrders()
    {
        using var context = new CommandStoreContext();
        OrderService orderService = new OrderService(context);

        List<OrderDto> orders = orderService.GetAllOrders();

        bool isEmpty = !orders.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de commandes.");
        }
        else
        {
            foreach (OrderDto order in orders)
            {
                ShowOrder(order);
            }
        }
    }

    private void GetOrder()
    {
        using var context = new CommandStoreContext();
        OrderService orderService = new OrderService(context);

        List<OrderDto> orders = orderService.GetAllOrders();

        bool isEmpty = !orders.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de commandes.");
        }
        else
        {
            try
            {
                GetAllOrders();

                OrderDto order = orderService.GetOrder(GetUserEntry.GetEntier("Quelle commande voulez vous regarder ?"));
                ShowOrder(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void CreateOrder()
    {
        Console.WriteLine("\nAjouter une commande");

        using var context = new CommandStoreContext();
        OrderService orderService = new OrderService(context);
        UserService userService = new UserService(context);
        ProductService productService = new ProductService(context);

        List<UserDto> users = userService.GetAllUsers();

        if (users.Count() == 0)
        {
            throw new Exception("Il n'y a pas d'user en base de données, veuillez en créer une.");
        }

        UserDto user = null;
        do
        {
            foreach (UserDto u in users)
            {
                Console.WriteLine($"{u.Id} - {u.FirstName.ToUpper()} {u.LastName}");
            }

            int userChoice = GetUserEntry.GetEntier("\nChoisissez un utilisateur pour la commande");
            try
            {
                user = userService.GetUser(userChoice);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Cette utilisateur n'existe pas.");
                break;
            }
        } while (user is null);

        int status = 1;

        List<ProductDto> products = productService.GetAllProducts();
        List<OrderProductDto> orderProducts = new List<OrderProductDto>();

        bool otherProductChoice = true;

        foreach (ProductDto p in products)
        {
            Console.WriteLine($"#{p.Id} - {p.Name} - {p.Price}");
        }

        do
        {
            Console.WriteLine("Ajouter un produit :");

            int productChoice = GetUserEntry.GetEntier("\nChoisissez un produit pour la commande");

            try
            {
                ProductDto? product = productService.GetProduct(productChoice);
                if (product is not null)
                {
                    orderProducts.Add(new OrderProductDto()
                    {
                        Product = product
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Ce produit n'existe pas.");
                break;
            }

            if (!GetUserEntry.GetBool("Souhaitez-vous ajouter un autre produit ? (O/N)"))
            {
                otherProductChoice = false;
            }
        } while (otherProductChoice == true);

        OrderDto order = new OrderDto()
        {
            Status = status,
            User = user,
            DeliveryAddress = user.Address,
            Products = orderProducts,
            DeliveryDate = null,
        };
        orderService.CreateOrder(order);
    }

    private void UpdateOrder()
    {
        using var context = new CommandStoreContext();
        OrderService orderService = new OrderService(context);
        UserService userService = new UserService(context);
        ProductService productService = new ProductService(context);

        List<OrderDto> orders = orderService.GetAllOrders();

        bool isEmpty = !orders.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de commandes.");
        }
        else
        {
            try
            {
                GetAllOrders();

                int orderId = GetUserEntry.GetEntier("Quelle commande voulez vous modifier ?");
                OrderDto order = orderService.GetOrder(orderId);
                ShowOrder(order);

                Console.WriteLine("Que voulez-vous modifier ?");
                Console.WriteLine("1 : Information générale (status, date de livraison, ...)");
                Console.WriteLine("2 : Le propriétaire de la commande");
                Console.WriteLine("3 : Le contenu de la commande");
                Console.WriteLine("4 : Revenir au menu précédent");
                int choice = GetUserEntry.GetEntier("\n");

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Quel champ voulez-vous modifier ?");
                        Console.WriteLine("1 : Statut de la commande");
                        Console.WriteLine("2 : Revenir au menu précédent");

                        int choice2 = GetUserEntry.GetEntier("\n");

                        switch (choice2)
                        {
                            case 1:
                                
                                bool status = GetUserEntry.GetBool("La commande a-t-elle été livrée ? (O/N)");
                                if (status)
                                {
                                    order.Status = 2;
                                    order.DeliveryDate = DateTime.Now;
                                }
                                break;

                            default:
                                break;
                        }

                        orderService.UpdateOrder(orderId, order);
                        break;

                    case 2:
                        List<UserDto> users = userService.GetAllUsers();
                        UserDto user = null;

                        do
                        {
                            foreach (UserDto u in users)
                            {
                                Console.WriteLine($"{u.Id} - {u.FirstName} - {u.LastName}");
                            }

                            int choiceUser = GetUserEntry.GetEntier("\nChoisissez un utilisateur dans la liste");
                            try
                            {
                                user = userService.GetUser(choiceUser);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                Console.WriteLine("Cette utilisateur n'existe pas.");
                                break;
                            }
                        } while (user is null);

                        order.User = user;

                        orderService.UpdateOrder(orderId, order);
                        break;

                    case 3:
                        Console.WriteLine("Que voulez-vous faire ?");
                        Console.WriteLine("1 : Retirer une produit");
                        Console.WriteLine("2 : Ajouter un produit");
                        Console.WriteLine("3 : Revenir au menu précédent");

                        int choice3 = GetUserEntry.GetEntier("\n");
                        List<OrderProductDto> orderProducts = order.Products.ToList();

                        switch (choice3)
                        {
                            case 1:
                                foreach (var productOrder in orderProducts)
                                {
                                    Console.WriteLine($"#{productOrder.Product.Id} - {productOrder.Product.Name} - {productOrder.Product.Price} euro");
                                }

                                int choiceUser = GetUserEntry.GetEntier("\nChoisissez un produit dans la liste");
                                orderService.UpdateOrderRemoveProductRow(orderId, choiceUser);
                                break;
                            case 2:
                                List<ProductDto> produits = productService.GetAllProducts();
                                bool choiceOtherProduct = true;

                                foreach (ProductDto p in produits)
                                {
                                    Console.WriteLine($"#{p.Id} - {p.Name} - {p.Price}");
                                }

                                do
                                {
                                    Console.WriteLine("Ajouter un produit :");

                                    int choiceProduct = GetUserEntry.GetEntier("\nChoisissez un produit pour la commande");
                                    ProductDto product;

                                    try
                                    {
                                        product = productService.GetProduct(choiceProduct);
                                        orderProducts.Add(new OrderProductDto()
                                        {
                                            Product = product
                                        });
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        Console.WriteLine("Ce produit n'existe pas.");
                                        break;
                                    }

                                    if (!GetUserEntry.GetBool("Souhaitez-vous ajouter un autre produit ? (O/N)"))
                                    {
                                        choiceOtherProduct = false;
                                    }
                                } while (choiceOtherProduct == true);

                                order.Products = orderProducts;
                                orderService.UpdateOrderAddProductRow(orderId, order);
                                break;
                            default:
                                break;
                        }

                        break;

                    default:
                        break;
                }

                ShowOrder(order);
            }
            catch
            {
                Console.WriteLine("Cette commande n'existe pas");
            }
        }
    }

    private void DeleteOrder()
    {
        using var context = new CommandStoreContext();
        OrderService orderService = new OrderService(context);

        List<OrderDto> orders = orderService.GetAllOrders();

        bool isEmpty = !orders.Any();
        if (isEmpty)
        {
            Console.WriteLine("Pas de commandes.");
        }
        else
        {
            try
            {
                GetAllOrders();

                int orderId = GetUserEntry.GetEntier("Quelle commande souhaitez vous supprimer ?");
                OrderDto order = orderService.GetOrder(orderId);
                ShowOrder(order);

                bool choice = GetUserEntry.GetBool("\nÊtes vous sûr de vouloir supprimer cette commande ? (O/N) ");
                if (choice)
                {
                    orderService.DeleteOrder(orderId);
                }
            }
            catch
            {
                Console.WriteLine("Cette commande n'existe pas");
            }
        }
    }
}
