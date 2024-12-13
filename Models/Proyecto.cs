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

    [Required]
    [StringLength(255)]
    public string Titulo { get; set; }

    [Required]
    public string Resumen { get; set; }

    [StringLength(255)]
    public string EnlaceRepositorio { get; set; }

    [Required]
    [Column("DocumentoPDF")]
    [StringLength(255)]
    public string DocumentoPdf { get; set; }

    [Required]
    [StringLength(50)]
    public string Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaSubida { get; set; }

    [Required]
    [StringLength(50)]
    public string EstatusAprobacion { get; set; }

    public string ComentarioAprobacion { get; set; }

    [Column("IDUsuario")]
    public int Idusuario { get; set; }

    [Column("IDAdministrador")]
    public int? Idadministrador { get; set; }

    [Column("IDTipo")]
    public int Idtipo { get; set; }

    [ForeignKey("Idadministrador")]
    [InverseProperty("ProyectoIdadministradorNavigations")]
    public virtual Usuario IdadministradorNavigation { get; set; }

    [ForeignKey("Idtipo")]
    [InverseProperty("Proyectos")]
    public virtual TiposTrabajo IdtipoNavigation { get; set; }

    [ForeignKey("Idusuario")]
    [InverseProperty("ProyectoIdusuarioNavigations")]
    public virtual Usuario IdusuarioNavigation { get; set; }

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
