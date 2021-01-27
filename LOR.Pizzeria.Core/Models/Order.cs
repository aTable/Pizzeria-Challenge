using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Serilog;

namespace LOR.Pizzeria.Core.Models
{
    public class Order
    {
        public Order(ILogger logger, Store store)
        {
            Logger = logger;
            Store = store;
        }
        public ILogger Logger { get; }
        public Store Store { get; }

        public double Total
        {
            get
            {
                return LineItems.Select(x => x.BasePrice).Sum();
            }
        }

        public List<MenuItem> LineItems { get; set; } = new List<MenuItem>();

        public IEnumerable<Pizza> Pizzas
        {
            get
            {
                var abc = LineItems.Where(x => x is Pizza);
                return abc.Cast<Pizza>();
            }
        }

      
        public void AddItem(MenuItem item)
        {
            if (Store.MenuItems.Any(x => x.Name == item.Name))
                LineItems.Add(item);
            else
            {
                Logger.Warning($"Attempted to add menu item '{item.Name}' not available in store '{Store.Name}'");
            }
        }

        public string GenerateReceipt()
        {
            var output = new List<string>();
            output.Add("\n--- YOUR ORDER ---");
            output.AddRange(LineItems.Select(x => x.Print(Store.CurrencyCode)));
            output.Add($"Total: {Total} {Store.CurrencyCode}");
            return string.Join("\r\n",output);
        }
    }
}
