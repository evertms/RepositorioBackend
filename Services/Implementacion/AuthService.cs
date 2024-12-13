using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.Context;
using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly string _jwtKey;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _jwtKey = configuration["Jwt:Key"];
    }

    public Usuario AutenticarUsuario(string correo, string contraseña)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == correo && u.Contraseña == contraseña);
        if (usuario == null)
        {
            throw new Exception("Credenciales incorrectas.");
        }

        return usuario;
    }

    public string GenerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Idusuario.ToString()),
            new Claim(ClaimTypes.Role, usuario.Rol),
            new Claim(ClaimTypes.Email, usuario.Correo)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost",
            audience: "http://localhost",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}