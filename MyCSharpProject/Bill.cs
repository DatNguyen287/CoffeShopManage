using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace MyCSharpProject
{
    public class Bill
    {
        public List<BillItems> Items { get; } = new List<BillItems>(); 
        public int TotalPrice { get; set; } 
        public DateTime PaymentTime { get; set; } 

        public Bill(List<BillItems> items, int totalPrice, DateTime paymentTime)
        {
            Items.AddRange(items); 
            TotalPrice = totalPrice; 
            PaymentTime = paymentTime;
        }
        public void AddDish(BillItems billItems)
        {
            Items.Add(billItems);
            TotalPrice += billItems.totalPrice();
        }
        public string ToFileFormat()
        {
            string dishesInfo = string.Join(";", Items.ConvertAll(d => $"{d.Name}-{d.Price}-{d.Quantity}-{d.Note }"));
            return $"{PaymentTime}_{TotalPrice}_{dishesInfo}\n";
        }
    }
} 