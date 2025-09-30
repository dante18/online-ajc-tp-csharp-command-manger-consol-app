using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerService.Seeders;

public static class DatabaseSeeder
{
    public static void SeedDevData(CommandStoreContext context)
    {
        context.Database.Migrate();

        if (context.Users.Any())
            return;

        // 10 Users
        var addressFaker = new Faker<Address>("fr")
            .RuleFor(a => a.Street, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.State, f => f.Address.State())
            .RuleFor(a => a.Zip, f => f.Address.ZipCode())
            .RuleFor(a => a.Country, f => "France");

        var addresses = addressFaker.Generate(50);

        var userFaker = new Faker<User>("fr")
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("06########"))
            .RuleFor(u => u.Mail, f => f.Internet.Email())
            .RuleFor(u => u.Address, f => f.PickRandom(addresses));

        var users = userFaker.Generate(10);
        context.Users.AddRange(users);
        context.SaveChanges();

        // 2 Doughs
        List<Doughs> doughs = [
            new()
            {
                Name = "Pâte fine",
            },

            new()
            {
                Name = "Pâte épaisse",
            },
        ];
        context.Doughs.AddRange(doughs);
        context.SaveChanges();

        // 39 Ingredients
        Random rnd = new Random();
        List<Ingredient> ingredients = [
            new()
            {
                Name = "tomate",
                KCal = (decimal)10.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "anchois",
                KCal = 28,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "origan",
                KCal = 8,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "olives",
                KCal = 58,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "mozzarella",
                KCal = (decimal)20.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "ail",
                KCal = 10,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "basilic",
                KCal = 38,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "huile d’olive",
                KCal = 100,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "jambon",
                KCal = 150,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "chèvre",
                KCal = 200,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "chorizo",
                KCal = 125,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "cabecou",
                KCal = 100,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "miel",
                KCal = 100,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "thon",
                KCal = 160,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "tomates confites",
                KCal = 0,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "persillade",
                KCal = 90,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "cocktail de fruits de mer",
                KCal = 250,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "champignons frais",
                KCal = 75,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "gorgonzola",
                KCal = 100,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "reblochon",
                KCal = 185,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "poivrons",
                KCal = (decimal)78.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "artichauts",
                KCal = (decimal)100.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "jambon de Parme",
                KCal = (decimal)200.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "filet de poulet",
                KCal = (decimal)150.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "jaune d’oeuf",
                KCal = 89,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "viande hachée",
                KCal = 120,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "merguez",
                KCal = 200,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "oeuf",
                KCal = 100,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "mâche",
                KCal = 80,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "oignons rouges",
                KCal = 78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "sauce blanche",
                KCal = 20,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "kebab volaille",
                KCal = 100,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "Bord fourré au gorgonzola",
                KCal = 120,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "copeaux de parmesan",
                KCal = 250,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "crème fraîche",
                KCal = 200,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "cheddar",
                KCal = 200,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "Boule de burrata",
                KCal = (decimal)100.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "spianata (chorizo italien)",
                KCal = (decimal)120.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "pesto maison",
                KCal = (decimal)100.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
        ];

        context.Ingredients.AddRange(ingredients);
        context.SaveChanges();

        // 35 Pizzas
        List<string> pizzaNameList = [
            "Anchois",
            "Marguerite",
            "Provençale",
            "Jambon",
            "Chèvre",
            "Napolitaine",
            "Chorizo",
            "Chèvre miel",
            "Thon",
            "Fruits de mer",
            "Reine",
            "4 Fromages",
            "Chorizo Poivron",
            "4 Saisons",
            "Montagnarde",
            "Poulet",
            "Calzone",
            "La Bouchère",
            "L’Orientale",
            "La Végétarienne",
            "Parmigiano",
            "La Kebab",
            "5 Fromages",
            "La Cannibale",
            "La Burger",
            "La Burrata",
            "Alsacienne",
            "Suprême de Poulet",
            "Raclette",
            "Munster",
            "Saumon Blanche",
            "Délice de Camembert",
            "Tartiflette",
            "Larzac"
        ];
        List<Pizza> pizzas = new List<Pizza>();

        foreach (var pName in pizzaNameList)
        {
            var pizzaFaker = new Faker<Pizza>("fr")
                .RuleFor(p => p.Name, pName)
                .RuleFor(p => p.Price, f => f.Random.Decimal(8, 20))
                .RuleFor(p => p.Vegetarian, f => f.Random.Bool())
                .RuleFor(p => p.Doughs, f => f.PickRandom(doughs))
                .RuleFor(p => p.Ingredients, f =>
                {
                    var count = f.Random.Int(1, 15);
                    return ingredients
                        .OrderBy(i => rnd.Next())
                        .Take(count)
                        .Select(i => new Ingredient
                        {
                            Name = i.Name,
                            KCal = i.KCal,
                            IsAllergen = i.IsAllergen
                        })
                        .ToList();
                });

            // Générer UNE pizza et l'ajouter à la liste
            pizzas.Add(pizzaFaker.Generate());
        }

        context.Pizzas.AddRange(pizzas);
        context.SaveChanges();

        // 20 Drinks
        List<Drink> drinks = [
            new () { Name = "Coca-Cola", KCal = (decimal) 140, Price = (decimal) 1.5, Fizzy = true },
            new () { Name = "Eau Minérale", KCal = (decimal) 0, Price = (decimal) 1.0, Fizzy = false },
            new () { Name = "Orangina", KCal = (decimal) 120, Price = (decimal) 1.6, Fizzy = true },
            new () { Name = "Jus d'Orange", KCal = (decimal) 110, Price = (decimal) 1.8, Fizzy = false },
            new () { Name = "Thé Glacé", KCal = (decimal) 90, Price = (decimal) 1.4, Fizzy = false },
            new () { Name = "Perrier", KCal = (decimal) 0, Price = (decimal) 1.3, Fizzy = true },
            new () { Name = "Red Bull", KCal = (decimal) 160, Price = (decimal) 2.2, Fizzy = true },
            new () { Name = "Fanta", KCal = (decimal) 150, Price = (decimal) 1.5, Fizzy = true },
            new () { Name = "Sprite", KCal = (decimal) 140, Price = (decimal) 1.5, Fizzy = true },
            new () { Name = "Limonade Artisanale", KCal = 130, Price = (decimal) 2.0, Fizzy = true },
            new () { Name = "Café Frappé", KCal = (decimal) 80, Price = (decimal) 2.5, Fizzy = false },
            new () { Name = "Jus de Pomme", KCal = (decimal) 100, Price = (decimal) 1.7, Fizzy = false },
            new () { Name = "Bière Sans Alcool", KCal = (decimal) 90, Price = (decimal) 2.0, Fizzy = true },
            new () { Name = "Kombucha", KCal = (decimal) 60, Price = (decimal) 3.0, Fizzy = true },
            new () { Name = "Milkshake Fraise", KCal = (decimal) 250, Price = (decimal) 3.5, Fizzy = false },
            new () { Name = "Smoothie Tropical", KCal = (decimal) 200, Price = (decimal) 3.2, Fizzy = false },
            new () { Name = "Eau de Coco", KCal = (decimal) 45, Price = (decimal) 2.8, Fizzy = false },
            new () { Name = "Tonic Water", KCal = (decimal) 90, Price = (decimal) 1.9, Fizzy = true },
            new () { Name = "Ginger Ale", KCal = (decimal) 120, Price = (decimal) 1.6, Fizzy = true },
            new () { Name = "Soda Light", KCal = (decimal) 5, Price = (decimal) 1.5, Fizzy = true }
        ];
        context.Drinks.AddRange(drinks);
        context.SaveChanges();

        // 20 Burgers
        List<Ingredient> ingredientsBurger = [
            new()
            {
                Name = "Steak haché",
                KCal = (decimal)10.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "sauce burger",
                KCal = 28,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "coleslaw",
                KCal = 8,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "tomates",
                KCal = 16,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "cheddar",
                KCal = (decimal)100.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "oignons rouges",
                KCal = 26,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "filet de cabillaud frit",
                KCal = (decimal)120.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "sauce tartare maison",
                KCal = 46,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "filet de poulet croustillant",
                KCal = (decimal)150.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "guacamole",
                KCal = 78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "tartare de tomates",
                KCal = 26,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "escalope de foie gras poêlée",
                KCal = (decimal)200.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "sauce tartufata",
                KCal = (decimal)40.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "steak de légumes",
                KCal = (decimal)60.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "sauce tomate",
                KCal = (decimal)36.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "piperade",
                KCal = (decimal)10.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "bacon",
                KCal = (decimal)36.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "sauce Bleu d’Auvergne AOP",
                KCal = 46,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "sauce poivre vert",
                KCal = 66,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
            new()
            {
                Name = "saint-nectaire",
                KCal = (decimal)80.78,
                IsAllergen = (rnd.Next() % 2 == 0)
            },
        ];

        context.Ingredients.AddRange(ingredientsBurger);
        context.SaveChanges();

        List<string> burgerNameList =
        [
            "Cheeseburger",
            "Bacon Burger",
            "Double Cheese Burger",
            "Veggie Burger",
            "Chicken Burger",
            "Fish Burger",
            "BBQ Burger",
            "Mushroom Swiss Burger",
            "Spicy Jalapeño Burger",
            "Avocado Burger",
            "Blue Cheese Burger",
            "Classic Hamburger",
            "Pulled Pork Burger",
            "Breakfast Burger",
            "Teriyaki Burger",
            "Truffle Burger",
            "Mediterranean Burger",
            "Texas Burger",
            "Hawaiian Burger",
            "Vegan Beyond Burger"
        ];

        List<Burger> burgers = new List<Burger>();

        foreach (var bName in burgerNameList)
        {
            var burgerFaker = new Faker<Burger>("fr")
                .RuleFor(b => b.Name, bName)
                .RuleFor(b => b.Price, f => f.Random.Decimal(8, 20))
                .RuleFor(b => b.Vegetarian, f => f.Random.Bool())
                .RuleFor(b => b.Ingredients, f =>
                {
                    var count = f.Random.Int(1, 15);
                    return ingredientsBurger
                        .OrderBy(i => rnd.Next())
                        .Take(count)
                        .Select(i => new Ingredient
                        {
                            Name = i.Name,
                            KCal = i.KCal,
                            IsAllergen = i.IsAllergen
                        })
                        .ToList();
                });

            // Générer UNE pizza et l'ajouter à la liste
            burgers.Add(burgerFaker.Generate());
        }

        context.Burger.AddRange(burgers);
        context.SaveChanges();

        // 9 Pastas
        List<Pasta> pastaList = [
            new()
            {
                Name = "Puglia",
                Price = (decimal)16.50,
                Type = 0,
                KCal = (decimal)100.78,
                Vegetarian = false
            },
            new()
            {
                Name = "Oceano",
                Price = (decimal)17.50,
                Type = 0,
                KCal = (decimal)90.78,
                Vegetarian = false
            },
            new()
            {
                Name = "Firma",
                Price = (decimal)15.50,
                Type = 0,
                KCal = (decimal)110.78,
                Vegetarian = false
            },
            new()
            {
                Name = "Carbonara Italia",
                Price = 17,
                Type = 0,
                KCal = (decimal)52.78,
                Vegetarian = false
            },
            new()
            {
                Name = "Bolognese",
                Price = (decimal)13,
                Type = 0,
                KCal = (decimal)152.78,
                Vegetarian = false
            },
            new()
            {
                Name = "Cacio E Pepe",
                Price = (decimal)13,
                Type = 0,
                KCal = 36,
                Vegetarian = false
            },
            new()
            {
                Name = "Giardino",
                Price = 12,
                Type = 0,
                KCal = 46,
                Vegetarian = true
            },
            new()
            {
                Name = "Tagliata Al Tonno",
                Price = (decimal)21,
                Type = 0,
                KCal = 125,
                Vegetarian = false
            },
            new()
            {
                Name = "Salmone",
                Price = 21,
                Type = 0,
                KCal = 89,
                Vegetarian = true
            }
        ];
        context.Pastas.AddRange(pastaList);
        context.SaveChanges();

        // 5 Orders avec produits
        var orderFaker = new Faker<Order>("fr")
            .RuleFor(o => o.OrderDate, f => f.Date.Past())
            .RuleFor(o => o.DeliveryDate, f => f.Date.Future())
            .RuleFor(o => o.Status, f => f.Random.Int(0, 2))
            .RuleFor(o => o.User, f => f.PickRandom(users))
            .RuleFor(o => o.DeliveryAddress, f => f.PickRandom(addresses))
            .RuleFor(o => o.OrderProduct, f =>
            {
                var products = pizzas.Cast<Product>().Concat(drinks).ToList();
                return f.PickRandom(products, 2)
                        .Select(p => new OrderProduct { Product = p })
                        .ToList();
            });

        var orders = orderFaker.Generate(5);
        context.Orders.AddRange(orders);
        context.SaveChanges();
    }
}
