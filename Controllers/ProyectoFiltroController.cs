using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class ProyectoFiltroController : Controller
{
    private readonly IProyectoFiltroService _proyectoFiltroService;

    public ProyectoFiltroController(IProyectoFiltroService proyectoFiltroService)
    {
        _proyectoFiltroService = proyectoFiltroService;
    }

    [HttpPost("filtrar")]
    public IActionResult FiltrarProyectos([FromBody] ProyectoFiltroDTO filtroDTO)
    {
        var proyectos = _proyectoFiltroService.FiltrarProyectos(filtroDTO);
        return Ok(proyectos);
    }
}