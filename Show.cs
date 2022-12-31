using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Show
    {
        Login log = new Login();
        Oper op = new Oper();

        public void ShowLogin()
        {

            switch ("1")
            {
                case "1":
                    log.LoginFirst();//
                    break;
                default:
                    break;
            }
        }

        public void ShowMain()
        {

            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("│    WELCOME TO SIMPLE BANKING SYSTEM    │");
            Console.WriteLine("│════════════════════════════════════════│");
            Console.WriteLine("│    1. Create a new account             │");
            Console.WriteLine("│    2. Search for an account            │");
            Console.WriteLine("│    3. Deposit                          │");
            Console.WriteLine("│    4. Withdraw                         │");
            Console.WriteLine("│    5. A/C statement                    │");
            Console.WriteLine("│    6. Delete account                   │");
            Console.WriteLine("│    7. Exit                             │");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("│    Enter your choice (1-7):            │");
            Console.WriteLine("╚════════════════════════════════════════╝");
            string oper = Console.ReadLine();
            switch (oper)
            {
                case "1":
                    Console.Clear();
                    op.createAccount();
                    break;
                case "2":
                    Console.Clear();
                    op.searchAccount();//
                    break;
                case "3":
                    Console.Clear();
                    op.Deposit();//
                    break;
                case "4":
                    Console.Clear();
                    op.WithMoney();//
                    break;
                case "5":
                    Console.Clear();
                    op.acStatement();//
                    break;
                case "6":
                    Console.Clear();
                    op.deleteAccount();//
                    break;
                case "7":
                    op.exit();//
                    break;
                default:
                    Console.WriteLine("Invalid input……");
                    ShowMain();
                    break;
            }
        }
    }
}
