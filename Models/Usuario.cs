using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("Usuario")]
[Index("Correo", Name = "UQ__Usuario__60695A191D5A1BC0", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("IDUsuario")]
    public int Idusuario { get; set; }

    [StringLength(255)]
    public string NombreCompleto { get; set; } = null!;

    [StringLength(255)]
    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    [StringLength(50)]
    public string Rol { get; set; } = null!;

    [InverseProperty("IdusuarioNavigation")]
    public virtual ICollection<Notificacion> Notificacions { get; } = new List<Notificacion>();

    [InverseProperty("IdadministradorNavigation")]
    public virtual ICollection<Proyecto> ProyectoIdadministradorNavigations { get; } = new List<Proyecto>();

    [InverseProperty("IdusuarioNavigation")]
    public virtual ICollection<Proyecto> ProyectoIdusuarioNavigations { get; } = new List<Proyecto>();
    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
}
