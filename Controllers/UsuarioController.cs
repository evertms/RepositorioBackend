using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("registro")]
    public IActionResult Registrar([FromBody] UsuarioRegistroDTO usuarioDTO)
    {
        try
        {
            // Convertir el DTO al modelo interno
            var nuevoUsuario = new Usuario
            {
                NombreCompleto = usuarioDTO.NombreCompleto,
                Correo = usuarioDTO.Correo,
                Contraseña = usuarioDTO.Contraseña,
                Rol = usuarioDTO.Rol
            };

            var usuarioRegistrado = _usuarioService.RegistrarUsuario(nuevoUsuario);
            return Ok(new
            {
                usuarioRegistrado.Idusuario,
                usuarioRegistrado.NombreCompleto,
                usuarioRegistrado.Correo,
                usuarioRegistrado.Rol
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.Idusuario)
            return BadRequest("El ID del usuario no coincide.");

        try
        {
            var actualizado = _usuarioService.ActualizarUsuario(usuario);
            return Ok(actualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        try
        {
            _usuarioService.EliminarUsuario(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var usuario = _usuarioService.ObtenerUsuarioPorId(id);
        if (usuario == null)
            return NotFound("Usuario no encontrado.");

        return Ok(usuario);
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var usuarios = _usuarioService.ObtenerTodosLosUsuarios();
        return Ok(usuarios);
    }
}