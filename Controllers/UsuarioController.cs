using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services;
using ProyectoFinal.Services.Contrato;

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
        if (usuarioDTO == null ||
            string.IsNullOrEmpty(usuarioDTO.NombreCompleto) ||
            string.IsNullOrEmpty(usuarioDTO.Correo) ||
            string.IsNullOrEmpty(usuarioDTO.Contrasena) ||
            string.IsNullOrEmpty(usuarioDTO.Rol))
        {
            return BadRequest("No pueden haber campos vacíos.");
        }

        try
        {
            var usuarioRegistrado = _usuarioService.RegistrarUsuario(usuarioDTO);
            return Ok(usuarioRegistrado);
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

    [HttpDelete("delete/{id}")]
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