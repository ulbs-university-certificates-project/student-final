namespace student_final.Emails.Services.Interfaces;

public interface IEmailSenderCommandService
{
    Task SendEmailAsync(string certificateName);
}