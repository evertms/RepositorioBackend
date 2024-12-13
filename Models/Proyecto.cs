using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("Proyecto")]
public partial class Proyecto
{
    [Key]
    [Column("IDProyecto")]
    public int Idproyecto { get; set; }

    [StringLength(255)]
    public string Titulo { get; set; } = null!;

    public string Resumen { get; set; } = null!;

    [StringLength(255)]
    public string? EnlaceRepositorio { get; set; }

    [Column("DocumentoPDF")]
    [StringLength(255)]
    public string DocumentoPdf { get; set; } = null!;

    [StringLength(50)]
    public string Estado { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaSubida { get; set; }

    [StringLength(50)]
    public string EstatusAprobacion { get; set; } = null!;

    public string? ComentarioAprobacion { get; set; }

    [Column("IDUsuario")]
    public int Idusuario { get; set; }

    [Column("IDAdministrador")]
    public int? Idadministrador { get; set; }

    [Column("IDTipo")]
    public int Idtipo { get; set; }

    [ForeignKey("Idadministrador")]
    [InverseProperty("ProyectoIdadministradorNavigations")]
    public virtual Usuario? IdadministradorNavigation { get; set; }

    [ForeignKey("Idtipo")]
    [InverseProperty("Proyectos")]
    public virtual TiposTrabajo IdtipoNavigation { get; set; } = null!;

    [ForeignKey("Idusuario")]
    [InverseProperty("ProyectoIdusuarioNavigations")]
    public virtual Usuario IdusuarioNavigation { get; set; } = null!;

    [ForeignKey("Idproyecto")]
    [InverseProperty("Idproyectos")]
    public virtual ICollection<AreasConocimiento> Idareas { get; } = new List<AreasConocimiento>();

    [ForeignKey("Idproyecto")]
    [InverseProperty("Idproyectos")]
    public virtual ICollection<Concepto> Idconceptos { get; } = new List<Concepto>();

    [ForeignKey("Idproyecto")]
    [InverseProperty("Idproyectos")]
    public virtual ICollection<Participante> Idparticipantes { get; } = new List<Participante>();
}
