using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Show shoLogin = new Show();
            shoLogin.ShowLogin();

            Console.ReadKey();
        }
    }
}
