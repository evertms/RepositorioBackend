using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("Concepto")]
[Index("NombreConcepto", Name = "UQ__Concepto__9C8097353FF02CE2", IsUnique = true)]
public partial class Concepto
{
    [Key]
    [Column("IDConcepto")]
    public int Idconcepto { get; set; }

    [StringLength(255)]
    public string NombreConcepto { get; set; } = null!;

    [ForeignKey("Idconcepto")]
    [InverseProperty("Idconceptos")]
    public virtual ICollection<Proyecto> Idproyectos { get; } = new List<Proyecto>();
}
