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

    [Required]
    [StringLength(255)]
    public string NombreCompleto { get; set; }

    [Required]
    [StringLength(255)]
    public string Carrera { get; set; }

    [ForeignKey("Idparticipante")]
    [InverseProperty("Idparticipantes")]
    public virtual ICollection<Proyecto> Idproyectos { get; } = new List<Proyecto>();
}
