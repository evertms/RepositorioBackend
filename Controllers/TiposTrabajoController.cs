using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Services;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class TiposTrabajoController : ControllerBase
{
    private readonly ITiposTrabajoService _tiposTrabajoService;

    public TiposTrabajoController(ITiposTrabajoService tiposTrabajoService)
    {
        _tiposTrabajoService = tiposTrabajoService;
    }
    
    [HttpGet]
    public IActionResult GetTiposTrabajo()
    {
        var tiposTrabajo = _tiposTrabajoService.GetTiposTrabajo();
        return Ok(tiposTrabajo);
    }
}