using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace dotnet_webapp_ec2.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    private const string FromAddress = "Rich Argo <rich@argohaus.com>";
    private static string AwsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID")!;
    private static string AwsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY")!;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        // Setup the email recipients.
        var oDestination = new Destination(new List<string>() { toEmail });

        // Create the email subject.
        var oSubject = new Content(subject);

        // Create the email body.
        var oBody = new Body();
        var oHtml = new Content();
        oHtml.Charset = "UTF-8";
        oHtml.Data = message;
        oBody.Html = oHtml;


        // Create and transmit the email to the recipients via Amazon SES.
        var oMessage = new Message(oSubject, oBody);
        var request = new SendEmailRequest(FromAddress, oDestination, oMessage);

        try
        {
            using (var client = new AmazonSimpleEmailServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                await client.SendEmailAsync(request);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception caught in SendEmailAsync");
        }
    }
}
