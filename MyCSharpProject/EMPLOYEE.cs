using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace MyCSharpProject
{
    public class EMPLOYEE
    {
        public static void Employees()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----NHÂN VIÊN-----");
                Console.WriteLine("1. Đăng kí ca làm.");
                Console.WriteLine("2. Check in.");
                Console.WriteLine("3. Quản lí hóa đơn.");
                Console.WriteLine("4. Lịch làm.");
                Console.WriteLine("5. Quay lại");
                Console.Write("Nhập lựa chọn của bạn (1-5): ");

                string input = Console.ReadLine();
                int option;
                bool isParsed = int.TryParse(input, out option);

                if (isParsed)
                {
                    Console.Clear();
                    switch (option)
                    {
                        case 1:
                            Register();
                            break;
                        case 2:
                            CheckIn();
                            break;
                        case 3:
                            manageBill.manageBills();
                            break;
                        case 4:
                            DisplayScheduleForNextWeek();
                            break;
                        case 5:
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
        public static void Register()
        {
            Console.WriteLine("Nhập tên nhân viên: ");
            string name= Console.ReadLine();

            Console.WriteLine("Thời gian rảnh thứ 2: ");
            string monday= Console.ReadLine();

            Console.WriteLine("Thời gian rảnh thứ 3: ");
            string tuesday= Console.ReadLine();

            Console.WriteLine("Thời gian rảnh thứ 4: ");
            string wednesday= Console.ReadLine();

            Console.WriteLine("Thời gian rảnh thứ 5: ");
            string thursday= Console.ReadLine();

            Console.WriteLine("Thời gian rảnh thứ 6: ");
            string friday= Console.ReadLine();

            Console.WriteLine("Thời gian rảnh thứ 7: ");
            string saturday= Console.ReadLine();

            Console.WriteLine("Thời gian rảnh chủ nhật: ");
            string sunday= Console.ReadLine();

            string time=$"{name}, T2: {monday}, T3: {tuesday}, T4: {wednesday}, T5: {thursday}, T6: {friday}, T7: {saturday}, CN: {sunday}";
            string filePath= "Schedule.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true)) 
                {
                    writer.WriteLine(time);
                }
            Console.WriteLine("Đăng kí ca làm thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi ghi vào tệp: {ex.Message}");
            }
        }
        public static void CheckIn()
        {
            Console.WriteLine("Nhập tên nhân viên: ");
            string name= Console.ReadLine();
            DateTime now = DateTime.Now;
            string checkInTime = now.ToString("dd/MM/yyyy HH:mm:ss");
            string record = $"{name}, Thời gian check-in: {checkInTime}";
            string filePath= "CheckIn.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true)) 
                {
                    writer.WriteLine(record);
                }
            Console.WriteLine("Đăng kí ca làm thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi ghi vào tệp: {ex.Message}");
            }
        }
        
        public static void DisplayScheduleForNextWeek()
        {
            Console.Clear();
            Console.WriteLine("----- LỊCH LÀM CỦA TUẦN TỚI -----");
            string fileName = "ScheduleNextWeek.txt";
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Chưa có lịch làm cho tuần tới.");
            }
            else
            {
                string[] lines = File.ReadAllLines(fileName);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Lịch làm cho tuần tới trống.");
                }
                else
                {
                    Console.WriteLine("Danh sách lịch làm cho tuần tới:");
                    foreach (var line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            Console.WriteLine("\nNhấn Enter để quay lại.");
            Console.ReadLine();
        }
    }
} 