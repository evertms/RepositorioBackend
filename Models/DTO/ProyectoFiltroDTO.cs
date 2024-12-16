namespace ProyectoFinal.Models.DTO;

public class ProyectoFiltroDTO
{
    public string Estado { get; set; } // "Activo" o "Finalizado"
    //public string EstatusAprobacion { get; set; } // "Pendiente", "Aprobado", "Rechazado"
    public List<int> IDAreas { get; set; } // IDs de áreas de conocimiento
    public List<int> IDConceptos { get; set; } // IDs de conceptos
    //public DateTime? FechaInicio { get; set; } // Fecha mínima
    //public DateTime? FechaFin { get; set; } // Fecha máxima
    public string NombreAutor { get; set; }
}