using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class ProyectoAprobacionController : Controller
{
    private readonly IProyectoAprobacionService _proyectoAprobacionService;

    public ProyectoAprobacionController(IProyectoAprobacionService proyectoAprobacionService)
    {
        _proyectoAprobacionService = proyectoAprobacionService;
    }
    [Authorize(Policy = "EsRevisor")]
    [HttpPut("aprobar")]
    public IActionResult AprobarProyecto([FromBody] ProyectoAprobacionDTO aprobacionDTO)
    {
        try
        {
            _proyectoAprobacionService.AprobarProyecto(aprobacionDTO);
            return Ok(new { mensaje = "El proyecto ha sido actualizado exitosamente." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
    
    [Authorize(Policy = "EsRevisor")]
    [HttpGet("pendientes")]
    public IActionResult ObtenerProyectosPendientes()
    {
        var proyectosPendientes = _proyectoAprobacionService.ObtenerProyectosPendientes();
        return Ok(proyectosPendientes);
    }
}