using API.Helpers.EmailHelper;

namespace API.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(Message message);
    }
}
