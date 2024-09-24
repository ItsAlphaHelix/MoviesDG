namespace MoviesDG.Core.Messaging
{
    using MailKit.Net.Smtp;
    using MimeKit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static MovieDG.Core.ErrorMessages.ErrorMessageConstants;
    public class EmailSender : IEmailSender
    {
        private readonly string smtpServer;
        private readonly int port;
        private readonly string username;
        private readonly string password;

        public EmailSender(string smtpServer, int port, string username, string password)
        {
            this.smtpServer = smtpServer;
            this.port = port;
            this.username = username;
            this.password = password;
        }

        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(htmlContent))
            {
                throw new ArgumentException(EmailSubjectErrorMessage);
            }

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromName, from));
            email.To.Add(new MailboxAddress(null, to));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlContent };
            if (attachments?.Any() == true)
            {
                foreach (var attachment in attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.FileName, attachment.Content, ContentType.Parse(attachment.MimeType));
                }
            }
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(smtpServer, port, false);
                await smtp.AuthenticateAsync(username, password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                throw;
            }
        }
    }
}