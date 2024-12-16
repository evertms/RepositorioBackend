using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services.Contrato;

public interface IUsuarioService
{
    Usuario RegistrarUsuario(UsuarioRegistroDTO usuario);
    Usuario ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int idUsuario);
    Usuario ObtenerUsuarioPorId(int idUsuario);
    IEnumerable<Usuario> ObtenerTodosLosUsuarios();
}