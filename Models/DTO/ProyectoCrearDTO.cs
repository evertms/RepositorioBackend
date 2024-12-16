namespace ProyectoFinal.Models.DTO;

public class ProyectoCrearDTO
{
    public string Titulo { get; set; }
    public string Resumen { get; set; }
    public string? EnlaceRepositorio { get; set; }
    public int IdTipoTrabajo { get; set; }
    public string Estado { get; set; }
    public int IdUsuario { get; set; }
    public List<ParticipanteDTO> Participantes { get; set; } = new List<ParticipanteDTO>();
}