using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogatyrev_Project_2
{
    static class Bank 
    {     
        static void Main(string[] args)
        {
            Accounts.Add(new Account() { _login = "admin", _password = "1234" });
            Accounts.Add(new Account() { _login = "admin1", _password = "11234" });
            MainPage();             
        }

        

        public static DateTime Date = new DateTime();

        public static string head = $"Welcome to \"Scrooge McDuck Enterprises\"!\n{new String('*', 75)}\n\n";
        public static string foot = $"\n\n{new String('*', 75)}\n(c) \"Scrooge McDuck Enterprises\"";

        public static void MainPage()
        {
            Date = DateTime.Now;
            Console.Clear();
            Console.WriteLine($"Current date: {Date}\n" +
                $"{head}1) Press 'R' if you want to register." +
               $"\n2) Press 'L' if you already have an account." +
               $"\n3) Press 'A' if you want to add 1 month to current date." +
               $"\n4) Press 'Esc' or any other key if you want to close this window.{foot}");

            ConsoleKey consoleKey = Console.ReadKey().Key;
            switch (consoleKey)
            {                
                case ConsoleKey.R:
                    RegisterPage();
                    break;
                case ConsoleKey.L:
                    LoginPage();
                    break;
                case ConsoleKey.A:
                    Date = Date.AddMonths(1);
                    MainPage();
                    break;
                case ConsoleKey.Escape:
                default:
                    break;
            }
        }

        public static List<Account> Accounts = new List<Account>();
        public static List<User> Users = new List<User>();
        

        private static void RegisterPage()    // UR
        {
            Console.Clear();
            Console.WriteLine($"{head}You are Bank.Date on a registration page.");
            Console.WriteLine("To register, follow instructions on screen:");
            Console.WriteLine("\n1) Enter your full name:");
            string _fName = Console.ReadLine();
            Console.WriteLine("2) Enter your birth date in DD/MM/YY format:");
            string _bDate = Console.ReadLine();
            Console.WriteLine("3) Enter your phone number:");
            string _phNumber = Console.ReadLine();
            Console.WriteLine("4) Create your login:");
            string _l = Console.ReadLine();
            Console.WriteLine("5) Create your password:");
            string _p = Console.ReadLine();
            Console.WriteLine("   Confirm your password:");
            string _pc = Console.ReadLine();
            Console.WriteLine($"\nYou have successfully registered in \"Scrooge McDuck Enterprises\"" +
                $"\nPress 'M' to move to main page or 'Esc' to close this window.{foot}");

            Users.Add(new User() { _fullName = _fName, _birthDate = _bDate, _phoneNumber = _phNumber });
            Accounts.Add(new Account() { _login = _l, _password = _p });

            ConsoleKey key = Console.ReadKey().Key;
            switch (key) 
            {
                case ConsoleKey.M:
                    MainPage();
                    break;
                case ConsoleKey.Escape:
                default:
                    break;
            }
            Console.ReadKey();
        }

        private static void LoginPage()
        {
            Console.Clear();
            Console.WriteLine($"{head}You are Bank.Date on a login page.");
            Console.WriteLine("\n1) Enter your login: ");
            string _l = Console.ReadLine();
            Account._l = _l;
            Console.WriteLine("2) Enter your password: ");
            string _p = Console.ReadLine();
            Account._p = _p;

            if (Accounts.Exists(x => x._login == _l & x._password == _p))
            {
                Console.Clear();
                Account.PersonalAccount();               
            } else
            {
                Console.Clear();
                Console.WriteLine($"{head}Incorrect credentials were given." +
                    $"\n1) Press 'L' to try again." +
                    $"\n2) Press 'R' to register." +
                    $"\n3) Press 'Esc' or any other key to close this window.{foot}");
                ConsoleKey key = Console.ReadKey().Key;
                switch(key)
                {
                    case ConsoleKey.L:
                        LoginPage();
                        break;
                    case ConsoleKey.R:
                        RegisterPage();
                        break;
                    case ConsoleKey.Escape:
                    default:
                        break;
                }
            }
        }          // UL
    }
}
