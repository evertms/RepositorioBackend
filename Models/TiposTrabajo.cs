using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("TiposTrabajo")]
[Index("NombreTipo", Name = "UQ__TiposTra__7586661C4E89714A", IsUnique = true)]
public partial class TiposTrabajo
{
    [Key]
    [Column("IDTipo")]
    public int Idtipo { get; set; }

    [Required]
    [StringLength(255)]
    public string NombreTipo { get; set; }

    [InverseProperty("IdtipoNavigation")]
    public virtual ICollection<Proyecto> Proyectos { get; } = new List<Proyecto>();
}
