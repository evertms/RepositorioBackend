using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public interface IProyectoService
{
    IEnumerable<Proyecto> ObtenerTodos();
    Proyecto ObtenerPorId(int id);
    void CrearProyecto(Proyecto proyecto);
    void ActualizarProyecto(Proyecto proyecto);
    void EliminarProyecto(int id);
}