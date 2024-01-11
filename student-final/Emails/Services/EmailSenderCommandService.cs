using System.Net;
using System.Net.Mail;
using student_final.Emails.Services.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Emails.Services;

public class EmailSenderCommandService : IEmailSenderCommandService
{
    private SmtpClient _client;

    public EmailSenderCommandService()
    {
        _client = new SmtpClient(Constants.EMAIL_SMTP_SERVER, Constants.EMAIL_SMTP_PORT)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(Constants.EMAIL_SENDER_ADDRESS, Constants.EMAIL_SENDER_PASSWORD)
        };
    }

    public async Task SendEmailAsync(string certificateName)
    {
        MailMessage mail = new MailMessage(from: Constants.EMAIL_SENDER_ADDRESS, to: "qflorescucristian@gmail.com");
        mail.Subject = certificateName;

        using (Attachment document = new Attachment(Constants.DOCUMENT_OUTPUT_PATH + certificateName))
        {
            mail.Attachments.Add(document);
            
            await _client.SendMailAsync(mail);
        }
    }
}