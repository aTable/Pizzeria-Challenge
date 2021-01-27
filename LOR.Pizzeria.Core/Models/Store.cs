using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria.Core.Models
{
    public class Store
    {

        public string Name { get; set; }
        public MenuItem[] MenuItems { get; set; }

        public Topping[] Toppings { get; set; }

        public string CurrencyCode { get; set; }

        public string GenerateMenu()
        {
            var menu = new List<string>();
            foreach (var item in MenuItems)
            {
                menu.Add(item.Print(CurrencyCode));
            }

            var output = string.Join("\r\n", menu);
            return output;
        }
    }
}
