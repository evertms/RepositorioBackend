using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Services.Implementacion;

public class RecordatorioRevisoresService : BackgroundService
{
    private readonly INotificacionService _notificacionService;

    public RecordatorioRevisoresService(INotificacionService notificacionService)
    {
        _notificacionService = notificacionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _notificacionService.EnviarRecordatoriosARevisores();
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}