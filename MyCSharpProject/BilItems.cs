using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpProject
{
    public class BillItems
    {
        public string Name { get; }
        public int Quantity { get; }
        public string Note { get; }
        public int Price { get; }

        public BillItems(string name, int quantity, string note, int price)
        {
            Name = name;
            Quantity = quantity;
            Note = note;
            Price = price;
        }
        public int totalPrice()
        {
            return Price * Quantity;
        }
        public void printInfor()
        {
            Console.WriteLine("{0,-20} {1,-10} x{2,-4} {3,-20} {4,-20}", Name, Price, Quantity, totalPrice(), Note);
        }
    }
}
