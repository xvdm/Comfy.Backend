using Comfy.Application.Common.Configuration;
using Comfy.Application.Common.Exceptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Comfy.Application.Services.Email;

public sealed class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public EmailService(IOptions<EmailConfiguration> emailConfiguration)
    {
        _emailConfiguration = emailConfiguration.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.DisplayName, _emailConfiguration.From));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfiguration.Host, _emailConfiguration.Port, true);
            await client.AuthenticateAsync(_emailConfiguration.Login, _emailConfiguration.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
        catch (Exception)
        {
            throw new EmailServiceException();
        }
    }

    public async Task<string> GenerateEmailConfirmationCodeAsync()
    {
        await Task.CompletedTask;
        var guid = Guid.NewGuid().ToString();
        var confirmationCode = guid.Substring(0, guid.IndexOf('-'));
        return confirmationCode;
    }
}