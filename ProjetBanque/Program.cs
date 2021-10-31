using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetBanque
{
    class Program
    {
        static void Main(string[] args)
        {
            Commands.CreateCustomersFile();
            Commands.Title();
            Bank Dorset = new Bank("DorsetCollege");
            Client theo = Bank.Create("theo", "midavaine", "adegoat", Dorset);
            Commands.Menu(Dorset);

        }
    }
}
