using System.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Centenary.Mail.SendInBlue;

public class SendInBlueApiClient: IEmailSender
{
    private readonly ILogger<SendInBlueApiClient> _logger;
    private readonly IConfiguration _config;

    public SendInBlueApiClient(ILogger<SendInBlueApiClient> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Configuration.Default.AddApiKey("api-key", _config["Mail:SendInBlue:ApiKey"]);
        
        var apiInstance = new TransactionalEmailsApi();
        var emailTo = new SendSmtpEmailTo(email);
        var sendSmtpEmail = new SendSmtpEmail
        {
            To = new List<SendSmtpEmailTo>{new(email)},
            //sendSmtpEmail.To = new List<SendSmtpEmailTo>(new[] { new SendSmtpEmailTo { Email = email } });
            Subject = subject,
            HtmlContent = htmlMessage,
            TextContent = htmlMessage,
            Sender = new SendSmtpEmailSender("Centenary Web", "brady@bradykelly.net")
        };

        try
        {
            // Send a transactional email
            CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
            _logger.LogInformation("Successfully sent transactional email with ID {messageId} to {email}", result.MessageId, email);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception when calling TransactionalEmailsApi.SendTransacEmail: {message}", e.Message );
            throw;
        }
    }
}