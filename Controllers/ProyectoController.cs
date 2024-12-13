using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class ProyectoController : Controller
{
    private readonly IProyectoService _proyectoService;

    public ProyectoController(IProyectoService proyectoService)
    {
        _proyectoService = proyectoService;
    }

    [HttpGet(Name = "GetAllProyectos")]
    public IEnumerable<Proyecto> ObtenerTodos()
    {
        var proyectos = _proyectoService.ObtenerTodos();
        return proyectos;
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var proyecto = _proyectoService.ObtenerPorId(id);
        if (proyecto == null) return NotFound("Proyecto no encontrado");

        return Ok(proyecto);
    }

    [HttpPost]
    public IActionResult CrearProyecto([FromBody] Proyecto proyecto)
    {
        _proyectoService.CrearProyecto(proyecto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = proyecto.Idproyecto }, proyecto);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarProyecto(int id, [FromBody] Proyecto proyecto)
    {
        if (id != proyecto.Idproyecto) return BadRequest("ID no coincide");

        _proyectoService.ActualizarProyecto(proyecto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarProyecto(int id)
    {
        _proyectoService.EliminarProyecto(id);
        return NoContent();
    }
}