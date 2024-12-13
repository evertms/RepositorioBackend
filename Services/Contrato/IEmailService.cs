namespace ProyectoFinal.Services;

public interface IEmailService
{
    void EnviarEmail(string destinatario, string asunto, string mensaje);
}