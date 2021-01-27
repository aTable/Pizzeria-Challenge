using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOR.Pizzeria.Core.Models
{
    public abstract class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<Topping> Extras { get; set; } = new List<Topping>();

        public string Print(string currencyCode)
        {
            return $"{Name} - {Description} {(Extras.Any() ? $"(with extra {string.Join(",", Extras.Select(x => x.Name))})" : "")} - {BasePrice} {currencyCode}";
        }
    }
}
