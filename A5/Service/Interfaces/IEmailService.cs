using A5.Models;
namespace A5.Service.Interfaces{
    public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
}