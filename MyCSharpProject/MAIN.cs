using System;
using System.IO;
using System.Text;
namespace MyCSharpProject
{
    public class Mains
    {
        public static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            while (true) 
            {
                Console.WriteLine("Chào mừng đến với hệ thống quản lý nhân viên nhà DE.");
                Console.WriteLine("Vui lòng chọn vai trò:");
                Console.WriteLine("1. Đăng nhập với tư cách Quản lý.");
                Console.WriteLine("2. Đăng nhập với tư cách Nhân viên.");
                Console.WriteLine("3. Thoát.");
                Console.Write("Nhập lựa chọn của bạn: ");
                string input = Console.ReadLine();
                int roleChoice;
                bool isParsed = int.TryParse(input, out roleChoice);

                if (isParsed)
                {
                    switch (roleChoice)
                    {
                        case 1:
                            Console.Clear();
                            Login("manager");
                            break;
                        case 2:
                            Console.Clear();
                            Login("employee");
                            break;
                        case 3:
                            Console.WriteLine("Cảm ơn bạn đã sử dụng hệ thống.");
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập số hợp lệ.");
                }
            }
        }

        public static void Login(string role)
        {
            bool check = false;
            while (!check)
            {
                Console.Write("Tên đăng nhập:");
                string username = Console.ReadLine();
                Console.Write("Mật khẩu: ");
                string password = Console.ReadLine();

                if (CheckTK(username, password, role))
                {
                    check = true;
                    Console.Clear();
                    if (role == "manager")
                    {
                        Console.WriteLine("-----Quản Lí-----");
                        Console.WriteLine("Đăng nhập thành công!");
                        QUANLI.ManageEmployees();  
                    }
                    if (role == "employee")
                    {
                        Console.WriteLine("-----Nhân viên-----");
                        Console.WriteLine("Đăng nhập thành công");
                        EMPLOYEE.Employees();     // Nhân viên  
                    }
                }
                else
                {
                    Console.WriteLine("Tên đăng nhập hoặc mật khẩu không chính xác. Vui lòng thử lại.");
                }
            }
        }

        public static bool CheckTK(string username, string password, string role)
        {
            string[] lines;
            if (role == "manager")
            {
                lines = File.ReadAllLines("admin.txt"); 
                foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 3)
                {
                    string fileUsername = parts[0];
                    string filePassword = parts[1];
                    string fileRole = parts[2];

                    if (fileUsername == username && filePassword == password && fileRole == role)
                    {
                        return true;
                    }
                }
            }
            return false;
            }
            else if (role == "employee")
            {
                lines = File.ReadAllLines("accounts.txt"); 
                foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 4)
                {
                    string fileUsername = parts[0];
                    string filePassword = parts[2];
                    string fileRole = parts[3];

                    if (fileUsername == username && filePassword == password && fileRole == role)
                    {
                        return true;
                    }
                }
            }
            return false;
            }
            else
            {
                return false;
            }
        }
    }
}
