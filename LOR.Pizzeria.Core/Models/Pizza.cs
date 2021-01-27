using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LOR.Pizzeria.Core.Models
{
    public class Pizza : MenuItem
    {
       public int BakingMinutes { get; set; }
        public int BakingTemperature { get; set; }

        public void Prepare()
        {
            Console.WriteLine("Preparing " + Name + "...");
            Console.Write("\tAdding ");
            foreach (var ingredient in Ingredients)
            {
                Console.Write(ingredient + " ");
            }

            if (Extras.Any())
            {
                Console.Write("\n\tAdding extra ");
                foreach (var topping in Extras)
                {
                    Console.Write(topping.Name + " ");
                }
            }

            Console.WriteLine();
        }

        public void Bake()
        {
            Console.WriteLine($"\tBaking pizza for {BakingMinutes} minutes at {BakingTemperature} degrees...");
        }

        public virtual void Cut()
        {
            Console.WriteLine("\tCutting pizza into 8 slices...");
        }

        public void Box()
        {
            Console.WriteLine("\tPutting pizza into a very nice box...");
        }
    }
}
