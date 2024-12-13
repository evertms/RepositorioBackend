namespace ProyectoFinal.Models.DTO;

public class ProyectoCrearDTO
{
    public string Titulo { get; set; }
    public string Resumen { get; set; }
    public string? EnlaceRepositorio { get; set; }
    public int idTipoTrabajo { get; set; }
    public string Estado { get; set; }
}