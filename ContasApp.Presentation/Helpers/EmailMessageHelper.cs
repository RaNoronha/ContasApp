using System.Net;
using System.Net.Mail;

namespace ContasApp.Presentation.Helpers
{
    public class EmailMessageHelper
    {
        public static void EnvioEmail(string mailTo, string subject, string body)
        {
            var remetente = "ranoronha@msn.com";
            var senha = "Lz5365@pq";
            var smtp = "smtp-mail.outlook.com";
            var porta = 587;

            var mailMessage = new MailMessage(remetente, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(smtp, porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(remetente, senha);
            smtpClient.Send(mailMessage);

        }
    }
}
