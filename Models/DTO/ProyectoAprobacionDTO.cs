namespace ProyectoFinal.Models.DTO;

public class ProyectoAprobacionDTO
{
    public int IdProyecto { get; set; }
    public string EstatusAprobacion { get; set; }
    public string ComentariosAprobacion { get; set; }
    public int IDAprobador { get; set; }
    public string RolAprobador { get; set; }
}