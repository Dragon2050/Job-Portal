using JobBoard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace JobBoard.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendPasswordResetEmailAsync(string toEmail, string toName, string resetToken)
        {
            var emailSettings  = _configuration.GetSection("EmailSettings");
            var message = new MimeMessage();

            //1. Setup the Sender
            message.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            //2. Setup the Receiver
            message.To.Add(new MailboxAddress(toName, toEmail));

            //3. Setup the Subject
            message.Subject = "Reset Password";

            //4. Build the email body
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
            <h1>Hello {toName}</h1>
            <p>You requested a password reset. Use the token below in your API request:</p>
            <div style='background:#f4f4f4; padding:10px; font-family:monospace;'>
                {resetToken}
            </div>
            <p>This token expires in 1 hour.</p>"; 
            message.Body = bodyBuilder.ToMessageBody();
            // 3. Connect and Send via SMTP
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(emailSettings["SmtpServer"], 
                    int.Parse(emailSettings["Port"]),
                    SecureSocketOptions.StartTls
                );
                await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch(Exception ex)
            {
                // Log the error here
                throw new Exception("Failed to send reset email: " + ex.Message);
            }
        }
    }
}
