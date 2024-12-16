using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class ProyectoAprobacionController : ControllerBase
{
    private readonly IProyectoAprobacionService _proyectoAprobacionService;

    public ProyectoAprobacionController(IProyectoAprobacionService proyectoAprobacionService)
    {
        _proyectoAprobacionService = proyectoAprobacionService;
    }
    
    [HttpPut("aprobar")]
    public IActionResult AprobarProyecto([FromBody] ProyectoAprobacionDTO aprobacionDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _proyectoAprobacionService.AprobarProyecto(aprobacionDTO);
            var proyectoActualizado = _proyectoAprobacionService.ObtenerProyectoPorId(aprobacionDTO.IdProyecto);
            return Ok(proyectoActualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
    
    [HttpGet("pendientes")]
    public IActionResult ObtenerProyectosPendientes()
    {
        var proyectosPendientes = _proyectoAprobacionService.ObtenerProyectosPendientes();
        return Ok(proyectosPendientes);
    }
    
    [HttpGet("parcialmente-aprobados")]
    public IActionResult ObtenerPorAprobar()
    {
        var projsParcialmenteAprobados = _proyectoAprobacionService.ObtenerProyectosPorAprobar();
        return Ok(projsParcialmenteAprobados);
    }
}