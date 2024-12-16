namespace ProyectoFinal.Models.DTO;

public class UsuarioTokenDTO
{
    public int IdUsuario { get; set; }
    public string Token { get; set; } = null!;
    public string NombreCompleto { get; set; } = null!;
    public string Rol { get; set; } = null!;
}