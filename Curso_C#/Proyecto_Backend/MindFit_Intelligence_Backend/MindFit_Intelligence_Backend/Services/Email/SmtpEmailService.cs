using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MindFit_Intelligence_Backend.Services.Email
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            var host = _configuration["Smtp:Host"]!;
            var port = int.Parse(_configuration["Smtp:Port"]!);
            var useSsl = bool.Parse(_configuration["Smtp:UseSsl"]!);

            var username = _configuration["Smtp:Username"]!;
            var password = _configuration["Smtp:Password"]!;

            var fromEmail = _configuration["Smtp:FromEmail"]!;
            var fromName = _configuration["Smtp:FromName"]!;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            message.Body = new BodyBuilder
            {
                HtmlBody = htmlBody
            }.ToMessageBody();

            using var client = new SmtpClient();

            // STARTTLS típico: puerto 587
            // Si UseSsl=true suele ser puerto 465
            var secureOption = useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

            await client.ConnectAsync(host, port, secureOption);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
