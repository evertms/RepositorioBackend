using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("Notificacion")]
public partial class Notificacion
{
    [Key]
    [Column("IDNotificacion")]
    public int Idnotificacion { get; set; }

    [Column("IDUsuario")]
    public int? Idusuario { get; set; }

    [StringLength(50)]
    public string RolReceptor { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    public bool Leida { get; set; }

    [ForeignKey("Idusuario")]
    [InverseProperty("Notificacions")]
    public virtual Usuario? IdusuarioNavigation { get; set; }
}
