using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
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

    [HttpGet("GetAllProyectos")]
    public IEnumerable<Proyecto> ObtenerTodosLosAprobados()
    {
        var proyectos = _proyectoService.ObtenerTodosAprobados();
        return proyectos;
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var proyecto = _proyectoService.ObtenerPorId(id);
        if (proyecto == null) return NotFound("Proyecto no encontrado");

        return Ok(proyecto);
    }

    [HttpPost("subir")]
    public IActionResult CrearProyectoConArchivo([FromForm] ProyectoCrearDTO proyectoDTO, IFormFile file)
    {
        var proyecto = _proyectoService.CrearProyectoConArchivo(proyectoDTO, file);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = proyecto.Idproyecto }, proyectoDTO);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarProyecto(int id, [FromBody] ProyectoActualizarDTO proyectoDTO)
    {
        if (id != proyectoDTO.Idproyecto) return BadRequest("ID no coincide");

        _proyectoService.ActualizarProyecto(proyectoDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarProyecto(int id)
    {
        _proyectoService.EliminarProyecto(id);
        return NoContent();
    }
    
    [HttpGet("buscar")]
    public IActionResult BuscarProyectos([FromQuery] string termino)
    {
        var proyectos = _proyectoService.BuscarProyectos(termino);
        if (!proyectos.Any())
        {
            return NotFound("No se encontraron proyectos que coincidan con el término de búsqueda.");
        }
        return Ok(proyectos);
    }
}