using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpProject
{
    public class Statictis
    {
        public static int totalSellQuantity = 0;
        public static List<MenuItem> menuItems = new List<MenuItem>();
        static List<MenuItem> Coffee = new List<MenuItem>();
        static List<MenuItem> iceBlended = new List<MenuItem>();
        static List<MenuItem> juice = new List<MenuItem>();
        static List<MenuItem> cake = new List<MenuItem>();
        static List<MenuItem> milkTea = new List<MenuItem>();
        public static void QuickSort(List<MenuItem> list, int low, int high)
        {
            if (low < high)
            {
                // Partition index
                int pi = Partition(list, low, high);

                // Recursively sort elements before and after partition
                QuickSort(list, low, pi - 1);
                QuickSort(list, pi + 1, high);
            }
        }

        // Partition function for QuickSort, now sorted in descending order
        private static int Partition(List<MenuItem> list, int low, int high)
        {
            // Take the last element as the pivot
            MenuItem pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                // Change condition to 'greater than' for descending order
                if (list[j].SellQuantity >= pivot.SellQuantity)
                {
                    i++;
                    // Swap list[i] and list[j]
                    var temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }

            // Swap list[i + 1] and pivot
            var temp2 = list[i + 1];
            list[i + 1] = list[high];
            list[high] = temp2;

            return i + 1;
        }
        public static void printOfSellQuantity(List<MenuItem> Items)
        {
            int totalSellQuantity = 0;
            foreach (MenuItem item in Items)
            {
                totalSellQuantity += item.SellQuantity;
            }
            Console.WriteLine("{0,-20} {1,-16} {2,-20} {3,-20}", "Tên món", "", "Số lượng bán", "Tỉ lệ(%)");
            if (totalSellQuantity == 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine("{0,-20} {1,-20} {2,-16} {3,-20}", Items[i].Name, "", "0","0%");
                }
            }  
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine("{0,-20} {1,-20} {2,-16} {3,-20}", Items[i].Name, "", Items[i].SellQuantity, (float)Items[i].SellQuantity / totalSellQuantity * 100 + "%");
                }
            }    
            
        }
        public static void Run()
        {
            Console.Clear();
            int revenue = manageBill.ReadRevenueToFile();
            Console.WriteLine($"Doanh thu: {revenue}\n");
            menuItems.Clear();
            juice.Clear();
            cake.Clear();
            Coffee.Clear();
            iceBlended.Clear();
            milkTea.Clear();
            if (!File.Exists("menu.txt"))
            {
                Console.WriteLine("Chưa có món.");
                return;
            }

            try
            {
                // Đọc dữ liệu từ tệp
                string[] dataFromFile = File.ReadAllLines("menu.txt");

                foreach (string line in dataFromFile)
                {
                    string[] dataParts = line.Split(',');

                    // Kiểm tra xem dòng có đủ 4 phần không
                    if (dataParts.Length != 4)
                    {
                        Console.WriteLine($"Dòng không hợp lệ: {line}");
                        continue;
                    }

                    string category = dataParts[1].Trim();
                    string name = dataParts[0].Trim();
                    int price;
                    int sellQuantity;

                    // Kiểm tra xem giá và ID có hợp lệ không

                    if (!int.TryParse(dataParts[2], out price))
                    {
                        Console.WriteLine($"Giá không hợp lệ: {dataParts[2]}");
                        continue;
                    }
                    if (!int.TryParse(dataParts[3], out sellQuantity))
                    {
                        Console.WriteLine($"Số lượng bán không hợp lệ: {dataParts[3]}");
                        continue;
                    }

                    MenuItem temp = new MenuItem(name, category, price);
                    temp.SellQuantity = sellQuantity;
                    menuItems.Add(temp);
                    // So sánh không phân biệt chữ hoa chữ thường
                    if (string.Equals(temp.Category, "Cafe", StringComparison.OrdinalIgnoreCase))
                    {
                        Coffee.Add(temp);
                    }
                    else if (string.Equals(temp.Category, "Nước ép", StringComparison.OrdinalIgnoreCase))
                    {
                        juice.Add(temp);
                    }
                    else if (string.Equals(temp.Category, "Đá xay", StringComparison.OrdinalIgnoreCase))
                    {
                        iceBlended.Add(temp);
                    }
                    else if (string.Equals(temp.Category, "Trà sữa", StringComparison.OrdinalIgnoreCase))
                    {
                        milkTea.Add(temp);
                    }
                    else
                    {
                        cake.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi đọc tệp: {ex.Message}");
            }
            QuickSort(menuItems, 0, menuItems.Count - 1);
            totalSellQuantity = 0;
            foreach (MenuItem item in menuItems)
            {
                totalSellQuantity += item.SellQuantity;
            }
            int temp2 = 0;
            Console.WriteLine("{0,-20} {1,-16} {2,-20} {3,-20}", "Tên món","", "Số lượng bán","Tỉ lệ(%)");
            for(int i = 0; i < 3; i++)
            {
                temp2 += menuItems[i].SellQuantity;
                MenuItem item = menuItems[i];
                if(totalSellQuantity==0)
                {
                    Console.WriteLine("{0,-20} {1,-20} {2,-16} {3,-20}", item.Name, "",'0',"0%");
                }
                else
                {
                     Console.WriteLine("{0,-20} {1,-20} {2,-16} {3,-20}", item.Name, "", item.SellQuantity, (float)item.SellQuantity / totalSellQuantity * 100 + "%");
                }
            }
            if(totalSellQuantity==0)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-16} {3,-20}","Khác", "",'0',"0%");
            }
            else
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-16} {3,-20}", "Khác", "", totalSellQuantity - temp2, (float)(totalSellQuantity - temp2) / totalSellQuantity * 100 +"%");
            }
            QuickSort(Coffee, 0, Coffee.Count - 1);
            QuickSort(cake, 0, cake.Count - 1);
            QuickSort(milkTea, 0, milkTea.Count - 1);
            QuickSort(juice, 0, juice.Count - 1);
            QuickSort(iceBlended, 0, iceBlended.Count - 1);
            Console.WriteLine("\n1. Xem chi tiết.\n" +
                              "2. Thoát.");
            int choice = 0;
            while (true)
            {
                Console.Write("Nhập lựa chọn (1 - 2): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại số từ 0 đến 4)");
                }
            }
            switch(choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("(Nhập phím bất kì để quay lại)\n");
                    Console.WriteLine("- Trà sữa ");
                    printOfSellQuantity(milkTea);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("- Nước ép");
                    printOfSellQuantity(juice);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("- Cafe ");
                    printOfSellQuantity(Coffee);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("- Đá xay");
                    printOfSellQuantity(iceBlended);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("- Bánh ngọt");
                    printOfSellQuantity(cake);
                    Console.ReadKey();
                    Run();
                    break;
                case 2:
                    QUANLI.ManageEmployees();
                    break;

            }    

        }
        
    }
}
