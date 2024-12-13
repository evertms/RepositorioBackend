using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("AreasConocimiento")]
[Index("NombreArea", Name = "UQ__AreasCon__D5E8EEB5D70ABFD4", IsUnique = true)]
public partial class AreasConocimiento
{
    [Key]
    [Column("IDArea")]
    public int Idarea { get; set; }

    [StringLength(255)]
    public string NombreArea { get; set; } = null!;

    [ForeignKey("Idarea")]
    [InverseProperty("Idareas")]
    public virtual ICollection<Proyecto> Idproyectos { get; } = new List<Proyecto>();
}
