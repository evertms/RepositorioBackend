using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services;
using ProyectoFinal.Services.Contrato;

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
    
    [HttpPost("subir-archivo")]
    public IActionResult SubirArchivo([FromForm] IFormFile file)
    {
        try
        {
            var ruta = _proyectoService.SubirArchivo(file);
            return Ok(new { ruta });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error interno.", details = ex.Message });
        }
    }

    [HttpPost("crear-proyecto")]
    public IActionResult CrearProyecto([FromBody] ProyectoCrearDTO proyectoDTO)
    {
        try
        {
            var proyecto = _proyectoService.CrearProyecto(proyectoDTO);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = proyecto.Idproyecto }, proyecto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error interno al procesar la solicitud.", details = ex.Message });
        }
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
    
    [HttpGet("mis-proyectos/{idUsuario}")]
    public IActionResult ObtenerProyectosPorUsuario(int idUsuario)
    {
        var proyectos = _proyectoService.ObtenerProyectosPorUsuario(idUsuario);
        return Ok(proyectos);
    }

    [HttpGet("recientes")]
    public IActionResult ObtenerProyectosRecientes()
    {
        var proyectos = _proyectoService.ObtenerProyectosRecientes();
        return Ok(proyectos);
    }
}