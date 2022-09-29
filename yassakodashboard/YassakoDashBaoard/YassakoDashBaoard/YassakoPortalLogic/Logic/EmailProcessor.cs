using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YassakoPortalLogic.Models;

namespace YassakoPortalLogic.Logic
{
    internal class EmailProcessor
    {
        private const string smtpServer = "mail.yassako.com";
        private const string smtpUsername = "reports@yassako.com";
        private const string smtpPassword = "YassaKo@Ug4nda";

        internal GenericResponse SendEmail(string Name, string emailAddress, string Subject, string Message)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                string output = "";
                MailMessage message = new MailMessage();
                message.To.Clear();
                SmtpClient mailClient = new SmtpClient(smtpServer);
                MailAddress Email = new MailAddress(emailAddress);
                message.To.Add(emailAddress);
                message.CC.Add(new MailAddress(emailAddress));
                message.Subject = Subject.Replace('\r', ' ').Replace('\n', ' ');
                message.Body = Message;
                message.IsBodyHtml = true;
                message.From = new MailAddress(smtpUsername, "YASSAKO REPORTS");
                //I USE GMAIL AS THE SMTP SERVER..for more info google
                mailClient.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(smtpUsername, smtpPassword);
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.Timeout = 1000 * 60 * 4;
                mailClient.Credentials = cred;
                mailClient.Port = 587;
                mailClient.EnableSsl = true;
                //SEND EMAIL
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
                if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(emailAddress))
                    mailClient.Send(message);

                output = "Email successfully delivered";
                response.IsSuccessfull = true;
                response.Message= "Email successfully delivered";
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
