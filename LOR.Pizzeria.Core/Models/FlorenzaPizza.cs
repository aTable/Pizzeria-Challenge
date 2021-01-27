using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria.Core.Models
{ 
    public class FlorenzaPizza : Pizza 
    {
        public override void Cut()
        {
            Console.WriteLine("\tCutting pizza into 6 slices with a special knife...");
        }
    }
}
