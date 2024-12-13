using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.Context;
using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public Usuario RegistrarUsuario(Usuario usuario)
    {
        if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
        {
            throw new Exception("El correo ya está registrado.");
        }
        
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }

    public Usuario ActualizarUsuario(Usuario usuario)
    {
        var existente = _context.Usuarios.Find(usuario.Idusuario);
        if (existente == null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        existente.NombreCompleto = usuario.NombreCompleto;
        existente.Rol = usuario.Rol;
        _context.SaveChanges();
        return existente;
    }

    public void EliminarUsuario(int idUsuario)
    {
        var usuario = _context.Usuarios.Find(idUsuario);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
    }

    public Usuario? ObtenerUsuarioPorId(int idUsuario)
    {
        return _context.Usuarios.Find(idUsuario);
    }

    public IEnumerable<Usuario> ObtenerTodosLosUsuarios()
    {
        return _context.Usuarios.ToList();
    }
}