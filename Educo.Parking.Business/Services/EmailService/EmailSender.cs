using System.Net;
using System.Net.Mail;
using System.Text;

namespace Educo.Parking.Business.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string email, string subject, string message)
        {
            MailAddress from = new MailAddress("alexandr.korostelev75@gmail.com", "Администратор сайта", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Body = message;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.Subject = subject;
            mailMessage.SubjectEncoding = Encoding.UTF8;

            using (SmtpClient client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("email", "password");
                client.Timeout = 20000;
                client.Send(mailMessage);
            }
                
            mailMessage.Dispose();
        }
    }
}
