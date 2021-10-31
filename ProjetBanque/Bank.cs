using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ProjetBanque
{
    public class Bank
    {
        private List<Client> customers;
        string name { get; set; }
        public Bank(string Name)
            {
            this.name = Name;
            List<Client> Bank = new List<Client>();
            this.customers = Bank;
            }
        public List<Client> Customers
        {
             get { return this.customers; } 
        }
        public void view_balances()

        {
            foreach (Client customers in this.Customers)
            {
                Console.WriteLine(Client.AccountName(customers));
                Client.BalanceInfo(customers);
            }
        } // Function which prints account name, name of the customer and his savings and current balance
        public static Client Create(string Name, string Surname, string Mail, Bank b) // Function which creates a customer, add it to a bank and to the customers.txt also it creates the current and savings history file 
        {
          
            Client customer = new Client(Name.ToLower(), Surname.ToLower(), Mail.ToLower());
            b.Customers.Add(customer);
            Console.WriteLine($"A customer account has been successfully created for {customer.FullName(customer)}");
            Console.WriteLine($"The account number is {Client.AccountName(customer)} and your PIN is {Client.Pin(customer)}");
            string fileC = $"{Client.AccountName(customer)}-current";
            string fileS = $"{Client.AccountName(customer)}-savings";
            string path = "C:/Users/theom/Desktop/Dorset C#";
            string fileToWriteC = $"{path}/{fileC}";
            DateTime dt = DateTime.Now;
            using (StreamWriter sw = new StreamWriter(fileToWriteC))
            {
                sw.WriteLine($"Account creation : {dt.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
            }
            string fileToWriteS = $"{path}/{fileS}";
            using (StreamWriter sw = new StreamWriter(fileToWriteS))
            {
                sw.WriteLine($"Account creation : {dt.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
            }
            Commands.AddCustomerToFile(customer);
                return customer;
        }
        public static void Delete(Client done,Bank b) // Function which delete a customer only if its total balance is 0
        {
            if (done.TotalBalance(done) > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"This customer has a total balance of {done.TotalBalance(done)} in order to delete this customer, his balance must be 0");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                b.Customers.Remove(done);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{done.FullName(done)} has been successfully removed from our customers");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
