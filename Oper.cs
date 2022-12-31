using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Security.Policy;
using System.Net.Mail;
using System.Net;
using System.Windows.Interop;

namespace BankSystem
{
    class Oper
    {
        public string money { get; set; }
        public string password { get; set; }

        public void createAccount()
        {
            Show sh = new Show();
            string dir = Directory.GetCurrentDirectory();//
            string firstName = "";
            string lastName = "";//
            string address = "";//
            string phone = "";//
            string email = "";//
            while (true)
            {
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("│    CREATE A NEW ACCOUNT                │");
                Console.WriteLine("│════════════════════════════════════════│");
                Console.WriteLine("│              ENTER THE DETAILS         │");
                Console.WriteLine("│                                        │");
                Console.WriteLine("│    First Name:                         │");
                Console.WriteLine("│    Last Name:                          │");
                Console.WriteLine("│    Address:                            │");
                Console.WriteLine("│    Phone:                              │");
                Console.WriteLine("│    Email:                              │");
                Console.WriteLine("╚════════════════════════════════════════╝");

                Console.SetCursorPosition(16, 5);
                firstName = Console.ReadLine();

                Console.SetCursorPosition(15, 6);
                lastName = Console.ReadLine();

                Console.SetCursorPosition(13, 7);
                address = Console.ReadLine();

                Console.SetCursorPosition(11, 8);
                phone = Console.ReadLine();

                Console.SetCursorPosition(11, 9);
                email = Console.ReadLine();

                const string pattern = "^[0-9]*$";
                Regex rx = new Regex(pattern);
                while (phone.Length > 10 || !rx.IsMatch(phone) || phone == "")
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("phone format is incorrect");
                    Console.SetCursorPosition(11, 8);
                    phone = Console.ReadLine();
                }

                Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
                while (!r.IsMatch(email))
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("E-mail format is incorrect");
                    Console.SetCursorPosition(11, 9);
                    email = Console.ReadLine();
                }
                Console.SetCursorPosition(0, 13);
                Console.WriteLine("Is the information correct (y/n)?");
                String flag = Console.ReadLine();
                if (flag == "y")
                {
                    Random rd = new Random();
                    int account = rd.Next(100000, 1000000);
                    string toEmail = email;
                    string titleEmail = "create account successful";
                    string emailBody = "Congratulations, the account was created successfully! Please remember your account number :"+account+";Frist name:"+ firstName+";Last name:"+lastName+";Address:"+address+";phone:"+phone;
                    SendEmail(toEmail, titleEmail, emailBody);
                    File.AppendAllText(dir + "\\" + account + ".txt", account + "\r\n");
                    File.AppendAllText(dir + "\\" + account + ".txt", 0 + "\r\n");
                    File.AppendAllText(dir + "\\" + account + ".txt", firstName + "\r\n");
                    File.AppendAllText(dir + "\\" + account + ".txt", lastName + " \r\n");//
                    File.AppendAllText(dir + "\\" + account + ".txt", address + "\r\n");//
                    File.AppendAllText(dir + "\\" + account + ".txt", phone + "\r\n");//
                    File.AppendAllText(dir + "\\" + account + ".txt", email);
                    File.AppendAllText(dir + "\\" + account + "statement.txt", 0 + "\r\n");
                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine("Account Created! details will be provided via email.");
                    Console.WriteLine("Account number is :" + account);
                    ConsoleKeyInfo info1 = Console.ReadKey(true);
                    Console.Clear();
                    sh.ShowMain();
                    return;
                }
                else
                {
                    Console.Clear();
                    createAccount();
                }
            }
        }

        public void searchAccount()
        {
            Show sh = new Show();
            string dir = Directory.GetCurrentDirectory();//
            string accountSearch = "";
            while (true)
            {
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("│    SEARCH A NEW ACCOUNT                │");
                Console.WriteLine("│════════════════════════════════════════│");
                Console.WriteLine("│              ENTER THE DETAILS         │");
                Console.WriteLine("│                                        │");
                Console.WriteLine("│    Account Number:                     │");
                Console.WriteLine("╚════════════════════════════════════════╝");

                Console.SetCursorPosition(20, 5);
                accountSearch = Console.ReadLine();
                string path = dir + @"\" + accountSearch + ".txt";
                if (!File.Exists(path))
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account not found!");
                }
                else
                {
                    string[] account = File.ReadAllLines(dir + @"\" + accountSearch + ".txt");//
                    string temp_account = account[0];
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account found!");
                    Console.WriteLine("╔════════════════════════════════════════╗");
                    Console.WriteLine("│    ACCOUNT DETAILS                     │");
                    Console.WriteLine("│════════════════════════════════════════│");
                    Console.WriteLine("│                                        │");
                    Console.WriteLine("│    Account No:" + account[0] + "                   │");
                    Console.WriteLine("│    Account Balance:" + account[1] + "                   │");
                    Console.WriteLine("│    First Name:" + account[2] + "                     │");
                    Console.WriteLine("│    Last Name:" + account[3] + "                      │");
                    Console.WriteLine("│    Address:" + account[4] + "                        │");
                    Console.WriteLine("│    Phone:" + account[5] + "                      │");
                    Console.WriteLine("│    Email:" + account[6] + "                   │");
                    Console.WriteLine("╚════════════════════════════════════════╝");
                }

                Console.WriteLine("Check another account (y/n)?");
                string flag = Console.ReadLine();
                if (flag == "y")
                {
                    Console.Clear();
                    searchAccount();
                    flag = Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    sh.ShowMain();
                    return;
                }
            }
        }





        /// <summary>
        /// 
        /// </summary>
        public void Deposit()
        {
            Login log = new Login();
            Show sh = new Show();
            try
            {
                string dir = Directory.GetCurrentDirectory();//

                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("│                DEPOSIT                 │");
                Console.WriteLine("│════════════════════════════════════════│");
                Console.WriteLine("│              ENTER THE DETAILS         │");
                Console.WriteLine("│                                        │");
                Console.WriteLine("│    Account Number:                     │");
                Console.WriteLine("│    Amount:$                            │");
                Console.WriteLine("╚════════════════════════════════════════╝");

                Console.SetCursorPosition(20, 5);
                string account = Console.ReadLine();


                const string pattern = "^[0-9]*$";
                Regex rx = new Regex(pattern);
                while (account.Length > 10 || !rx.IsMatch(account) || account == "")
                {
                    Console.SetCursorPosition(0, 9);
                    Console.WriteLine("Account must be a number and cannot be empty");
                    Console.SetCursorPosition(20, 5);
                    account = Console.ReadLine();
                }
                string path = dir + @"\" + account + ".txt";
                if (!File.Exists(path))
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account not found!");
                    Console.WriteLine("                                                                                                  ");
                    Console.WriteLine("Retry (y/n)");
                    string ynDep = Console.ReadLine();
                    if (ynDep.ToLower() == "y")
                    {
                        Console.Clear();
                        Deposit();
                    }
                    else
                    {
                        Console.Clear();
                        sh.ShowMain();
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account found! Enter the amount...");
                    Console.SetCursorPosition(13, 6);
                    string amount = Console.ReadLine();
                    int tempMon = 0;
                    string[] account_info = File.ReadAllLines(path);//
                    string tempMoney = account_info[1];//
                    tempMon = int.Parse(tempMoney) + int.Parse(amount);
                    account_info[1] = account_info[1].Replace(account_info[1], tempMon.ToString());
                    File.WriteAllLines(path, account_info);
                    File.AppendAllText(dir + "\\" + account + "statement.txt", amount + "\r\n");
                    Console.SetCursorPosition(0, 9);
                    Console.WriteLine("Deposit successfull!");
                    ConsoleKeyInfo info1 = Console.ReadKey(true);
                    Console.Clear();
                    sh.ShowMain();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void WithMoney()
        {
            Login log = new Login();
            Show sh = new Show();
            try
            {
                string dir = Directory.GetCurrentDirectory();//

                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("│                WITHDRAW                 │");
                Console.WriteLine("│════════════════════════════════════════│");
                Console.WriteLine("│              ENTER THE DETAILS         │");
                Console.WriteLine("│                                        │");
                Console.WriteLine("│    Account Number:                     │");
                Console.WriteLine("│    Amount:$                            │");
                Console.WriteLine("╚════════════════════════════════════════╝");

                Console.SetCursorPosition(20, 5);
                string account = Console.ReadLine();


                const string pattern = "^[0-9]*$";
                Regex rx = new Regex(pattern);
                while (account.Length > 10 || !rx.IsMatch(account) || account == "")
                {
                    Console.SetCursorPosition(0, 9);
                    Console.WriteLine("Account must be a number and cannot be empty");
                    Console.SetCursorPosition(20, 5);
                    account = Console.ReadLine();
                }
                string path = dir + @"\" + account + ".txt";
                if (!File.Exists(path))
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account not found!");
                    Console.WriteLine("                                                                                                  ");
                    Console.WriteLine("Retry (y/n)");
                    string ynDep = Console.ReadLine();
                    if (ynDep.ToLower() == "y")
                    {
                        Console.Clear();
                        WithMoney();
                    }
                    else
                    {
                        Console.Clear();
                        sh.ShowMain();
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account found! Enter the amount...");
                    Console.SetCursorPosition(13, 6);
                    string amount = Console.ReadLine();
                    int tempMon = 0;
                    string[] account_info = File.ReadAllLines(path);//
                    string tempMoney = account_info[1];//
                    while (int.Parse(tempMoney) < int.Parse(amount))
                    {
                        Console.SetCursorPosition(0, 9);
                        Console.WriteLine("Balance is not enough");
                        Console.SetCursorPosition(13, 6);
                        amount = Console.ReadLine();
                    }
                    tempMon = int.Parse(tempMoney) - int.Parse(amount);
                    account_info[1] = account_info[1].Replace(account_info[1], tempMon.ToString());
                    File.WriteAllLines(path, account_info);
                    File.AppendAllText(dir + "\\" + account + "statement.txt", "-"+amount + "\r\n");
                    Console.SetCursorPosition(0, 9);
                    Console.WriteLine("Withdraw successfull!       ");
                    ConsoleKeyInfo info1 = Console.ReadKey(true);
                    Console.Clear();
                    sh.ShowMain();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void acStatement()
        {
            Show sh = new Show();
            string dir = Directory.GetCurrentDirectory();//
            string accountSearch = "";
            string flag = "";
            while (true)
            {
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("│                STATEMENT               │");
                Console.WriteLine("│════════════════════════════════════════│");
                Console.WriteLine("│              ENTER THE DETAILS         │");
                Console.WriteLine("│                                        │");
                Console.WriteLine("│    Account Number:                     │");
                Console.WriteLine("╚════════════════════════════════════════╝");

                Console.SetCursorPosition(20, 5);
                accountSearch = Console.ReadLine();
                string path = dir + @"\" + accountSearch + ".txt";
                if (!File.Exists(path))
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account not found!");
                    Console.WriteLine("Check another account (y/n)?");
                    flag = Console.ReadLine();
                    if (flag == "y")
                    {
                        Console.Clear();
                        acStatement();
                        flag = Console.ReadLine();
                    }
                    else
                    {
                        Console.Clear();
                        sh.ShowMain();
                        return;
                    }
                }
                else
                {
                    string[] account = File.ReadAllLines(dir + @"\" + accountSearch + ".txt");//
                    string[] accountStatement = File.ReadAllLines(dir + @"\" + accountSearch + "statement.txt");//
                    int accountStatementCount = accountStatement.Length;
                    string temp_account = account[0];
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account found! The statement is displayed below...");
                    Console.WriteLine("╔════════════════════════════════════════╗");
                    Console.WriteLine("│          SIMPLE BANKING SYSTEM         │");
                    Console.WriteLine("│════════════════════════════════════════│");
                    Console.WriteLine("│       Account Statement                │");
                    Console.WriteLine("│                                        │");
                    Console.WriteLine("│    Account No:" + account[0] + "                   │");
                    Console.WriteLine("│    Account Balance:" + account[1] + "                   │");
                    Console.WriteLine("│    First Name:" + account[2] + "                     │");
                    Console.WriteLine("│    Last Name:" + account[3] + "                      │");
                    Console.WriteLine("│    Address:" + account[4] + "                        │");
                    Console.WriteLine("│    Phone:" + account[5] + "                      │");
                    Console.WriteLine("│    Email:" + account[6] + "                   │");
                    Console.WriteLine("╚════════════════════════════════════════╝");
                    Console.WriteLine("Email statement (y/n)?");
                    flag = Console.ReadLine();
                    if (flag == "y")
                    {
                        string toEmail = account[6];
                        string titleEmail = "Account Statement";

                        string emailBody = "Your last five transactions:";
                        if(accountStatementCount <= 5)
                        {
                            for (int i = 0; i < accountStatement.Length; i++)
                            {
                                string msg = "";
                                if (int.Parse(accountStatement[i]) >= 0)
                                {
                                    msg = "Deposit:";
                                }
                                else
                                {
                                    msg = "Withdrawal:";
                                }
                                emailBody += msg+accountStatement[i].ToString();
                            }
                        }
                        else
                        {
                            int tmpNum = accountStatementCount;
                            for (int j = 0; j < 5; j++)
                            {
                                string msg = "";
                                if (int.Parse(accountStatement[tmpNum - 1]) >= 0)
                                {
                                    msg = "Deposit:";
                                }
                                else
                                {
                                    msg = "Withdrawal:";
                                }
                                emailBody += msg + accountStatement[tmpNum - 1].ToString();
                                tmpNum--;
                            }
                        }
                        
                        SendEmail(toEmail, titleEmail, emailBody);
                        Console.WriteLine("Email sent successfully!...");
                        ConsoleKeyInfo info1 = Console.ReadKey(true);
                        Console.Clear();
                        sh.ShowMain();
                    }
                    else
                    {
                        Console.Clear();
                        sh.ShowMain();
                        return;
                    }
                }
            }
        }

        public void deleteAccount()
        {
            Show sh = new Show();
            string dir = Directory.GetCurrentDirectory();//
            string accountSearch = "";
            string flag = "";
            while (true)
            {
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("│            DELETE AN ACCOUNT           │");
                Console.WriteLine("│════════════════════════════════════════│");
                Console.WriteLine("│              ENTER THE DETAILS         │");
                Console.WriteLine("│                                        │");
                Console.WriteLine("│    Account Number:                     │");
                Console.WriteLine("╚════════════════════════════════════════╝");

                Console.SetCursorPosition(20, 5);
                accountSearch = Console.ReadLine();
                string path = dir + @"\" + accountSearch + ".txt";
                if (!File.Exists(path))
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account not found!");
                    Console.WriteLine("Check another account (y/n)?");
                    flag = Console.ReadLine();
                    if (flag == "y")
                    {
                        Console.Clear();
                        deleteAccount();
                        flag = Console.ReadLine();
                    }
                    else
                    {
                        Console.Clear();
                        sh.ShowMain();
                        return;
                    }
                }
                else
                {
                    string[] account = File.ReadAllLines(path);//
                    string temp_account = account[0];
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account found! The statement is displayed below...");
                    Console.WriteLine("╔════════════════════════════════════════╗");
                    Console.WriteLine("│            ACCOUNT DETAILS             │");
                    Console.WriteLine("│════════════════════════════════════════│");
                    Console.WriteLine("│                                        │");
                    Console.WriteLine("│    Account No:" + account[0] + "                   │");
                    Console.WriteLine("│    Account Balance:" + account[1] + "                   │");
                    Console.WriteLine("│    First Name:" + account[2] + "                     │");
                    Console.WriteLine("│    Last Name:" + account[3] + "                      │");
                    Console.WriteLine("│    Address:" + account[4] + "                        │");
                    Console.WriteLine("│    Phone:" + account[5] + "                      │");
                    Console.WriteLine("│    Email:" + account[6] + "                   │");
                    Console.WriteLine("╚════════════════════════════════════════╝");
                    Console.WriteLine("Delete (y/n)?");
                    flag = Console.ReadLine();
                    if (flag == "y")
                    {
                        File.Delete(path);
                        Console.WriteLine("Account Deleted!...");
                        ConsoleKeyInfo info1 = Console.ReadKey(true);
                        Console.Clear();
                        sh.ShowMain();
                    }
                    else
                    {
                        Console.Clear();
                        deleteAccount();
                        return;
                    }
                }
            }
        }



        /// <summary>
        /// exit
        /// </summary>
        public void exit()
        {

            Console.WriteLine("Are you sure to exit the system?(y/n)");
            string ynExit = Console.ReadLine();
            if (ynExit.ToLower() == "y")
            {
                Environment.Exit(0);
            }
            else
            {
                Show sh = new Show();
                Console.Clear();
                sh.ShowMain();
            }
        }


        public static void SendEmail(String sendTo, String title, String body)
        {
            string fromEmail = "gyy86125@gmail.com";
            string fromName = "Bank System";
            MailMessage msg = new MailMessage();
            //
            msg.To.Add(sendTo); // 

            //
            msg.From = new MailAddress(fromEmail, fromName);

            msg.Subject = title;// 
            msg.SubjectEncoding = Encoding.UTF8; // 

            msg.Body = body;//
            msg.BodyEncoding = Encoding.UTF8; // 
            msg.IsBodyHtml = true;//

            SmtpClient client = new SmtpClient();

            //
            client.Host = "smtp.gmail.com"; // 
            client.Port = 587; //

            client.EnableSsl = true; //


            client.Credentials = new System.Net.NetworkCredential(fromEmail, "cndkusxfninvpigv");

            try
            {
                client.Send(msg); //
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
