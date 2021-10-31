using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetBanque
{
    public class Commands // This class will be used as commands, in order to keep program.cs as clean as possible
    {
     
        public Commands()
        {

        }
        public static void Title()
        {
            Console.WriteLine("//////      ////    //////      /////  //////   ////////");
            Console.WriteLine("//    //  //    //  //   //   //       //          //   ");
            Console.WriteLine("//    //  //    //  //////      //     ////        //   ");
            Console.WriteLine("//    //  //    //  //   //       //   //          //   ");
            Console.WriteLine("//////      ////    //   //   /////    //////      //   ");
        } // Function to Display DORSET in the big screen
        public static void ReadFile(string file)
        {
            {
                string path = "C:/Users/theom/Desktop/Dorset C#";

                string fileToRead = $"{path}/{file}";

                Console.WriteLine($"Reading {fileToRead}");
                try
                {
                    using (StreamReader sr = new StreamReader(fileToRead))
                    {
                        Console.WriteLine($"Starting to Read {fileToRead}");
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            Console.WriteLine($"\t {line}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file counld not be read");
                    Console.WriteLine(e.Message);
                }

                Console.ReadKey();
            }

        } // Function used to read customers files and their accounts history
        public static void Menu(Bank bank) // Function which creates the menu in which we will naviguate
        {

            Console.WriteLine("Login in progress... ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Are you an Employee (1) or a Customer (2)");
            Console.ForegroundColor = ConsoleColor.White;

            int caseSwitch = Convert.ToInt32(Console.ReadLine());
            switch(caseSwitch)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("PIN required");
                    Console.ForegroundColor = ConsoleColor.White;
                    string pin = Convert.ToString(Console.ReadLine());

                    if (pin == "A1234")
                    {
                        int boucle = 2;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Welcome back");
                        Console.ForegroundColor = ConsoleColor.White;
                        while (boucle == 2) 
                        // This while is used to stay within the menu and have the ability to chain different actions without having to login each time
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Create Customer (1) Delete Customer (2) Transactions (3) ShowCustomers (4)");
                            Console.ForegroundColor = ConsoleColor.White;
                            int employeeSwitch = Convert.ToInt32(Console.ReadLine());
                            switch (employeeSwitch)
                            // Switch Case to chose easily which task we want to perform
                            {
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Customer name :");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string name2 = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Customer surname :");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string surname2 = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Customer mail :");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string mail = Console.ReadLine();
                                    Client temp = new Client(name2.ToLower(), surname2.ToLower(), mail.ToLower()); 
                                    // To.Lower() to avoid issues with double creation for the same name one with capital letters and one without
                                    bool oldclient = bank.Customers.Any(Client => Client.AccountName(Client) == Client.AccountName(temp));
                                    // Boot oldClient to check if we already have an account created for this customer
                                    if (oldclient == true)                                   
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine($"You already have an account in our bank, which is {Client.AccountName(temp)} ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    // If no account is found, we create a new customer and add it to our bank files
                                    {

                                        Client newclient = Bank.Create(name2, surname2, mail, bank);
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Would you like to effectue a deposit right away? Yes (1) No (2)");
                                        Console.ResetColor();
                                        int directtransac = Convert.ToInt32(Console.ReadLine());
                                        switch (directtransac)
                                        {
                                            case 1:
                                                Commands.Transactions(newclient);
                                                break;
                                            case 2:
                                                break;
                                        }
                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("type (1) to quit, type (2) to continue");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    boucle = Convert.ToInt32(Console.ReadLine());
                                    break;

                                case 2:
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Customer name :");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string name3 = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Customer surname");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string surname3 = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("As a security measure, we will need your email adress");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string securitymail = Console.ReadLine();
                                    Client temp2 = new Client(name3.ToLower(), surname3.ToLower(), securitymail.ToLower());
                                    bool isclient = bank.Customers.Any(Client => Client.FullName(Client) == temp2.FullName(temp2));
                                    // Again, we check if the person already has an account in our bank.
                                    if (isclient == false)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine($"You don't have an account in our bank");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("And also your secret PIN");
                                        // Security measure so people can't delete your account without your secret PIN
                                        Console.ForegroundColor = ConsoleColor.White;
                                        string securitypin = Console.ReadLine();
                                        Client deleting = bank.Customers.Find(Client => Client.AccountName(Client) == Client.AccountName(temp2));
                                        // We find the customer in our database, and delete his account
                                        Bank.Delete(deleting, bank);
                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("type (1) to quit, type (2) to continue");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    boucle = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 3:
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Customer name :");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string name4 = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Customer surname");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string surname4 = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("As a security measure, we will need your secret PIN");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string pinsecu = Console.ReadLine();
                                    Client temp3 = new Client(name4.ToLower(), surname4.ToLower(), "random");
                                    bool isclient2 = bank.Customers.Any(Client => Client.FullName(Client) == temp3.FullName(temp3));
                                    // Again, we check if the person already has an account in our bank.
                                    if (isclient2 == false || pinsecu != Client.Pin(temp3))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine($"You don't have an account in our bank or you gave us the wrong secret PIN");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Client transaction = bank.Customers.Find(Client => Client.AccountName(Client) == Client.AccountName(temp3));
                                        // We find the customer and use the function Commands.Transactions() which prints the differents transactions possible and let the user chose
                                        Commands.Transactions(transaction);

                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("type (1) to quit, type (2) to continue");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    boucle = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 4:
                                    bank.view_balances();
                                    // Bank.view_balances() is a function which prints customer name, account name and balance of current and savings account.
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("type (1) to quit, type (2) to continue");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    boucle = Convert.ToInt32(Console.ReadLine());
                                    break;
                                default:
                                    // If the input is not par of our switch, it will be redirected here.
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Wrong input, please select (1) (2) (3) oe (4)");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadKey();
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You are a not an employee.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("type in your name");
                    Console.ForegroundColor = ConsoleColor.White;
                    string name = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("type in your surname");
                    Console.ForegroundColor = ConsoleColor.White;

                    string surname = Console.ReadLine();
                    Client login = new Client(name.ToLower(), surname.ToLower(), "tempmail");
                    bool alreadyExist = bank.Customers.Any(Client => Client.FullName(Client) == login.FullName(login));
                    // There is an exception here, if two different person have the same name / surname i won't be able to detect it.
                    if (alreadyExist == false)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You are not registered, please contact an employee to open an account");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("type in your account number");
                        Console.ForegroundColor = ConsoleColor.White;
                        string account = Console.ReadLine();
                        int maxtry = 2;
                        // We allow 3 errore, in the account name and private PIN, if the users misses the 3rd time the console will automatically be closed.
                        while (account != Client.AccountName(login) && maxtry > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"This account doesnt exist you have {maxtry} attempts left");
                            Console.ForegroundColor = ConsoleColor.White;
                            account = Console.ReadLine();
                            maxtry--;
                        }
                        maxtry = 2;
                        Console.WriteLine("type in your private PIN");
                        string privatepin = Console.ReadLine();
                        while (privatepin != Client.Pin(login))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Wrong PIN, you have {maxtry} attempts left");
                            Console.ForegroundColor = ConsoleColor.White;
                            privatepin = Console.ReadLine();
                            maxtry--;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Welcome back {name}");
                        Console.ForegroundColor = ConsoleColor.White;
                        int boucle = 2;
                        Client client = bank.Customers.Find(Client => Client.AccountName(Client) == Client.AccountName(login));
                        while (boucle == 2)
                        // while so the customer can do multiple actions whitout having to relogin
                        {
                            Console.WriteLine("Transactions (1) Show Current Account history (2) Show Current Account history (3)");
                            int clientchoice = Convert.ToInt32(Console.ReadLine());
                            //switchcase in order to let the customer chose what he wants
                            switch(clientchoice)
                            {
                                case 1:
                                    Commands.Transactions(client);
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("type (1) to quit, type (2) to continue");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    boucle = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 2:
                                    ReadFile($"{Client.AccountName(client)}-current");
                                    // Reading the file in which we write every transactions in the current account
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("type (1) to quit, type (2) to continue");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    boucle = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 3:
                                    ReadFile($"{Client.AccountName(client)}-savings");
                                    // Reading the file in which we write every transactions in the savings account
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("type (1) to quit, type (2) to continue");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    boucle = Convert.ToInt32(Console.ReadLine());
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    // If the input is not par of our switch, it will be redirected here.
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Wrong input, please select (1) or (2)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    break;
            }
        }
        public static void CreateCustomersFile() // Function which creates the file customers.txt
        {
            string file = "customers.txt";
            string path = "C:/Users/theom/Desktop/Dorset C#";
            string fileToWrite = $"{path}/{file}";
            using (StreamWriter sw = new StreamWriter(fileToWrite))
            {
            }
        }
        public static void AddCustomerToFile(Client a)
        {
            string file = "customers.txt";
            string path = "C:/Users/theom/Desktop/Dorset C#";

            string fileToWrite = $"{path}/{file}";

            using (StreamWriter sw = File.AppendText(fileToWrite))
            {
                sw.WriteLine($"{a.FullName(a)} \t {Client.AccountName(a)}");
            }
        } // Function which add the customer to the file customers.txt 
        public static void Transactions(Client a)
        {
            Console.WriteLine("Deposit Current (1) Deposit Savings (2) Withdrawal Current (3) Withdrawal Savings (4) Transfer To Saving (5) Transfer To Current (6)");
            int whichtransaction = Convert.ToInt32(Console.ReadLine());
            switch (whichtransaction)
            {
                case 1:
                    Console.WriteLine("How much do you want to deposit in your Current account");
                    int depositc = Convert.ToInt32(Console.ReadLine());
                    Client.DepositCurrent(a, depositc);
                    break;
                case 2:
                    Console.WriteLine("How much do you want to deposit in your Savings account");
                    int deposits = Convert.ToInt32(Console.ReadLine());
                    Client.DepositSavings(a, deposits);
                    break;
                case 3:
                    Console.WriteLine("How much do you want to withdraw from your Current account");
                    int withdrawc = Convert.ToInt32(Console.ReadLine());
                    Client.WithdrawalCurrent(a, withdrawc);
                    break;
                case 4:
                    Console.WriteLine("How much do you want to withdraw from your Current account");
                    int withdraws = Convert.ToInt32(Console.ReadLine());
                    Client.WithdrawalCurrent(a, withdraws);
                    break;
                case 5:
                    Console.WriteLine("How much do you want to transfer from your Current account to your Savings account");
                    int transferc = Convert.ToInt32(Console.ReadLine());
                    Client.TransferToSavings(a, transferc);
                    break;
                case 6:
                    Console.WriteLine("How much do you want to transfer from your Savings account to your Current account");
                    int transfers = Convert.ToInt32(Console.ReadLine());
                    Client.TransferToCurrent(a, transfers);
                    break;
            }
        } // Function which display the differents transactiosn available


    }
}
