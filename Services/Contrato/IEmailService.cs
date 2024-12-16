namespace ProyectoFinal.Services.Contrato;

public interface IEmailService
{
    void EnviarEmail(string destinatario, string asunto, string mensaje);
}