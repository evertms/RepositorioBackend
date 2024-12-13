using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("Concepto")]
[Index("NombreConcepto", Name = "UQ__Concepto__9C80973589FD599F", IsUnique = true)]
public partial class Concepto
{
    [Key]
    [Column("IDConcepto")]
    public int Idconcepto { get; set; }

    [Required]
    [StringLength(255)]
    public string NombreConcepto { get; set; }

    [ForeignKey("Idconcepto")]
    [InverseProperty("Idconceptos")]
    public virtual ICollection<Proyecto> Idproyectos { get; } = new List<Proyecto>();
}
