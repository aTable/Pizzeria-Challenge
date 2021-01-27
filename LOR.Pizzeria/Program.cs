using LOR.Pizzeria.Core.Models;
using LOR.Pizzeria.Logic;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LOR.Pizzeria
{
    class Program
    {
        private static Store[] _stores;
        private static ILogger _logger;

        static void Main(string[] args)
        {
            Init();
            var orderService = new OrderService(_logger);
            var selectedStore = orderService.GetStoreLocation(_stores);
            var order = orderService.GetOrder(selectedStore);
            orderService.ExecuteOrder(order);

            Console.WriteLine(order.GenerateReceipt());
            Console.WriteLine($"\nYour order is ready! Thank you for visiting {selectedStore.Name}");
        }

        private static void Init()
        {
            _logger = new LoggerConfiguration()
                        .WriteTo.File(path: ".log")
                        .CreateLogger();

            var toppings = new List<Topping>
            {
                new Topping { Name = "Mayo", Price = 0, },
                new Topping { Name = "Cheese", Price = 0, },
                new Topping { Name = "Olive oil", Price = 0, },
            }.ToArray();

            var capriciosa = new Pizza { Name = "Capriciosa", Description = "mushrooms, cheese, ham, mozarella", BasePrice = 20, Ingredients = new List<string> { "mushrooms", "cheese", "ham", "mozarella" }, BakingMinutes = 30, BakingTemperature = 200 };

            var stores = new Store[]
            {
                new Store
                {
                    Name = "Brisbane",
                    CurrencyCode = "AUD",
                    MenuItems = new MenuItem[]
                    {
                        capriciosa,
                        new FlorenzaPizza { Name = "Florenza", Description = "olives, pastrami, mozarella, onion", BasePrice = 21, Ingredients = new List<string> { "olives", "pastrami", "mozarella", "onion" }, BakingMinutes = 30, BakingTemperature = 200 },
                        new Pizza { Name = "Margherita", Description = "mozarella, onion, garlic, oregano", BasePrice = 22, Ingredients = new List<string> { "mozarella", "onion", "garlic", "oregano" }, BakingMinutes = 15, BakingTemperature = 200 },
                        new Dessert { Name = "Lava Cake", Description = "Melting deliciousness", BasePrice = 4, StorageTemperature = -4, },
                    },
                    Toppings = toppings,
                },
                new Store
                {
                    Name = "Sydney",
                    CurrencyCode = "AUD",
                    MenuItems = new MenuItem[]
                    {
                        capriciosa,
                        new Pizza { Name = "Inferno", Description = "chili peppers, mozzarella, chicken, cheese", BasePrice = 31, Ingredients = new List<string> { "chili peppers", "mozzarella", "chicken", "cheese" }, BakingMinutes = 30, BakingTemperature = 200 },
                    },
                    Toppings = toppings,
                }
            };

            _stores = stores;
        }
    }
}
