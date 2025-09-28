using System;
using System.Text.Json;

namespace MyCSharpProject
{
    public class ManageMenu
    {
        public static bool running = true;
        public  static CafeMenu cafeMenu = new CafeMenu();
        public  static string filePath = "menu.txt";


        public static void Run()
        {
            Console.Clear();
            CafeMenu.LoadMenuFromFile(filePath);  // Load menu khi chạy chương trình

            Console.WriteLine("--- Quản Lý Menu Quán Cafe ---");
            Console.WriteLine("1. Thêm món");
            Console.WriteLine("2. Xóa món");
            Console.WriteLine("3. Cập nhật món");
            Console.WriteLine("4. Hiển thị menu");
            Console.WriteLine("5. Thoát");
            int choice = 0;
            while (true)
            {
                Console.Write("Nhập lựa chọn (1 - 5):");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại số từ 0 đến 4)");
                }
            }
            switch (choice)
            {
                case 1:
                    AddItem();
                    break;

                case 2:
                    RemoveItem();
                    break;

                case 3:
                    UpdateItem();
                    break;

                case 4:
                    DisplayMenuForCustomers();
                    break;

                case 5:
                    QUANLI.ManageEmployees();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
                }
            }

        public static void AddItem()
        {
            Console.Clear();
            bool addingItem = true;
            while (addingItem)
            {
                Console.Write("Nhập tên món (hoặc 'q' để quay lại): ");
                string name = Console.ReadLine();
                if (name.ToLower() == "q")
                {
                    Run();
                }
                int price = 0;
                while (true)
                {
                    Console.Write($"Nhập giá: ");
                    string input = Console.ReadLine().Trim();
                    if (int.TryParse(input, out price) && price > 0 )
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Lựa chọn không hợp lệ. Nhập lại số lớn hơn 0");
                    }
                }

                cafeMenu.AddItem(name, price);
                cafeMenu.SaveMenuToFile(filePath);
                Console.WriteLine("Nhấn Enter để quay lại menu chính...");
                Console.ReadLine();
                Console.Clear() ;
                Run();
                addingItem = false;
            }
            cafeMenu.SaveMenuToFile(filePath);
        }

        public static void RemoveItem()
        {
            ChooseTypeOfMenuItemsOfItemToRemove();
            cafeMenu.SaveMenuToFile(filePath);
        }

        public static void UpdateItem()
        {
            ChooseTypeOfMenuItemsOfItemToUpdate();
            cafeMenu.SaveMenuToFile(filePath);
        }


        public static void DisplayMenuForCustomers()
        {
            Console.Clear();
            cafeMenu.DisplayMenuByCategory();
            cafeMenu.SaveMenuToFile(filePath);
            Console.WriteLine("(Nhập phím bất kì để quay lại)");
            Console.ReadLine();
            Run();
        }

        public static void Exit()
        {
            running = false;
            Console.WriteLine("Đang thoát chương trình...");
            Console.ReadLine();
            cafeMenu.SaveMenuToFile(filePath);
        }
        public static void ChooseTypeOfMenuItemsOfItemToRemove()
        {
            Console.Clear();
            Console.WriteLine("------ Chọn món ------");
            Console.WriteLine("1. Cafe\n" +
                              "2. Đá xay\n" +
                              "3. Nước ép\n" +
                              "4. Trà sữa\n" +
                              "5. Bánh ngọt\n" +
                              "0. Quay lại\n");

            int choice = 0;
            while (true)
            {
                Console.WriteLine("Nhập lựa chọn (0 - 5): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >= 0 && choice <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại số từ 0 đến 4)");
                }
            }
            switch (choice)
            {
                case 0:
                    Run();
                    break;
                case 1:
                    ChooseMenuItemsToRemove(CafeMenu.Coffee, "Cafe");
                    break;
                case 2:
                    ChooseMenuItemsToRemove(CafeMenu.iceBlended, "Đá xay");
                    break;
                case 3:
                    ChooseMenuItemsToRemove(CafeMenu.juice, "Nước ép");
                    break;
                case 4:
                    ChooseMenuItemsToRemove(CafeMenu.milkTea, "Trà sữa");
                    break;
                case 5:
                    ChooseMenuItemsToRemove(CafeMenu.milkTea, "Bánh ngọt");
                    break;
            }
        }
        public static void ChooseMenuItemsToRemove(List<MenuItem> menuItems, string groupName)
        {
            Console.Clear();
            List<String> requirement = new List<String>();
            Console.WriteLine($"-> {groupName}");
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i].Name}");
            }
            Console.WriteLine($"0. Quay lại\n");
            int choice = 0;
            while (true)
            {
                Console.Write($"Chọn món (0-{menuItems.Count}): ");
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out choice) && choice >= 0 && choice <= menuItems.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Lựa chọn không hợp lệ. Nhập lại số từ 0 đến {menuItems.Count}");
                }
            }

            if (choice == 0)
            {
                ChooseTypeOfMenuItemsOfItemToRemove();
            }
            MenuItem selectedItem = menuItems[choice - 1];
            cafeMenu.RemoveItem(selectedItem.Name.Trim());
            cafeMenu.SaveMenuToFile(filePath);
            Console.WriteLine("(Nhập phím bất kì để quay lại)");
            Console.ReadKey();
            Run();
        }
        public static void ChooseTypeOfMenuItemsOfItemToUpdate()
        {
            Console.Clear();
            Console.WriteLine("------ Chọn món ------");
            Console.WriteLine("1. Cafe\n" +
                              "2. Đá xay\n" +
                              "3. Nước ép\n" +
                              "4. Trà sữa\n" +
                              "5. Bánh\n" +
                              "0. Quay lại\n");
            int choice = 0;
            while (true)
            {
                Console.Write("Nhập lựa chọn (0 - 5): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >= 0 && choice <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại số từ 0 đến 5)");
                }
            }
            switch (choice)
            {
                case 0:
                    Run();
                    break;
                case 1:
                    ChooseMenuItemsToUpdate(CafeMenu.Coffee, "Cafe");
                    break;
                case 2:
                    ChooseMenuItemsToUpdate(CafeMenu.iceBlended, "Đá xay");
                    break;
                case 3:
                    ChooseMenuItemsToUpdate(CafeMenu.juice, "Nước ép");
                    break;
                case 4:
                    ChooseMenuItemsToUpdate(CafeMenu.milkTea, "Trà sữa");
                    break;
                case 5:
                    ChooseMenuItemsToUpdate(CafeMenu.cake, "Bánh ngọt");
                    break;
            }
        }
        public static void ChooseMenuItemsToUpdate(List<MenuItem> menuItems, string groupName)
        {
            Console.Clear();
            List<String> requirement = new List<String>();
            Console.WriteLine($"-> {groupName}");
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i].Name}");
            }
            Console.WriteLine($"0. Quay lại\n");
            int choice = 0;
            while (true)
            {
                Console.Write($"Chọn món (0-{menuItems.Count}): ");
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out choice) && choice >= 0 && choice <= menuItems.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Lựa chọn không hợp lệ. Nhập lại số từ 0 đến {menuItems.Count}");
                }
            }

            if (choice == 0)
            {
                ChooseTypeOfMenuItemsOfItemToUpdate();
            }
            MenuItem selectedItem = menuItems[choice - 1];
            Console.Clear();
            ChooseTypeOfUpdate(selectedItem,menuItems,groupName);
        }
        public static void ChooseTypeOfUpdate(MenuItem selectedItem, List<MenuItem> menuItems, string groupName)
        {
            Console.Clear();
            int choice = 0;
            Console.WriteLine($"Cập nhật {selectedItem.Name}");
            Console.WriteLine("1. Tên.\n" +
                              "2. Giá.\n" +
                              "3. Quay lại.\n" +
                              "4. Thoát.");
            while (true)
            {
                Console.WriteLine("Nhập vào lựa chọn: ");
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 4)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn của bạn không hợp lệ. Vui lòng chọn lại số từ 1 đến 4.");
                }

            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Nhập vào tên mới: ");
                    string newName = Console.ReadLine().Trim();
                    cafeMenu.UpdateItem(selectedItem.Name.Trim(), selectedItem.Price, newName);
                    cafeMenu.SaveMenuToFile(filePath);
                    Console.WriteLine("Đã cập nhật tên mới.\n" +
                                      "(Nhấn phím bất kì để quay lại)");
                    Console.ReadKey();
                    ChooseTypeOfUpdate(selectedItem, menuItems,groupName);
                    break;
                case 2:
                    Console.Clear();
                    int price = 0;
                    while (true)
                    {
                        Console.Write("Nhập giá mới: ");
                        string input = Console.ReadLine().Trim();
                        if (int.TryParse(input, out price))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Nhập số nguyên");
                        }
                    }
                    cafeMenu.UpdateItem(selectedItem.Name.Trim(), price, selectedItem.Name);
                    cafeMenu.SaveMenuToFile(filePath);
                    Console.WriteLine("Đã cập nhật giá mới.\n" +
                                      "(Nhấn phím bất kì để quay lại)");
                    Console.ReadKey();
                    ChooseTypeOfUpdate(selectedItem,menuItems,groupName);
                    break;
                case 3:
                    ChooseMenuItemsToUpdate(menuItems, groupName);
                    break;
                case 4:
                    Run();
                    break;
            }
        }
    }
}
