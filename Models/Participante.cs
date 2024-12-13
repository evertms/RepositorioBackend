using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models;

[Table("Participante")]
public partial class Participante
{
    [Key]
    [Column("IDParticipante")]
    public int Idparticipante { get; set; }

    [StringLength(255)]
    public string NombreCompleto { get; set; } = null!;

    [StringLength(255)]
    public string Carrera { get; set; } = null!;

    [ForeignKey("Idparticipante")]
    [InverseProperty("Idparticipantes")]
    public virtual ICollection<Proyecto> Idproyectos { get; } = new List<Proyecto>();
}
