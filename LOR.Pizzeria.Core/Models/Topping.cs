using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria.Core.Models
{
    public class Topping
    {       
        public string Name { get; set; }
        public double Price { get; set; }
        public Store Store { get; }

        public string Print()
        {
            return $"{Name} ({Price})";
        }
    }
}
