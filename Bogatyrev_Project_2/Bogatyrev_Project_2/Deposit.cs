using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogatyrev_Project_2
{
    class DepositAccount : Account
    {
        public Money Balance { get; set; }
        public string Id { get; set; }

        public DateTime Term { get; set; }    // ST

        public DateTime DateCreated { get; set; }    // ST

        public DepositAccount()
        {
            this.Balance = new Money();
        }

        public static float YearRate = 1.05f;

        public static float MonthlyRate = 1.005f;

        public string[] DepositInfo = new string[5];

        

        public override void Create()    // OBA & DBA
        {
            Console.Clear();
            Console.WriteLine($"{Bank.head}" +
                $"To create a deposit account, follow instructions of screen:" +
                $"\n\n1) Enter a wanted size of your deposit: ");
            Balance = new Money(Console.ReadLine());
            Console.WriteLine($"\n2) Enter 'M' or 'Y' if you want to create a deposit for monthly or annual term respectively:");
            DepositInfo[0] = Console.ReadLine();
            Console.WriteLine("\n   Enter number of months/years for your deposit term: ");
            DepositInfo[1] = Console.ReadLine();
            Console.WriteLine("3) Enter 'M' or 'Y' for monthly or annual period of calculating interest respectively:");
            DepositInfo[2] = Console.ReadLine();
            if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount != null)
            {
                Console.WriteLine("4) Enter 'Y' or 'N' if you opt for or don't opt for a reinvestment of interest respectively: ");
                DepositInfo[3] = Console.ReadLine();
                Console.WriteLine("5) Enter 'Y' or 'N' if you opt for or don't opt for monthly automatic replenishment of the deposit from the salary account respectively: ");
                DepositInfo[4] = Console.ReadLine();
            }

            if (DepositInfo[0] == "Y" || DepositInfo[0] == "y")
            {
                Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Term = Bank.Date.AddYears(int.Parse(DepositInfo[1]));
            } else if (DepositInfo[0] == "M" || DepositInfo[0] == "m")
            {
                Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Term = Bank.Date.AddMonths(int.Parse(DepositInfo[1]));
            }

            DateCreated = DateTime.Now;

            Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Id = Account.CreateId();

            Console.WriteLine($"\nYou have successfully created your deposit account with." +
                $"\n\n1) Press 'P' to navigate to your personal account." +
                $"\n2) Press 'M' to navigate to main page." +
                $"\n3) Press 'Esc' or any other key to close this window{Bank.foot}");

            ConsoleKey consoleKey = Console.ReadKey().Key;
            switch (consoleKey)
            {
                case ConsoleKey.P:
                    PersonalAccount();
                    break;
                case ConsoleKey.M:
                    Bank.MainPage();
                    break;
                case ConsoleKey.Escape:
                default:
                    break;
            }
        }

        public override void Show()
        {
            Console.Clear();
            Console.WriteLine($"{Bank.head}" +
                $"Your deposit: {Balance}" +
                $"\nTerm expires: {Term.ToString()}");
            Console.WriteLine($"\n\n1) Press 'P' to navigate to your personal account." +
                $"\n2) Press 'M' to navigate to main page." +
                $"\n3) Press 'T' to make a transfer." +
                $"\n4) Press 'W' to put or withdraw money from salary account." +
                $"\n5) Press 'C' to close this account." +
                $"\n3) Press 'Esc' or any other key to close this window.{Bank.foot}");

            ConsoleKey consoleKey = Console.ReadKey().Key;
            switch (consoleKey)
            {
                case ConsoleKey.P:
                    PersonalAccount();
                    break;
                case ConsoleKey.M:
                    Bank.MainPage();
                    break;
                case ConsoleKey.T:
                    Transfer();
                    break;
                case ConsoleKey.W:
                    PutWithdraw();
                    break;
                case ConsoleKey.C:
                    CloseAccount();
                    break;
                case ConsoleKey.Escape:
                default:
                    break;
            }
        } // DB
    
        public override void Transfer()
        {
            if (DateTime.Compare(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Term, Bank.Date) <= 0)
            {
                Console.Clear();
                Console.WriteLine($"{Bank.head}" +
                    $"\n\n1) Press 'I' if you want to transfer your money to one of your accounts." +
                    $"\n2) Press 'E' if you want to transfer your money to another user." +
                    $"\n3) Press 'P' to navigate to your personal account." +
                    $"\n4) Press 'M' to navigate to main page.");

                ConsoleKey consoleKey = Console.ReadKey().Key;
                switch (consoleKey)
                {
                    case ConsoleKey.I:
                        Console.Clear();
                        Console.WriteLine($"{Bank.head}" +
                            $"\n\n1) Press 'S' if you want to transfer your money to your salary account." +
                            $"\n2) Press 'C' if you want to transfer your money to your credit account." +
                            $"\n3) Press 'P' to navigate to your personal account." +
                            $"\n4) Press 'M' to navigate to main page.");

                        ConsoleKey consoleKey1 = Console.ReadKey().Key;
                        switch (consoleKey1)
                        {
                            case ConsoleKey.S:
                                if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount != null)
                                {
                                    Console.WriteLine($"\nEnter an amount of money that you want to transfer to your salary account: ");
                                    Money _transfer = new Money(Console.ReadLine());
                                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MSub2(_transfer);
                                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Balance.MAdd2(_transfer);
                                    Console.WriteLine($"\n\n{_transfer} transfered from your deposit account to your salary account." +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKey2 = Console.ReadKey().Key;
                                    switch (consoleKey2)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                    break;
                                } else
                                {
                                    Console.WriteLine($"\n You do not have a salary account yet." +
                                    $"\n\n1) Press 'P' to navigate to your personal account." +
                                    $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKeyq = Console.ReadKey().Key;
                                    switch (consoleKeyq)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                    break;
                                }
                                
                            case ConsoleKey.C:
                                if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).CreditAccount != null)
                                {
                                    Console.WriteLine($"\nEnter an amount of money that you want to transfer to your credit account: ");
                                    Money _transfer1 = new Money(Console.ReadLine());
                                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MSub2(_transfer1);
                                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).CreditAccount.Balance.MAdd2(_transfer1);
                                    Console.WriteLine($"\n\n{_transfer1} transfered from your deposit account to your credit account." +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKey3 = Console.ReadKey().Key;
                                    switch (consoleKey3)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                    break;
                                } else
                                {
                                    Console.WriteLine($"\n You do not have a credit account yet." +
                                    $"\n\n1) Press 'P' to navigate to your personal account." +
                                    $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKeyq = Console.ReadKey().Key;
                                    switch (consoleKeyq)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                    break;
                                }                               
                        }
                        break;
                    case ConsoleKey.E:
                        Console.Clear();
                        Console.WriteLine($"{Bank.head}" +
                            $"\n\n1) Press 'S' if you want to transfer your money yo another salary account." +
                            $"\n2) Press 'D' if you want to transfer your money to another deposit account." +
                            $"\n3) Press 'C' if you want to transfer your money to another credit account." +
                            $"\n4) Press 'P' to navigate to your personal account." +
                            $"\n5) Press 'M' to navigate to main page.");

                        ConsoleKey consoleKey4 = Console.ReadKey().Key;
                        switch (consoleKey4)
                        {
                            case ConsoleKey.S:
                                Console.WriteLine($"\nEnter the ID of the account that you want to transfer money to: ");
                                string _id2 = Console.ReadLine();
                                if (Bank.Accounts.Exists(x => x.SalaryAccount.Id == _id2))
                                {
                                    Console.WriteLine($"Enter an amount of money that you want to transfer:");
                                    Money _transfer2 = new Money(Console.ReadLine());
                                    Bank.Accounts.Find(x => x.SalaryAccount.Id == _id2).SalaryAccount.Balance.MAdd2(_transfer2);
                                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MSub2(_transfer2);
                                    Console.WriteLine($"\n\n{_transfer2} transfered from your deposit account." +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKey7 = Console.ReadKey().Key;
                                    switch (consoleKey7)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"Salary account with ID: {_id2} dows not exist." +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKey7 = Console.ReadKey().Key;
                                    switch (consoleKey7)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                }
                                break;
                            case ConsoleKey.D:
                                Console.WriteLine($"\nEnter the ID of the account that you want to transfer money to: ");
                                string _id = Console.ReadLine();
                                if (Bank.Accounts.Exists(x => x.DepositAccount.Id == _id))
                                {
                                    if (DateTime.Compare(Bank.Accounts.Find(x => x.DepositAccount.Id == _id).DepositAccount.Term, Bank.Date) <= 0)
                                    {
                                        Console.WriteLine($"Enter an amount of money that you want to transfer:");
                                        Money _transfer = new Money(Console.ReadLine());
                                        Bank.Accounts.Find(x => x.DepositAccount.Id == _id).DepositAccount.Balance.MAdd2(_transfer);
                                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MSub2(_transfer);
                                        Console.WriteLine($"\n\n{_transfer} transfered from your deposit account." +
                                            $"\n\n1) Press 'P' to navigate to your personal account." +
                                            $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                        ConsoleKey consoleKey5 = Console.ReadKey().Key;
                                        switch (consoleKey5)
                                        {
                                            case ConsoleKey.P:
                                                PersonalAccount();
                                                break;
                                            case ConsoleKey.M:
                                                Bank.MainPage();
                                                break;
                                        }
                                    } else
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Denied. Addressed account cannot be changed until its term is expired." +
                                            $"\n\n1) Press 'P' to navigate to your personal account." +
                                            $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                        ConsoleKey consoleKey2 = Console.ReadKey().Key;
                                        switch (consoleKey2)
                                        {
                                            case ConsoleKey.P:
                                                PersonalAccount();
                                                break;
                                            case ConsoleKey.M:
                                                Bank.MainPage();
                                                break;
                                        }
                                        break;
                                    }                                       
                                }
                                else
                                {
                                    Console.WriteLine($"Deposit account with ID: {_id} dows not exist." +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKey7 = Console.ReadKey().Key;
                                    switch (consoleKey7)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                }
                                break;
                            case ConsoleKey.C:
                                Console.WriteLine($"\nEnter the ID of the account that you want to transfer money to: ");
                                string _id1 = Console.ReadLine();
                                if (Bank.Accounts.Exists(x => x.CreditAccount.Id == _id1))
                                {
                                    Console.WriteLine($"Enter an amount of money that you want to transfer:");
                                    Money _transfer1 = new Money(Console.ReadLine());
                                    Bank.Accounts.Find(x => x.CreditAccount.Id == _id1).CreditAccount.Balance.MAdd2(_transfer1);
                                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MSub2(_transfer1);
                                    Console.WriteLine($"\n\n{_transfer1} transfered from your deposit account." +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKey6 = Console.ReadKey().Key;
                                    switch (consoleKey6)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Credit account with ID: {_id1} dows not exist." +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                                    ConsoleKey consoleKey7 = Console.ReadKey().Key;
                                    switch (consoleKey7)
                                    {
                                        case ConsoleKey.P:
                                            PersonalAccount();
                                            break;
                                        case ConsoleKey.M:
                                            Bank.MainPage();
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.P:
                        PersonalAccount();
                        break;
                    case ConsoleKey.M:
                        Bank.MainPage();
                        break;
                    case ConsoleKey.Escape:
                    default:
                        break;
                }
            } else
            {
                Console.Clear();
                Console.WriteLine($"{Bank.head}" +
                    $"Denied in transfering because term is not experied yet. You will be able to transfer your money after {Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Term}" +
                                        $"\n\n1) Press 'P' to navigate to your personal account." +
                                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                ConsoleKey consoleKey7 = Console.ReadKey().Key;
                switch (consoleKey7)
                {
                    case ConsoleKey.P:
                        PersonalAccount();
                        break;
                    case ConsoleKey.M:
                        Bank.MainPage();
                        break;
                }
            }
        }// TMOA & TMOP

        public override void PutWithdraw()
        {
            Console.Clear();
            Console.WriteLine($"{Bank.head}" +
                $"\n1) Press 'I' to put money on your deposit account." +
                $"\n2) Press 'O' to withdraw money from your deposit account." +
                $"\n3) Press 'P' to navigate to your personal account." +
                $"\n4) Press 'M' to navigate to main page.{Bank.foot}");

            ConsoleKey consoleKey = Console.ReadKey().Key;
            switch (consoleKey)
            {
                // if term is out of date

                case ConsoleKey.I:
                    Console.Clear();
                    Console.WriteLine($"{Bank.head}Enter an amount of money you want to put on your deposit account: ");
                    Money _in = new Money(Console.ReadLine());

                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MAdd2(_in);

                    Console.WriteLine($"\nYour deposit account was replenished with {_in}" +
                        $"\n\n1) Press 'P' to navigate to your personal account." +
                        $"\n2) Press 'M' to navigate to main page.");
                    ConsoleKey consoleKey1 = Console.ReadKey().Key;

                    switch (consoleKey1)
                    {
                        case ConsoleKey.P:
                            PersonalAccount();
                            break;
                        case ConsoleKey.M:
                            Bank.MainPage();
                            break;
                        case ConsoleKey.Escape:
                        default:
                            break;
                    }
                    break;
                case ConsoleKey.O:
                    Console.Clear();
                    Console.WriteLine($"{Bank.head}Enter an amount of money you want to withdraw from your deposit account: ");

                    Money _out = new Money(Console.ReadLine());
                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MSub2(_out);

                    Console.WriteLine($"\n{_out} have been withdrawn from your deposit account" +
                        $"\n\n1) Press 'P' to navigate to your personal account." +
                        $"\n2) Press 'M' to navigate to main page.{Bank.foot}");
                    ConsoleKey consoleKey2 = Console.ReadKey().Key;

                    switch (consoleKey2)
                    {
                        case ConsoleKey.P:
                            PersonalAccount();
                            break;
                        case ConsoleKey.M:
                            Bank.MainPage();
                            break;
                        case ConsoleKey.Escape:
                        default:
                            break;
                    }
                    break;
            }
        }// PM & WM

        public override void CloseAccount()
        {
            if (DateTime.Compare(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Term, Bank.Date) <= 0)
            {
                Console.Clear();
                Console.WriteLine($"{Bank.head}" +
                    $"Do you really want to close your deposit account? If yes, all money (if any) from your deposit account will be withdrawn." +
                    $"\nPress 'Y' if yes or press'N' if no.");
                ConsoleKey consoleKey = Console.ReadKey().Key;

                switch (consoleKey)
                {
                    case ConsoleKey.Y:
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MSub2(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance);
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount = null;
                        PersonalAccount();
                        break;
                    case ConsoleKey.N:
                        PersonalAccount();
                        break;
                    default:
                        break;
                }               
            } else
            {
                Console.Clear();
                Console.WriteLine($"{Bank.head}" +
                    $"Denied in closing your deposit account. You cannot close it until term is expired." +
                    $"\n\n1) Press 'P' to navigate to your personal account." +
                    $"\n2) Press 'M' to navigate to main page.{Bank.foot}");

                ConsoleKey consoleKey1 = Console.ReadKey().Key;
                switch (consoleKey1)
                {
                    case ConsoleKey.P:
                        PersonalAccount();
                        break;
                    case ConsoleKey.M:
                        Bank.MainPage();
                        break;
                    default:
                        break;
                }          
            }
        }    // CIBA
    }
}
