using System.Net;
using System.Net.Mail;

namespace ProyectoFinal.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void EnviarEmail(string destinatario, string asunto, string mensaje)
    {
        var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
        {
            Port = int.Parse(_configuration["Smtp:Port"]),
            Credentials = new NetworkCredential(_configuration["Smtp:User"], _configuration["Smtp:Password"]),
            EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"])
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Smtp:User"]),
            Subject = asunto,
            Body = mensaje,
            IsBodyHtml = true
        };

        mailMessage.To.Add(destinatario);

        smtpClient.Send(mailMessage);
    }
}