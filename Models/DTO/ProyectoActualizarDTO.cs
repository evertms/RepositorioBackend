namespace ProyectoFinal.Models.DTO;

public class ProyectoActualizarDTO
{
    public int Idproyecto { get; set; }
    public string Titulo { get; set; }
    public string Resumen { get; set; }
    public string? EnlaceRepositorio { get; set; }
    public string DocumentoPdf { get; set; }
    public string Estado { get; set; }
    public string EstatusAprobacion { get; set; }
}