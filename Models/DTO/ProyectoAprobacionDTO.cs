namespace ProyectoFinal.Models.DTO;

public class ProyectoAprobacionDTO
{
    public int IDProyecto { get; set; }
    public string EstatusAprobacion { get; set; } // Aprobado o Rechazado
    public string ComentariosAprobacion { get; set; } // Comentarios opcionales
    public int IDAdministrador { get; set; }
}