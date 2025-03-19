namespace Aplication.Contracts
{
    public interface IMailRepository
    {
        Task SendMailTestimonialsAsync(string toEmail, string subject, string token);
        Task SendMailInterviewsAsync(string toEmail, string subject, string? token);
    }
}

