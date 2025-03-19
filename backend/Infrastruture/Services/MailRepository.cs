using Aplication.Contracts;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Infrastruture.Services
{
    public class MailRepository : IMailRepository
    {
        private readonly IConfiguration _configuration;

        public MailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailInterviewsAsync(string toEmail, string subject, string? token)
        {
            string body;

            if (subject.Equals("approve", StringComparison.OrdinalIgnoreCase))
            {
                body = ComposeApprovalBody(token);
            }
            else
            {
                body = ComposeRejectionBody();
            }

            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendMailTestimonialsAsync(string toEmail, string subject, string token)
        {
            string body = ComposeTestimonialsBody(token);
            await SendEmailAsync(toEmail, subject, body);
        }

        private string ComposeApprovalBody(string token)
        {
            string clientUrl = _configuration["Client"];
            string body = $@"
                <html>
                <body>
                    <div style=""width: 100%"">
                        <p>Hi Sir,</p>
                        <p>Congratulations! You have been approved for the interview.</p>
                        <p>Please visit the following link to schedule your interview:</p>
                        <div style=""text-align: center"">
                            <a href='{clientUrl}/schedule-interview?token={token}' style='display: inline-block; padding: 10px 20px; color: #fff; background-color: #0b1f3f; text-decoration: none; border-radius: 5px;'>
                                Schedule Interview
                            </a>
                        </div>
                        <hr style=""margin: 2rem; background-color: #0b1f3f; height: 1px""/>
                    </div>
                </body>
                </html>";
            return body;
        }

        private string ComposeRejectionBody()
        {
            string body = $@"
                <html>
                <body>
                    <div style=""width: 100%"">
                        <p>Hi Sir,</p>
                        <p>We regret to inform you that you were not selected for the position.</p>
                    </div>
                </body>
                </html>";
            return body;
        }

        private string ComposeTestimonialsBody(string token)
        {
            string clientUrl = _configuration["Client"];
            string body = $@"
                <html>
                <body>
                    <div style=""width: 100%"">
                        <p>Hi Sir,</p>
                        <p>Could you please provide feedback on the service you have used?</p>
                        <div style=""text-align: center"">
                            <a href='{clientUrl}/testimonials?token={token}' style='display: inline-block; padding: 10px 20px; color: #fff; background-color: #0b1f3f; text-decoration: none; border-radius: 5px;'>
                                Click
                            </a>
                        </div>
                        <hr style=""margin: 2rem; background-color: #0b1f3f; height: 1px""/>
                    </div>
                </body>
                </html>";
            return body;
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var emailHost = _configuration["EmailSettings:EmailHost"];
                var emailPort = int.Parse(_configuration["EmailSettings:EmailPort"]);
                var email = _configuration["EmailSettings:Email"];
                var password = _configuration["EmailSettings:Password"];

                using (var client = new SmtpClient(emailHost, emailPort))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(email, password);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(email),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to send email to {toEmail}.", ex);
            }
        }
    }
}
