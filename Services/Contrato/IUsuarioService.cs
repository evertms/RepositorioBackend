using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public interface IUsuarioService
{
    Usuario RegistrarUsuario(Usuario usuario);
    Usuario ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int idUsuario);
    Usuario ObtenerUsuarioPorId(int idUsuario);
    IEnumerable<Usuario> ObtenerTodosLosUsuarios();
}