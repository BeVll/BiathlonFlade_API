namespace MB_API.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendMailAsync(string Title, string Body, string Destination);
    }
}
