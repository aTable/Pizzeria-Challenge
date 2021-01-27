using LOR.Pizzeria.Core.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LOR.Pizzeria.Logic
{
    public class OrderService
    {
        public ILogger Logger { get; }

        public OrderService(ILogger logger)
        {
            Logger = logger;
        }

        public Store GetStoreLocation(Store[] allStores)
        {
            Console.Write($"Welcome to LOR Pizzeria! Please select the store location ({string.Join(" OR ", allStores.Select(x => x.Name))}): ");

            var storeInput = Console.ReadLine();

            var selectedStore = allStores.SingleOrDefault(x => x.Name.Equals(storeInput, StringComparison.CurrentCultureIgnoreCase));
            if (selectedStore == null)
            {
                Logger.Error($"User entered non-existent store: {storeInput}");
                Console.WriteLine("We don't have a store at that location. Please select a valid store.");
                Environment.Exit(0);
            }
            return selectedStore;
        }

        public Order GetOrder(Store selectedStore)
        {
            var order = new Order(Logger, selectedStore);

            bool isContinuingOrder = false;
            do
            {
                Console.WriteLine("\n\n--- MENU ---");
                Console.WriteLine(selectedStore.GenerateMenu());
                Console.Write("\nWhat can I get you?: ");
                var pizzaType = Console.ReadLine();
                var selectedMenuItem = selectedStore.MenuItems.FirstOrDefault(x => x.Name.Equals(pizzaType, StringComparison.CurrentCultureIgnoreCase));
                if (selectedMenuItem == null)
                {
                    Console.WriteLine($"Sorry, we do not stock '{pizzaType}'.");
                }
                else
                {
                    order.AddItem(selectedMenuItem);
                    if (selectedMenuItem is Pizza)
                    {
                        var toppings = GetToppings(selectedStore);
                        selectedMenuItem.Extras.AddRange(toppings);
                    }
                }

                Console.Write("Would you like to order another item? (Yes/No): ");
                var isContinueInput = Console.ReadLine();
                isContinuingOrder = isContinueInput.Equals("yes", StringComparison.CurrentCultureIgnoreCase);
            } while (isContinuingOrder);

            Console.WriteLine(order.GenerateReceipt());
            if (order.Total <= 0)
            {
                Console.WriteLine("You do not have an active order with us. Thank you for your interest, please come back later when you have decided.");
                Environment.Exit(0);
            }

            Console.Write("Do you wish to confirm and pay? (Yes/No): ");
            var confirmOrderInput = Console.ReadLine();
            if (confirmOrderInput.Equals("no", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Thank you for your interest, please come back later when you have decided.");
                Environment.Exit(0);
            }

            return order;
        }

        public void ExecuteOrder(Order order)
        {
            foreach (var pizza in order.Pizzas)
            {
                pizza.Prepare();
                pizza.Bake();
                pizza.Cut();
                pizza.Box();
            }
        }

        private static Topping[] GetToppings(Store store)
        {
            Console.Write("Would you like to add any toppings? (Yes/No): ");
            var addToppingsInput = Console.ReadLine();
            if (addToppingsInput.Equals("no", StringComparison.CurrentCultureIgnoreCase))
            {
                return Array.Empty<Topping>();
            }

            var toppings = new List<Topping>();
            bool isContinuing = false;
            do
            {
                Console.WriteLine("Which toppings?");
                foreach (var topping in store.Toppings)
                {
                    Console.WriteLine(topping.Print());
                }

                var toppingInput = Console.ReadLine();
                var selectedTopping = store.Toppings.FirstOrDefault(x => x.Name.Equals(toppingInput, StringComparison.CurrentCultureIgnoreCase));
                if (selectedTopping == null)
                {
                    Console.WriteLine($"Sorry, we don't stock '{toppingInput}' topping");
                }
                else
                {
                    toppings.Add(selectedTopping);
                }

                Console.Write("Would you like to add another? (Yes/No): ");
                var isContinueInput = Console.ReadLine();
                isContinuing = isContinueInput.Equals("yes", StringComparison.CurrentCultureIgnoreCase);
            } while (isContinuing);

            return toppings.ToArray();
        }
    }
}
