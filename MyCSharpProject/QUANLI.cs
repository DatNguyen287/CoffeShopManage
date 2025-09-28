using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace MyCSharpProject
{
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ID { get; set; }

        public Employee(string name, int age, string id)
        {
            Name = name;
            Age = age;
            ID = id;
        }
    }

    public class QUANLI
    {
        public static List<Employee> employees = new List<Employee>();

        public static void ManageEmployees()
        {
            LoadEmployeesFromFile();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----- QUẢN LÝ -----");
                Console.WriteLine("1. Thêm nhân viên mới.");
                Console.WriteLine("2. Thời gian nhân viên đăng kí lịch làm.");
                Console.WriteLine("3. Hiện danh sách nhân viên.");
                Console.WriteLine("4. Xóa nhân viên.");
                Console.WriteLine("5. Thời gian check-in của nhân viên.");
                Console.WriteLine("6. Kho nguyên liệu.");
                Console.WriteLine("7. Quản lí menu.");
                Console.WriteLine("8. Lịch làm cho tuần tới.");
                Console.WriteLine("9. Doanh thu.");
                Console.WriteLine("10. Quay lại.");
                Console.Write("Nhập lựa chọn của bạn (1-10): ");

                string input = Console.ReadLine();
                int option;
                bool isParsed = int.TryParse(input, out option);

                if (isParsed)
                {
                    Console.Clear();
                    switch (option)
                    {
                        case 1:
                            AddEmployee();
                            break;
                        case 2:
                            CheckWorkHours();
                            break;
                        case 3:
                            DisplayEmployees();
                            break;
                        case 4:
                            DeleteEmployee();
                            break;
                        case 5:
                            CheckinE();
                            break;
                        case 6:
                            IngredientManager.ShowMenu();
                            break;
                        case 7:
                            ManageMenu.Run();
                            break;
                        case 8:
                            ManageSchedule();
                            break;
                        case 9:
                            Statictis.Run();
                            break;
                        case 10:
                            Console.WriteLine("Quay lại!");
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một số hợp lệ.");
                }
                Console.WriteLine();
            }
        }

        public static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("----- THÊM NHÂN VIÊN MỚI -----");
            Console.Write("Nhập tên nhân viên: ");
            string name = Console.ReadLine();

            int age;
            while (true)
            {
                Console.Write("Nhập tuổi nhân viên: ");
                string ageInput = Console.ReadLine();

                if (int.TryParse(ageInput, out age) && age > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một tuổi hợp lệ.");
                }
            }

            Console.Write("Nhập mã nhân viên: ");
            string id = Console.ReadLine();

            Employee newEmployee = new Employee(name, age, id);
            employees.Add(newEmployee);

            UpdateEmployeeFile();

            Console.WriteLine($"Đã thêm nhân viên: {name}, Tuổi: {age}, Mã nhân viên: {id}");
            Console.WriteLine("Nhấn Enter để tiếp tục...");
            Console.ReadLine();
        }

        public static void CheckWorkHours()
        {
            Console.Clear();
            Console.WriteLine("----- THỜI GIAN NHÂN VIÊN ĐĂNG KÍ CA LÀM -----"); 
            string filePath = "Schedule.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Chưa có thông tin đăng ký ca làm nào.");
            }
            else
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Chưa có thông tin đăng ký ca làm nào.");
                }
                else
                {
                    Console.WriteLine("Danh sách thời gian nhân viên đăng kí:");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            Console.WriteLine("\nNhấn Enter để quay lại.");
            Console.ReadLine();
        }
        public static void CheckinE()
        {
            Console.Clear();
            Console.WriteLine("----- THỜI GIAN NHÂN VIÊN VÀO CA LÀM -----"); 
            string filePath = "CheckIn.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Chưa có thông tin nào.");
            }
            else
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Chưa có thông tin đăng ký ca làm nào.");
                }
                else
                {
                    Console.WriteLine("Danh sách thời gian nhân viên vào ca:");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            Console.WriteLine("\nNhấn Enter để quay lại.");
            Console.ReadLine();
        }


        public static void DisplayEmployees()
        {
            Console.Clear();
            Console.WriteLine("----- DANH SÁCH NHÂN VIÊN -----");

            if (employees.Count == 0)
            {
                Console.WriteLine("Danh sách nhân viên trống.");
            }
            else
            {
                Console.WriteLine("Danh sách nhân viên:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"- Tên nhân viên: {employee.Name}, Tuổi: {employee.Age}, Mã nhân viên: {employee.ID}");
                }
            }

            Console.WriteLine("Nhấn Enter để quay lại.");
            Console.ReadLine();
        }

        public static void DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("----- XÓA NHÂN VIÊN -----");
            Console.Write("Nhập mã nhân viên cần xóa: ");
            string nameToDelete = Console.ReadLine();
            bool found = false;
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].ID == nameToDelete)
                {
                    employees.RemoveAt(i);
                    Console.WriteLine($"Đã xóa nhân viên: {nameToDelete}");
                    found = true;
                    break;
                }
            }
            if(!found)
            {
                Console.WriteLine("Không tìm thấy nhân viên với ID đã nhập.");

            }
            UpdateEmployeeFile();
            Console.WriteLine("Nhấn Enter để quay lại.");
            Console.ReadLine();
        }
        public static void ManageSchedule()
        {
            Console.Clear();
            Console.WriteLine("----- LỊCH LÀM CHO TUẦN TỚI -----");
            string[] daysOfWeek = new string[]
            {
                "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật"
            };
            List<string> weekSchedule = new List<string>();
            foreach (string day in daysOfWeek)
            {
                Console.WriteLine($"Nhập lịch làm cho {day}:");
                string ca1 = GetShiftForDay("Ca 9-13");
                string ca2 = GetShiftForDay("Ca 13-17");
                string ca3 = GetShiftForDay("Ca 17-22");
                string dailySchedule = $"{day} -> Ca 9-13: {ca1}, Ca 13-17: {ca2}, Ca 17-22: {ca3}";
                weekSchedule.Add(dailySchedule);
            }
            SaveScheduleToFile(weekSchedule);
            Console.WriteLine("Lịch làm cho tuần tới đã được lưu.");
            Console.WriteLine("Nhấn Enter để quay lại.");
            Console.ReadLine();
        }
        public static string GetShiftForDay(string shift)
        {
            Console.WriteLine($"Nhập tên nhân viên cho {shift}: ");
            string employeeName = Console.ReadLine();
            return employeeName;
        }
        public static void SaveScheduleToFile(List<string> weekSchedule)
        {
            string fileName = "ScheduleNextWeek.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    foreach (var dailySchedule in weekSchedule)
                    {
                        writer.WriteLine(dailySchedule);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu lịch làm vào file: {ex.Message}");
            }
        }

        private static void LoadEmployeesFromFile()
        {
            string fileName = "accounts.txt";
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 4)
                    {
                        string name = parts[0];
                        int age = int.Parse(parts[1]);
                        string id = parts[2];
                        employees.Add(new Employee(name, age, id));
                    }
                }
            }
        }


        public static void UpdateEmployeeFile()
        {
            string fileName = "accounts.txt";
            try
            {
                File.WriteAllText(fileName, string.Empty);
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    foreach (var employee in employees)
                    {
                        writer.WriteLine($"{employee.Name}:{employee.Age}:{employee.ID}:{"employee"}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật thông tin nhân viên vào file: {ex.Message}");
            }
        }
    }
}
