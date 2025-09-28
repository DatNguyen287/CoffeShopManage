using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpProject
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int SellQuantity { get; set; }

        public MenuItem(string name, string category, int price)
        {
            Name = name;
            Category = category;
            Price = price;
        }

        public void DisplayItemStaff()
        {
            Console.WriteLine($" - {Name} - {Price:C}");
        }

        public void DisplayItemClient()
        {
            Console.WriteLine($" - {Name} - {Price:C}");
        }
    }
}
