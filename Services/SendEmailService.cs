using SoccerNetCore.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SoccerNetCore.Services
{
    public class SendEmailService : ISendEmailService
    {
        public void SendEmail(Player player, string teamName)
        {
            MailMessage correo = CreateCorreo(player, teamName);
            SmtpClient smtp = CreateSmtp();
            smtp.Send(correo);
        }

        private MailMessage CreateCorreo(Player player, string teamName)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("@team.com", teamName, System.Text.Encoding.UTF8); //Detail Team
            correo.To.Add("destinationEmail"); //Destination email
            correo.Subject = "WELCOME TO " + teamName;
            correo.Body = BodyEmail(player, teamName); //Body email
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            return correo;
        }

        private SmtpClient CreateSmtp()
        {
            SmtpClient smtp = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = "smtp.live.com", //Host service of email. hotmail ("warning with the spam)
                Port = 25,
                Credentials = new NetworkCredential("email@hotmail.com", "password"), //Sender email
                EnableSsl = true
            };

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            return smtp;
        }

        private static string BodyEmail(Player player, string teamName)
        {
            return "<html><body><h2>Welcome to " + teamName + ". </h2><h3>DATA SHEET</h3><p><b>Name and Last Name:</b> " + player.Name + " " + player.LastName + "</p><p><b>Country:</b> " + player.Country + "</p><p><b>Date of birth:</b> " + player.Birthday + "</p><p><b>Email:</b> " + player.Email + "</p><p><b>CONTRACT:</b></p><p><b>Start Date:</b> " + player.StartDate + "</p><p><b>End Date:</b> " + player.EndDate + "</p><img src='" + player.LogoNameUniq + "' alt='" + player.Name + "' height='75px;' width='75px'></body></html>";
        }
    }
}