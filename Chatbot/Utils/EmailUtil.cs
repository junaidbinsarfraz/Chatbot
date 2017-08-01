using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Chatbot.Utils
{
    public class EmailUtil
    {
        public static bool SendEMail(string emailid, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;

                string MailAccount = System.Configuration.ConfigurationManager.AppSettings["MailAccount"];
                string MailPassword = System.Configuration.ConfigurationManager.AppSettings["MailPassword"];

                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(MailAccount, MailPassword);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(MailAccount);
                msg.To.Add(new MailAddress(emailid));

                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = body;

                client.Send(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Email sending exception : \n" + e);
                return false;
            }

            return true;
        }
    }
}