using System;
using System.Collections.Generic;
using System.IO;
namespace MyCSharpProject
{
    public class Ingredient
    {
        public string Ten { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }

        public Ingredient(string ten, int soLuong, string donVi)
        {
            Ten = ten;
            SoLuong = soLuong;
            DonVi = donVi;
        }

        public override string ToString()
        {
            return $"{Ten,-15} {SoLuong,-10} {DonVi}";
        }
    }

    public class IngredientManager
    {
        static List<Ingredient> ingredients = new List<Ingredient>();
        static string filePath = "Nguyenlieu.txt"; 
        public static void ShowMenu()
        {
             Console.Clear();
            LoadIngredientsFromFile();
            DisplayIngredients();
            Console.WriteLine("1. Thêm nguyên liệu");
            Console.WriteLine("2. Xoá nguyên liệu");
            Console.WriteLine("3. Cập nhật nguyên liệu");
            Console.WriteLine("4. Quay lại"); 
            Console.Write("Chọn các chức năng (1-4): ");
        
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddIngredient();
                    break;
                case "2":
                    RemoveIngredient();
                    break;
                case "3":
                    EditIngredient();
                    break;
                case "4": 
                    return ; 
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại.");
                    Console.ReadKey();
                    break;
            }
        }

        public static void DisplayIngredients()
        {
            Console.Clear();
            Console.WriteLine("Nguyên liệu hiện có:");
            Console.WriteLine("{0,-15} {1,-10} {2}", "Tên nguyên liệu", "Số lượng", "Đơn vị");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine(ingredient);
            }
        }

        public static void AddIngredient()
        {
            Console.Clear();
            Ingredient ingredient = InputIngredient();
            ingredients.Add(ingredient);
            Console.WriteLine("Đã thêm nguyên liệu.");
            SaveIngredientsToFile();  // Lưu lại vào file sau khi thêm
            Console.WriteLine("Nhấn Enter để tiếp tục...");
            Console.ReadLine();
            ShowMenu();
        }

        public static void RemoveIngredient()
        {
            Console.Clear();
            Console.WriteLine("Chọn nguyên liệu cần xoá:");
            DisplayIngredientList();
            while (true)
            {
                Console.Write("Nhập số thứ tự của nguyên liệu cần xoá: ");
                string input = Console.ReadLine().Trim();
                int index;
                if (int.TryParse(input, out index) && index > 0 && index <= ingredients.Count)
                {
                    Ingredient ingredient = ingredients[index - 1];
                    ingredients.RemoveAt(index - 1);
                    Console.WriteLine($"Đã xoá nguyên liệu: {ingredient.Ten}");
                    SaveIngredientsToFile();  // Lưu lại vào file sau khi xoá
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    continue;
                }
            }
            Console.ReadKey();
            ShowMenu();
        }

        public static void EditIngredient()
        {
            Console.Clear();
            Console.WriteLine("Chọn nguyên liệu cần cập nhật:");
            DisplayIngredientList();
            Console.Write("Nhập số thứ tự của nguyên liệu cần cập nhật: ");
            int index;

            if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= ingredients.Count)
            {
                Ingredient ingredient = ingredients[index - 1];
                Console.WriteLine($"Bạn đã chọn: {ingredient.Ten}");

                Console.WriteLine("Bạn muốn cập nhật thuộc tính nào?");
                Console.WriteLine("1. Số lượng");
                Console.WriteLine("2. Đơn vị");
                Console.Write("Chọn thuộc tính để cập nhật (1-2): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Nhập số lượng mới: ");
                        int soLuongMoi;
                        while (!int.TryParse(Console.ReadLine(), out soLuongMoi) || soLuongMoi <= 0)
                        {
                            Console.Write("Vui lòng nhập lại số lượng hợp lệ: ");
                        }
                        ingredient.SoLuong = soLuongMoi;
                        Console.WriteLine("Số lượng đã được cập nhật.");
                        // Console.WriteLine("Bấm enter để thoát hoặc chọn chỉnh sửa tiếp.");
                        break;

                    case "2":
                        Console.Write("Nhập đơn vị mới: ");
                        string donViMoi = Console.ReadLine();
                        ingredient.DonVi = donViMoi;
                        Console.WriteLine("Đơn vị đã được cập nhật.");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
                SaveIngredientsToFile();  // Lưu lại vào file sau khi cập nhật
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ.");
            }
            Console.ReadKey();
            ShowMenu();
        }

        public static void DisplayIngredientList()
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                Console.WriteLine($"{i+1} {ingredients[i].Ten} - Số lượng: {ingredients[i].SoLuong} {ingredients[i].DonVi}");
            }
        }

        public static Ingredient InputIngredient()
        {
            Console.Clear();
            string name;
            do
            {
                Console.Write("Nhập tên nguyên liệu: ");
                name = Console.ReadLine().Trim(); 
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Tên nguyên liệu không được để trống. Vui lòng nhập lại.");
                }
            } while (string.IsNullOrEmpty(name));  

            int quantity;
            do
            {
                Console.Write("Nhập số lượng: ");
            } while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0);

            Console.Write("Nhập đơn vị: ");
            string unit = Console.ReadLine();

            return new Ingredient(name, quantity, unit);
        }

        public static void SaveIngredientsToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var ingredient in ingredients)
                {
                    writer.WriteLine($"{ingredient.Ten},{ingredient.SoLuong},{ingredient.DonVi}");
                }
            }
        }

        public static void LoadIngredientsFromFile()
        {
            if (File.Exists(filePath))
            {
                ingredients.Clear();  // Xoá danh sách hiện tại trước khi nhập từ file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(',');

                        if (parts.Length == 3)
                        {
                            string name = parts[0];
                            int quantity;
                            if (int.TryParse(parts[1], out quantity))
                            {
                                string unit = parts[2];
                                Ingredient ingredient = new Ingredient(name, quantity, unit);
                                ingredients.Add(ingredient);
                            }
                        }
                    }
                }
            }
        }
    }

} 