using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
namespace MyCSharpProject
{
    public class manageBill
    {
        static CafeMenu cafeMenu = new CafeMenu();
        public static List<MenuItem> menuItems = new List<MenuItem>(); 
        static List<BillItems> billItems = new List<BillItems>();
        public static int revenue;
        static List<Bill> allBills = new List<Bill>();
        static string filePath = "bills.txt";
        static List<MenuItem> Coffee = new List<MenuItem>();
        static List<MenuItem> iceBlended = new List<MenuItem>();
        static List<MenuItem> juice = new List<MenuItem>();
        static List<MenuItem> cake = new List<MenuItem>();
        static List<MenuItem> milkTea = new List<MenuItem>();
        static void cleanListMenuItem()
        {
            menuItems.Clear();
            Coffee.Clear();
            iceBlended.Clear();
            juice.Clear();
            cake.Clear();
            milkTea.Clear();
        }

        static void ReadMenuFromFile()
        {
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
        }

        static void ReadBillsFromFile()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Chưa có hóa đơn nào được lưu.");
                return;
            }

            if (File.Exists(filePath))
            {
                StackADT stack = new StackADT();
                // Đọc dữ liệu từ tệp
                string[] dataFromFile = File.ReadAllLines(filePath);
                foreach(string line in dataFromFile)
                {
                    string[] dataParts = line.Split('_');
                    string[] dishData = dataParts[2].Split(';');
                    // Phân tách và thêm các món ăn vào hóa đơn
                    List<BillItems> allBillItems = new List<BillItems>();
                    foreach (var dishInfo in dishData)
                    {
                        string[] dishParts = dishInfo.Split('-');
                        string name = dishParts[0];
                        int price = int.Parse(dishParts[2]);
                        int quantity = int.Parse(dishParts[1]);
                        string note = dishParts[3];
                        BillItems billItems = new BillItems(name, price, note, quantity);
                        allBillItems.Add(billItems);
                    }

                    // Tạo đối tượng Bill
                    DateTime paymentTime = DateTime.Parse(dataParts[0]);
                    int totalPrice = int.Parse(dataParts[1]);
                    Bill bill = new Bill(allBillItems, totalPrice, paymentTime);
                    bill.TotalPrice = totalPrice;
                    stack.Push(bill);
                }
                int id = stack.Count;
                while (stack.Count > 0)
                {
                    // Hiển thị thông tin hóa đơn
                    Bill temp = stack.Pop();    
                    Console.WriteLine($"Mã hóa đơn: GĐ{id}");
                    Console.WriteLine($"Thời gian: {temp.PaymentTime}");
                    Console.WriteLine("{0,-20} {1,-10} {2,-5} {3,-20} {4,-20}", "Tên sản phẩm", "Đơn giá", "", "Thành tiền", "Ghi chú");
                    foreach (var item in temp.Items)
                    {
                        int itemTotal = item.Quantity * item.Price;
                        Console.WriteLine("{0,-20} {1,-10} x{2,-4} {3,-20} {4,-20}", item.Name, item.Price, item.Quantity, itemTotal, item.Note);
                    }
                    Console.WriteLine("{0,-20} {1,-10} {2,-5} {3,-20} ", "Tổng tiền", "", "", temp.TotalPrice);
                    Console.WriteLine("--------------------------------------------------------");
                    id--;
                }    
            }    
            else
            {
                Console.WriteLine("Tệp không tồn tại.");
            }
        }
        static void ChooseSugar(List<String> requirement)
        {
            Console.WriteLine("\n-  Đường");
            Console.Write("1. Giữ nguyên.\n" +
                              "2. Ít đường.\n" +
                              "3. Nhiều đường.\n" +
                              "4. Không đường.");
            int choice = 0;  // Biến lưu giá trị người dùng nhập vào
            bool isValidChoice = false;  // Biến kiểm tra tính hợp lệ của lựa chọn

            while (!isValidChoice)  // Vòng lặp sẽ tiếp tục cho đến khi người dùng nhập hợp lệ
            {
                Console.Write("\nChọn lựa chọn(1 - 4): ");
                string input = Console.ReadLine().Trim();

                // Kiểm tra xem người dùng có nhập số hợp lệ và trong phạm vi từ 1 đến 4 không
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 4)
                {
                    isValidChoice = true;  // Nếu nhập hợp lệ, thoát khỏi vòng lặp
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại từ 1 đến 4.");
                }
            }
            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    requirement.Add("Ít đường");
                    break;
                case 3:
                    requirement.Add("Nhiều đường");
                    break;
                case 4:
                    requirement.Add("Không đường");
                    break;
            }
        }

        static void ChooseIce(List<String> requirement)
        {
            Console.WriteLine("\n-  Đá");
            Console.Write("1. Giữ nguyên.\n" +
                              "2. Ít đá.\n" +
                              "3. Nhiều đá.\n" +
                              "4. Không đá.");

            int choice = 0;  // Biến lưu giá trị người dùng nhập vào
            bool isValidChoice = false;  // Biến kiểm tra tính hợp lệ của lựa chọn

            while (!isValidChoice)  // Vòng lặp sẽ tiếp tục cho đến khi người dùng nhập hợp lệ
            {
                Console.Write("\nChọn lựa chọn(1 - 4): ");
                string input = Console.ReadLine().Trim();

                // Kiểm tra xem người dùng có nhập số hợp lệ và trong phạm vi từ 1 đến 4 không
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 4)
                {
                    isValidChoice = true;  // Nếu nhập hợp lệ, thoát khỏi vòng lặp
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại từ 1 đến 4.");
                }
            }


            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    requirement.Add("Ít đá");
                    break;
                case 3:
                    requirement.Add("Nhiều đá");
                    break;
                case 4:
                    requirement.Add("Không đá");
                    break;
            }
        }

        static void ChooseTopping(List<String> requirement)
        {
            Console.WriteLine("\n-  Topping");
            Console.Write("1. Trân châu đen.\n" +
                              "2. Trân châu trắng.\n" +
                              "3. Không.");
            int choice = 0;  // Biến lưu giá trị người dùng nhập vào
            bool isValidChoice = false;  // Biến kiểm tra tính hợp lệ của lựa chọn

            while (!isValidChoice)  // Vòng lặp sẽ tiếp tục cho đến khi người dùng nhập hợp lệ
            {
                Console.Write("\nChọn lựa chọn(1 - 3): ");
                string input = Console.ReadLine().Trim();

                // Kiểm tra xem người dùng có nhập số hợp lệ và trong phạm vi từ 1 đến 4 không
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 3)
                {
                    isValidChoice = true;  // Nếu nhập hợp lệ, thoát khỏi vòng lặp
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại từ 1 đến 4.");
                }
            }
            switch (choice)
            {
                case 1:
                    requirement.Add("Trân châu đen");
                    break;
                case 2:
                    requirement.Add("Trân châu trắng");
                    break;
                case 3:
                    break;
            }
        }

        public static void chooseMenuItems(List<MenuItem> menuItems, string groupName)
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
            while(true)
            {
                Console.WriteLine($"Chọn món (0-{menuItems.Count}): ");
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
                ChooseTypeOfMenuItems();
                return;
            }

            MenuItem selectedItem = menuItems[choice - 1];
            Console.Clear();
            Console.WriteLine($"-> {selectedItem.Name}");

            if (groupName == "Trà sữa")
            {
                ChooseTopping(requirement);
                ChooseSugar(requirement);
                ChooseIce(requirement);
            }
            else if (groupName == "Cafe")
            {
                ChooseSugar(requirement);
            }
            else if (groupName != "Bánh")
            {
                ChooseSugar(requirement);
                ChooseIce(requirement);
            }

            Console.Write("\nNhập vào số lượng: ");
            int quantity = int.Parse(Console.ReadLine());
            string note = "";
            note = string.Join(", ", requirement);
            BillItems newBillItem = new BillItems(selectedItem.Name, quantity, note, selectedItem.Price);

            Console.WriteLine($"Thông tin: {selectedItem.Name}({note}) x{quantity}\n");

            Console.WriteLine("1. Chọn tiếp.\n" +
                              "2. Thanh toán.\n" +
                              "3. Quay lại");
            choice = 0;
            while(true)
            {
                Console.Write("Nhập vào lựa chọn(1 - 3): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >=1 && choice <= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại từ 1 đến 3");
                }    
            }    
            switch (choice)
            {
                case 1:
                    billItems.Add(newBillItem);
                    ChooseTypeOfMenuItems();
                    break;
                case 2:
                    billItems.Add(newBillItem);
                    DisplayBill();
                    break;
                case 3:
                    ChooseTypeOfMenuItems();
                    break;
            }
        }

        public static void ChooseTypeOfMenuItems()
        {
            cleanListMenuItem();
            ReadMenuFromFile();
            Console.Clear();
            Console.WriteLine("------ Chọn món ------");
            Console.WriteLine("1. Cafe\n" +
                              "2. Đá xay\n" +
                              "3. Nước ép\n" +
                              "4. Trà sữa\n" +
                              "5. Bánh\n" +
                              "0. Quay lại\n");
            int choice = 0;
            while(true)
            {
                Console.Write("Nhập lựa chọn (0 - 5): ");
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
                    manageBills();
                    break;
                case 1:
                    chooseMenuItems(Coffee, "Cafe");
                    break;
                case 2:
                    chooseMenuItems(iceBlended, "Đá xay");
                    break;
                case 3:
                    chooseMenuItems(juice, "Nước ép");
                    break;
                case 4:
                    chooseMenuItems(milkTea, "Trà sữa");
                    break;
                case 5:
                    chooseMenuItems(cake, "Bánh");
                    break;
            }
        }

        static void DisplayBill()
        {
            Console.Clear();
            int total = 0;
            Console.WriteLine("{0,-20} {1,-10} {2,-5} {3,-20} {4,-20}", "Tên sản phẩm", "Đơn giá", "", "Thành tiền", "Ghi chú");
            foreach (var item in billItems)
            {
                int itemTotal = item.Quantity * item.Price;
                total += itemTotal;
                Console.WriteLine("{0,-20} {1,-10} x{2,-4} {3,-20} {4,-20}", item.Name, item.Price, item.Quantity, itemTotal, item.Note);
            }
            Console.WriteLine("{0,-20} {1,-10} {2,-5} {3,-20} ", "Tổng tiền", "", "", total);
            Console.WriteLine("\n1. Thanh toán.\n" +
                              "2. Hủy.");
            int choice = 0;
            while(true)
            {
                Console.Write("Nhập lựa chọn: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice <= 2 && choice >= 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại số từ 1 đến 2.");
                }    
            }    
            
            if (choice == 1)
            {
                foreach (var billItem in billItems)
                {
                    UpdateSellQuantity(billItem.Name, billItem.Quantity);
                }
                SaveMenuToFile("menu.txt");
                revenue = ReadRevenueToFile() + total;
                SaveRevenueToFile();
                DateTime paymentTime = DateTime.Now;
                Bill newBill = new Bill(billItems, total, paymentTime);
                allBills.Add(newBill);
                Console.Clear();
                File.AppendAllText(filePath, newBill.ToFileFormat());
                Console.WriteLine("Đã thanh toán!"); 
                Console.WriteLine("(Nhập nút bất kì để quay lại!)");
                Console.ReadKey();
                ChooseTypeOfMenuItems();
            }
            else if (choice == 2)
            {
                billItems.Clear();
                ChooseTypeOfMenuItems();
            }
        }


        static void UpdateSellQuantity(string itemName, int quantity)
        {
            var menuItem = menuItems.Find(item => item.Name == itemName);
            if (menuItem != null)
            {
                menuItem.SellQuantity += quantity;
                return;
            }
        }
        static void DisplayAllBills()
        {
            Console.Clear();
            Console.WriteLine("\n(Nhập phím bất kì để quay lại)");
            ReadBillsFromFile();
            Console.ReadKey();
            manageBills();
        }
        public static void manageBills()
        {
            Console.Clear();
            Console.WriteLine("------ Quản lí hóa đơn -----");
            Console.WriteLine("1. Tạo 1 hóa đơn mới.\n" +
                              "2. Lịch sử hóa đơn.\n"+
                              "0. Quay lại.\n");
            int choice = 0;
            while (true)
            {
                Console.Write("Nhập lựa chọn: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice <= 2 && choice >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại số từ 0 đến 2.");
                }
            }
            switch(choice)
            {
                case 0:
                    EMPLOYEE.Employees();
                    break;
                case 1:
                    ChooseTypeOfMenuItems();
                    break;
                case 2:
                    DisplayAllBills();
                    break;
            }
        }

        public static void SaveMenuToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
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
        public static void SaveRevenueToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("revenue.txt", false))
                {
                    writer.WriteLine(revenue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi lưu file: {ex.Message}");
            }
        }
        public static int ReadRevenueToFile()
        {
            string fileName = "revenue.txt";
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File doanh thu không tồn tại, tạo mới.");
                return 0;
            }

            string[] lines = File.ReadAllLines(fileName);
            if (lines.Length > 0 && int.TryParse(lines[0], out int revenue))
            {
                return revenue;
            }
            else
            {
                Console.WriteLine("Dữ liệu doanh thu không hợp lệ, tạo mới.");
                return 0;
            }
        }
    }
}



