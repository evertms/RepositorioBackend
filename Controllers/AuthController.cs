using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UsuarioLoginDTO loginDTO)
    {
        try
        {
            // Autenticar usuario
            var usuarioAutenticado = _authService.AutenticarUsuario(loginDTO.Correo, loginDTO.Contraseña);

            // Generar token
            var token = _authService.GenerarToken(usuarioAutenticado);

            // Crear respuesta con DTO
            var tokenDTO = new UsuarioTokenDTO
            {
                IdUsuario = usuarioAutenticado.Idusuario,
                Token = token,
                NombreCompleto = usuarioAutenticado.NombreCompleto,
                Rol = usuarioAutenticado.Rol
            };

            return Ok(tokenDTO);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { mensaje = ex.Message });
        }
    }
}