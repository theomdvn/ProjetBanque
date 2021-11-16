using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
// Theo midavaine 24452

namespace ProjetBanque
{
    public class Client
    {
        string name { get; set; }
        string surname { get; set; }
        string mail { get; set; }
        int Current { get; set; }
        int Saving { get; set; }
        string PIN { get; set; }
        public Client(string Name, string Surname, string Mail)
        {
            this.name = Name;
            this.surname = Surname;
            this.mail = Mail;
            this.Current = 0;
            this.Saving = 0;
            char c = this.name[0];
            int yy = char.ToUpper(c) - 64;
            char d = this.surname[0];
            int zz = char.ToUpper(d) - 64;
            this.PIN = $"{yy}{zz}";


        } // Constructor of client class
        public static string AccountName(Client a)
        {
            char c = a.name[0];
            int yy = char.ToUpper(c) - 64;
            char d = a.surname[0];
            int zz = char.ToUpper(d) - 64;
            int nn = a.name.Length + a.surname.Length;
            
            return $"{a.name[0]}{a.surname[0]}-{nn}-{yy}-{zz}";
            // PIN = yyzz
        } // Function which return the account name of a client
        public static string Pin(Client a)
        {
            return a.PIN;
        } // Function which return the Secret PIN of a client
        public static void BalanceInfo(Client a)
        {
            Console.WriteLine($"Account of {a.FullName(a)}");
            if (a.Current > 0)
            {
                Console.Write($"Current balance : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(a.Current);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write($"Current balance : ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(a.Current);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (a.Saving > 0)
            {
                Console.Write($"Savings balance : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(a.Saving);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write($"Savings balance : ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(a.Saving);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        } // Functon which return, name and balance info of a client
        public int TotalBalance(Client a)
        {
            int total = a.Saving + a.Current;
            return total;
        } // Function which calculates and return total balance of a client
        public string FullName(Client a)
        {
            string Full = $"{a.name} {a.surname}";
            return Full;
        } // Function which return name + surname of a client in one string
        public static void DepositCurrent(Client a, int b)
        {
            DateTime dt = DateTime.Now;
            int old = a.Current;
            a.Current += b;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"A deposit of {b}$ has been made on the current account");
            Console.ForegroundColor = ConsoleColor.White;


            string file = $"{Client.AccountName(a)}-current"; ;
            string path = "C:/Users/theom/Desktop/Dorset C#";

            string fileToWrite = $"{path}/{file}";

            using (StreamWriter sw = File.AppendText(fileToWrite))
            {
                sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Current \t Deposit  \t {old} \t {a.Current}");
            }
        } // Function to do a deposit in the current account of a client
        public static void DepositSavings(Client a, int b)
        {

            int old = a.Saving;
            a.Saving += b;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"A deposit of {b}$ has been made on the saving account");
            Console.ForegroundColor = ConsoleColor.White;


            string file = $"{Client.AccountName(a)}-savings";
            string path = "C:/Users/theom/Desktop/Dorset C#";
            DateTime dt = DateTime.Now;
            string fileToWrite = $"{path}/{file}";

            using (StreamWriter sw = File.AppendText(fileToWrite))
            {
                sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Savings \t Deposit  \t {old} \t {a.Saving}");
            }
        } // Function to do a deposit in the savings account of a client
        public static void WithdrawalCurrent(Client a, int b)
        {
            int old = a.Current;
            if (a.Current < b)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Your withdrawal can't be fulfilled, the amount requested is not available in you're current account, you have {a.Current}$ available");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                a.Current -= b;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Your withdrawal has been fulfilled, you now have {a.Current} available");
                Console.ForegroundColor = ConsoleColor.White;


            }
            string file = $"{Client.AccountName(a)}-current"; ;
            string path = "C:/Users/theom/Desktop/Dorset C#";
            DateTime dt = DateTime.Now;
            string fileToWrite = $"{path}/{file}";

            using (StreamWriter sw = File.AppendText(fileToWrite))
            {
                sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Current \t Withdrawal \t {old} \t {a.Current}");
            }
        } // Function to do a withdrawal from the current account of a client
        public static void WithdrawalSavings(Client a, int b)
        {
            int old = a.Current;
            if (a.Current < b)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Your withdrawal can't be fulfilled, the amount requested is not available in you're current account, you have {a.Saving}$ available");
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                a.Current -= b;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Your withdrawal has been fulfilled, you now have {a.Saving} available");
                Console.ForegroundColor = ConsoleColor.White;


            }
            string file = $"{Client.AccountName(a)}-savings"; ;
            string path = "C:/Users/theom/Desktop/Dorset C#";
            DateTime dt = DateTime.Now;
            string fileToWrite = $"{path}/{file}";

            using (StreamWriter sw = File.AppendText(fileToWrite))
            {
                sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Savings \t Withdrawal \t {old} \t {a.Saving}");
            }
        } // Function to do a withdrawal from the savings account of a client
        public static void TransferToSavings(Client a, int b)
        {
            int oldC = a.Current;
            int oldS = a.Saving;
            if (a.Current < b)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Your transfer can't be fulfilled, the amount requested is not available in your current account, you have {a.Current}$ available in your current account");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                a.Current -= b;
                a.Saving += b; 
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Your transfer has been fulfilled, you now have {a.Saving}$ in your savings account");
                Console.ForegroundColor = ConsoleColor.White;

                string fileC = $"{Client.AccountName(a)}-current";
                string fileS = $"{Client.AccountName(a)}-savings";
                string path = "C:/Users/theom/Desktop/Dorset C#";

                string fileToWriteC = $"{path}/{fileC}";
                DateTime dt = DateTime.Now;
                using (StreamWriter sw = File.AppendText(fileToWriteC))
                {
                    sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Current \t Transfer \t {oldC} \t {a.Current}");
                }
                string fileToWriteS = $"{path}/{fileS}";
                using (StreamWriter sw = File.AppendText(fileToWriteS))
                {
                    sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Savings \t Transfer \t {oldS} \t {a.Saving}");
                }
            }
        } // Function which transfer funds from current to savings
        public static void TransferToCurrent(Client a, int b)
        {
            int oldC = a.Current;
            int oldS = a.Saving;
            if (a.Saving < b)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine($"Your transfer can't be fulfilled, the amount requested is not available in your saving account, you have {a.Saving}$ available in your savings account");
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                a.Current += b;
                a.Saving -= b;
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine($"Your transfer has been fulfilled, you now have {a.Current}$ in your current account");
                Console.ForegroundColor = ConsoleColor.White;


            }
            string fileC = $"{Client.AccountName(a)}-current";
            string fileS = $"{Client.AccountName(a)}-savings";
            string path = "C:/Users/theom/Desktop/Dorset C#";

            string fileToWriteC = $"{path}/{fileC}";
            DateTime dt = DateTime.Now;
            using (StreamWriter sw = File.AppendText(fileToWriteC))
            {
                sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Current \t Transfer \t {oldC} \t {a.Current}");
            }
            string fileToWriteS = $"{path}/{fileS}";
            using (StreamWriter sw = File.AppendText(fileToWriteS))
            {
                sw.WriteLine($"{dt.ToString("dddd, dd MMMM yyyy")} \t Savings \t Transfer \t {oldS} \t {a.Saving}");

            }
        } // Function which transfer funds from savings to current

    }
}

