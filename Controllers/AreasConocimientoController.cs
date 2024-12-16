using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Services;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class AreasConocimientoController : ControllerBase
{
    private readonly IAreasConocimientoService _areasConocimientoService;

    public AreasConocimientoController(IAreasConocimientoService areasConocimientoService)
    {
        _areasConocimientoService = areasConocimientoService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AreasConocimiento>> ObtenerTodas()
    {
        var areas = _areasConocimientoService.ObtenerTodas();
        return Ok(areas);
    }
}