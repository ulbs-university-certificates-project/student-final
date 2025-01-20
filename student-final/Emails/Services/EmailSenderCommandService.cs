using System.Net;
using System.Net.Mail;
using student_final.Emails.Services.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Emails.Services;

public class EmailSenderCommandService : IEmailSenderCommandService
{
    private SmtpClient _client;
    private string _smtpServer;
    private int _smtpPort;
    private string _senderEmail;
    private string _senderPassword;

    public EmailSenderCommandService()
    {
        _smtpServer = Environment.GetEnvironmentVariable("EMAIL_SMTP_SERVER");
        _smtpPort = int.Parse(Environment.GetEnvironmentVariable("EMAIL_SMTP_PORT"));
        _senderEmail = Environment.GetEnvironmentVariable("EMAIL_SENDER_ADDRESS");
        _senderPassword = Environment.GetEnvironmentVariable("EMAIL_SENDER_PASSWORD");

        _client = new SmtpClient(_smtpServer, _smtpPort)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
        };
    }

    public async Task SendEmailAsync(string certificateName)
    {
        MailMessage mail = new MailMessage(from: _senderEmail, to: "qflorescucristian@gmail.com");
        mail.Subject = certificateName;

        using (Attachment document = new Attachment(Constants.BASE_PATH + @"Documents\Generated\" + certificateName))
        {
            mail.Attachments.Add(document);

            await _client.SendMailAsync(mail);
        }
    }
}