using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("Usuario")]
[Index("Correo", Name = "UQ__Usuario__60695A19794B62C6", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("IDUsuario")]
    public int Idusuario { get; set; }

    [Required]
    [StringLength(255)]
    public string NombreCompleto { get; set; }

    [Required]
    [StringLength(255)]
    public string Correo { get; set; }

    [Required]
    public string Contraseña { get; set; }

    [Required]
    [StringLength(50)]
    public string Rol { get; set; }

    [InverseProperty("IdusuarioNavigation")]
    public virtual ICollection<Notificacion> Notificacions { get; } = new List<Notificacion>();

    [InverseProperty("IdadministradorNavigation")]
    public virtual ICollection<Proyecto> ProyectoIdadministradorNavigations { get; } = new List<Proyecto>();

    [InverseProperty("IdusuarioNavigation")]
    public virtual ICollection<Proyecto> ProyectoIdusuarioNavigations { get; } = new List<Proyecto>();
}
