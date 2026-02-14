namespace MindFit_Intelligence_Backend.Services
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlBody);
    }
}
