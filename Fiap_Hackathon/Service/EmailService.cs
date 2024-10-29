using System.Net.Mail;
using System.Net;

namespace Fiap_Hackathon.Service
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarEmailAsync(string destinatario, string assunto, string mensagem)
        {
            var smtpConfig = _config.GetSection("Smtp");

            using (var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"])))
            {
                client.Credentials = new NetworkCredential(smtpConfig["User"], smtpConfig["Password"]);
                client.EnableSsl = bool.Parse(smtpConfig["EnableSSL"]);

                var mail = new MailMessage
                {
                    From = new MailAddress(smtpConfig["User"]),
                    Subject = assunto,
                    Body = mensagem,
                    IsBodyHtml = true
                };
                mail.To.Add(destinatario);

                await client.SendMailAsync(mail);
            }
        }
    }
}
