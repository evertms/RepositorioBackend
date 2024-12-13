using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services;

public interface INotificacionService
{
    void CrearNotificacion(NotificacionDTO notificacionDTO);
    void NotificarRevisorSobreProyecto(Proyecto proyecto);
    void NotificarDocenteSobreEstado(Proyecto proyecto, string estado, string comentarios);
    void EnviarRecordatoriosARevisores();
}