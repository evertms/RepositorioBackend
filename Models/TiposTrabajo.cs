using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("TiposTrabajo")]
[Index("NombreTipo", Name = "UQ__TiposTra__7586661C3447A058", IsUnique = true)]
public partial class TiposTrabajo
{
    [Key]
    [Column("IDTipo")]
    public int Idtipo { get; set; }

    [StringLength(255)]
    public string NombreTipo { get; set; } = null!;

    [InverseProperty("IdtipoNavigation")]
    public virtual ICollection<Proyecto> Proyectos { get; } = new List<Proyecto>();
}
