using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace MyCSharpProject
{
    public class CafeMenu
    {
        public static List<MenuItem> menuItems = new List<MenuItem>();
        public int nextID = 1;

        public static List<MenuItem> Coffee = new List<MenuItem>();

        public static List<MenuItem> juice = new List<MenuItem>();

        public static List<MenuItem> iceBlended = new List<MenuItem>();

        public static List<MenuItem> cake = new List<MenuItem>();
        public static List<MenuItem> milkTea = new List<MenuItem>();



        public void AddItem(string name, int price)
        {
            Console.WriteLine("\n--- Chọn loại món để thêm vào danh sách ---");
            Console.WriteLine("1. Cafe");
            Console.WriteLine("2. Nước ép");
            Console.WriteLine("3. Đá xay");
            Console.WriteLine("4. Trà sữa");
            Console.WriteLine("5. Bánh ngọt");
            int choice = 0;
            while (true)
            {
                Console.Write($"Chọn loại món (1-5): ");
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Lựa chọn không hợp lệ. Nhập lại số từ 1 đến 4");
                }
            }

            string category = "";
            switch (choice)
            {
                case 1:
                    category = "Cafe";
                    break;
                case 2:
                    category = "Nước ép";
                    break;
                case 3:
                    category = "Đá xay";
                    break;
                case 4:
                    category = "Trà sữa";
                    break;
                case 5:
                    category = "Bánh ngọt";
                    break;
                default:
                    Console.WriteLine("Lựa chọn loại không hợp lệ.");
                    return;
            }

            var newItem = new MenuItem(name, category, price);
            menuItems.Add(newItem);
            Console.Clear();
            Console.WriteLine($"Đã thêm món mới '{name}' vào danh mục {category}");

        }


        public void RemoveItem(string name)
        {
            var item = menuItems.Find(i => string.Equals(name, i.Name, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                menuItems.Remove(item);
                Console.Clear();
                Console.WriteLine("Đã xóa món từ menu.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy món đã nhập.");
            }
        }

        public void UpdateItem(string name, int newPrice, string newName)
        {
            if (menuItems == null || menuItems.Count == 0)
            {
                Console.WriteLine("Danh sách món trống, không thể cập nhật.");
                return;
            }

            bool isUpdated = false;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (string.Equals(menuItems[i].Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    menuItems[i].Name = newName;
                    menuItems[i].Price = newPrice;
                    Console.WriteLine($"Đã cập nhật món \"{name}\" thành \"{newName}\" với giá {newPrice}.");
                    isUpdated = true;
                    break; // Dừng vòng lặp sau khi tìm thấy và cập nhật
                }
            }

            if (!isUpdated)
            {
                Console.WriteLine($"Không tìm thấy món \"{name}\" trong danh sách.");
            }
        }


        public void DisplayMenu()
        {
            var categorizedMenu = new Dictionary<string, List<MenuItem>>();
            foreach (var item in menuItems)
            {
                if (!categorizedMenu.ContainsKey(item.Category))
                {
                    categorizedMenu[item.Category] = new List<MenuItem>();
                }
                categorizedMenu[item.Category].Add(item);
            }

            foreach (var category in categorizedMenu)
            {
                foreach (var item in category.Value)
                {
                    item.DisplayItemStaff();
                }
                Console.WriteLine($"{category} ------------------------");
            }
        }

        public void DisplayMenuByCategory()
        {
            var categorizedMenu = new Dictionary<string, List<MenuItem>>();
            foreach (var item in menuItems)
            {
                if (!categorizedMenu.ContainsKey(item.Category))
                {
                    categorizedMenu[item.Category] = new List<MenuItem>();
                }
                categorizedMenu[item.Category].Add(item);
            }

            foreach (var category in categorizedMenu)
            {
                Console.WriteLine($"\n--- {category.Key} ---");
                foreach (var item in category.Value)
                {
                    item.DisplayItemClient();
                }
            }
        }

        public void SaveMenuToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath,false))
                {
                    foreach (var item in menuItems)
                    {
                        writer.WriteLine($"{item.Name},{item.Category},{item.Price},{item.SellQuantity}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi lưu file: {ex.Message}");
            }
        }

        public static void LoadMenuFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    menuItems.Clear();
                    Coffee.Clear();
                    juice.Clear();
                    iceBlended.Clear();
                    cake.Clear();
                    milkTea.Clear();
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Đọc giá trị nextID từ dòng đầu tiên

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(',');
                            if (parts.Length == 4 && int.TryParse(parts[2], out int price) && int.TryParse(parts[3], out int sellQuantity))
                            {
                                MenuItem temp = new MenuItem(parts[0], parts[1], price);
                                temp.SellQuantity = sellQuantity;
                                menuItems.Add(temp);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File menu chưa tồn tại. Bắt đầu với menu mặc định.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi tải menu: {ex.Message}");
            }
            foreach (MenuItem dish in menuItems)
            {
                if (string.Equals(dish.Category, "Cafe", StringComparison.OrdinalIgnoreCase))
                {
                    Coffee.Add(dish);
                }
                else if (string.Equals(dish.Category, "Nước ép", StringComparison.OrdinalIgnoreCase))
                {
                    juice.Add(dish);
                }
                else if (string.Equals(dish.Category, "Đá xay", StringComparison.OrdinalIgnoreCase))
                {
                    iceBlended.Add(dish);
                }
                else if (string.Equals(dish.Category, "Trà sữa", StringComparison.OrdinalIgnoreCase))
                {
                    milkTea.Add(dish);
                }
                else
                {
                    cake.Add(dish);
                }
            }
        }



    }
}
