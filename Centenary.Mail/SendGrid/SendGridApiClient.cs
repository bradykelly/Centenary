using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Centenary.Mail.SendGrid;

public class SendGridApiClient: IEmailSender
{
    private readonly ILogger<SendGridApiClient> _logger;
    private readonly IConfiguration _config;

    public SendGridApiClient(ILogger<SendGridApiClient> logger, IConfiguration configuration)
    {
        _logger = logger;
        _config = configuration;
    }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var apiKey = _config["SendGrid:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException(("SendGrid API Key is not set."));
        }
        
        await Execute(apiKey, subject, htmlMessage, email);
    }

    private async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("brady@bradykelly.net", "Centenary"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));
        
        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failure to send email to {toEmail}: {response.StatusCode}");
        }
        else
        {
            _logger.LogInformation($"Successfully queued email to {toEmail}.");
        }
    }
}