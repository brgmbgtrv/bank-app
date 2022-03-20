using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogatyrev_Project_2
{
    class Account : IEquatable<Account>
    {
        /* In this class and in inherited classes I often use Bank.Accounts.Find() method, because of which the code looks messy
         * however, I'm not doing it with no purpose
         * I'm using this method to reference to a certain object of Account classs, and because these objects are stored in List
         * I can't reference to them in any other way
         * So I find an account with given login and password, and further work with this account's fields/methods/properties etc       
         */


        public override string ToString()
        {
            return $"Login: {_login}\nPassword: {_password}";
        }

        public bool Equals(Account other)
        {
            if (this.ToString() == other.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
         
        public string _login { get; set; }
        public string _password { get; set; }

        public SalaryAccount SalaryAccount { get; set; }    
        public DepositAccount DepositAccount { get; set; }
        public CreditAccount CreditAccount { get; set; }

        private static Random _rnd = new Random();

        public static string CreateId()
        {
            const string numbers = "0123456789";
            return new string(Enumerable.Repeat(numbers, 16)
              .Select(s => s[_rnd.Next(s.Length)]).ToArray());
        }
        public virtual void Create() 
        {
            throw new NotImplementedException();
        }   // OBA

        public virtual void Show()
        {
            throw new NotImplementedException();
        }    // DB
        
        public static string _l;
        public static string _p;

        public static int YearsDiff(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }   // ST

        public static int MonthsDiff(DateTime val_1, DateTime val_2)
        {
            int months = 12 * (val_1.Year - val_2.Year) + val_1.Month - val_2.Month;
            return Math.Abs(months);
        }  // ST

        public static void PersonalAccount()
        {
            Console.Clear();
            Console.WriteLine($"{Bank.head}Chosse an option:");
            if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount == null)
            {
                Console.WriteLine("\n1) Press 'S' if you want to create a salary account.");
            }
            else
            {
                Console.WriteLine("\n1) Press 'S' if you want to open a salary account.");
            }
            if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount == null)
            {
                Console.WriteLine("2) Press 'D' if you want to create a deposit account.");
            }
            else
            {
                Console.WriteLine("2) Press 'D' if you want to open a deposit account.");
            }
            if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).CreditAccount == null)
            {
                Console.WriteLine("3) Press 'C' if you want to create a credit account.");
            }
            else
            {
                Console.WriteLine("3) Press 'S' if you want to open a credit account.");
            }
            Console.WriteLine($"4) Press 'M' to move to main page.{Bank.foot}");


            // Code fragment below is for replenishing salary account with salary every month
            if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount != null)    // SBA
            {
                if (MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.DateCreated, Bank.Date) > 0)
                {
                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Balance.MAdd2(Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Salary.MMul(MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.DateCreated, Bank.Date)));
                    Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.DateCreated = Bank.Date;
                }
            }

            /* Code fragment below is for reinvestment of interest.
             first if ... else statement checks whether this account has deposit account
             second if ... else statement checks whether this deposit account has reinvestment of interest (Y - body of deposit grows, N - salary account's balance grows)
             third if ... else statement checks whether this deposit's account reinvestment is annual or monthly (Y - annual, M - monthly)
             */
            if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount != null)
            {
                if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DepositInfo[4] == "Y" || Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DepositInfo[2] == "y")
                {
                    if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DepositInfo[3] == "Y" || Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DepositInfo[2] == "y")
                    {
                        if (YearsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date) > 0)
                        {
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MMul((float)Math.Pow((float)(DepositAccount.YearRate), (float)YearsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date)));
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated = Bank.Date;
                        }
                    } else
                    {
                        if (MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date) > 0)
                        {
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MAdd2(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MMul((float)Math.Pow((float)(DepositAccount.MonthlyRate), (float)MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date))));
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated = Bank.Date;
                        }
                    }
                } else
                {
                    if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DepositInfo[3] == "Y" || Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DepositInfo[2] == "y")
                    {
                        if (YearsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date) > 0)
                        {
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Balance.MAdd2(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MMul((float)Math.Pow((float)(DepositAccount.YearRate), (float)YearsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date))) - Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance);
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated = Bank.Date;
                        }
                    }
                    else
                    {
                        if (MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date) > 0)
                        {
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Balance.MAdd2(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MMul((float)Math.Pow((float)(DepositAccount.MonthlyRate), (float)MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date))) - Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance);
                            Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated = Bank.Date;
                        }
                    }
                }              
            }

            /* Code fragment below is for automatic replenishment of deposit account's balance from salary account's balance 
             * If DepositInfo[5] for a given deposit account is "Y", then every month deposit account balance increases by 10% of salary account's balance
             * and salary account's balance decreases by 10%
             */
            if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount != null)
            {
                if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DepositInfo[5] == "Y")
                {
                    if (MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date) > 0)
                    {
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Balance.MAdd2(Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Balance.MDiv1(10).MMul(MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date)));
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Balance.MSub2(Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Balance.MDiv1(10).MMul(MonthsDiff(Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.DateCreated, Bank.Date)));
                    }
                }
            }

            ConsoleKey consoleKey = Console.ReadKey().Key;
            switch (consoleKey)
            {
                case ConsoleKey.M:
                    Bank.MainPage();
                    break;
                case ConsoleKey.S:
                    if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount == null)
                    {
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount = new SalaryAccount();
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Create();
                        break;
                    }
                    else
                    {
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).SalaryAccount.Show();
                        break;
                    }
                case ConsoleKey.D:
                    if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount == null)
                    {
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount = new DepositAccount();
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Create();
                        break;
                    }
                    else
                    {
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).DepositAccount.Show();
                        break;
                    }
                case ConsoleKey.C:
                    if (Bank.Accounts.Find(x => x._login == _l & x._password == _p).CreditAccount == null)
                    {
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).CreditAccount = new CreditAccount();
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).CreditAccount.Create();
                        break;
                    }
                    else
                    {
                        Bank.Accounts.Find(x => x._login == _l & x._password == _p).CreditAccount.Show();
                        break;
                    }
                default:
                    break;
            }
        }  // DB

        public virtual void Transfer()
        {
            throw new NotImplementedException();
        }  // TMOA & TMOP

        public virtual void PutWithdraw()
        {
            throw new NotImplementedException();
        }  // PM & WM

        public virtual void CloseAccount()
        {
            throw new NotImplementedException();
        }  // CIBA
    }
}
