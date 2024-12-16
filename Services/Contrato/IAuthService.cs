using ProyectoFinal.Models;

namespace ProyectoFinal.Services.Contrato;

public interface IAuthService
{
    Usuario AutenticarUsuario(string correo, string contraseña);
    string GenerarToken(Usuario usuario);
}
