using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows;

namespace BankSystem
{
    class Login
    {
        /// <summary>
        /// 
        /// </summary>
        public void LoginFirst(Boolean b = true)
        {
            Show sh = new Show();
            string dir = Directory.GetCurrentDirectory();//
            string[] register = File.ReadAllLines(dir + @"\login.txt");//
            string blockid = "";
            string pwd = "";//
            while (true)
            {
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("│    WELCOME TO SIMPLE BANKING SYSTEM    │");
                Console.WriteLine("│════════════════════════════════════════│");
                Console.WriteLine("│              LOGIN TO START            │");
                Console.WriteLine("│                                        │");
                Console.WriteLine("│                 User Name:             │");
                Console.WriteLine("│                 Password:              │");
                Console.WriteLine("╚════════════════════════════════════════╝");
                if (b)
                {
                    Console.SetCursorPosition(28, 5);
                    blockid = Console.ReadLine();
                    Console.SetCursorPosition(27, 6);
                }
                else
                {
                    Console.SetCursorPosition(28, 6);
                    blockid = Console.ReadLine();
                    Console.SetCursorPosition(27, 7);
                }

                while (true)
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    if (info.Key != ConsoleKey.Enter)
                    {
                        if (info.Key != ConsoleKey.Backspace)
                        {
                            Console.Write("*");
                            pwd += info.KeyChar;
                            //info = Console.ReadKey(true);
                        }
                        else
                        {
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < register.Length; i++)
                        {
                            if (register[i].Contains(blockid + "pwd"))
                            {
                                string tempid = register[i].Substring(0, 5);
                                string temppwd = register[i].Substring(9, 6);//
                                if (blockid == tempid)//
                                {
                                    if (pwd == temppwd.Trim())//
                                    {
                                        Console.SetCursorPosition(0, 9);
                                        Console.WriteLine("Valid credentials!..    Please enter");
                                        Console.WriteLine();//
                                        ConsoleKeyInfo info1 = Console.ReadKey(true);
                                        Console.Clear();
                                        sh.ShowMain();
                                        return;
                                    }
                                    else
                                    {
                                        pwd = "";
                                        Console.Clear();
                                        Console.WriteLine("The password is incorrect");
                                        LoginFirst(false);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                pwd = "";
                                Console.Clear();
                                Console.WriteLine("The password is incorrect");
                                LoginFirst(false);
                                break;
                            }
                        }
                    }

                }


            }
        }
    }
}
