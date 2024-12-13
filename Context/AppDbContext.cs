using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AreasConocimiento> AreasConocimientos { get; set; }

    public virtual DbSet<Concepto> Conceptos { get; set; }

    public virtual DbSet<Notificacion> Notificaciones { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<TiposTrabajo> TiposTrabajos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=GestionProyectos; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AreasConocimiento>(entity =>
        {
            entity.HasKey(e => e.Idarea).HasName("PK__AreasCon__05D2C292A2036F7B");
        });

        modelBuilder.Entity<Concepto>(entity =>
        {
            entity.HasKey(e => e.Idconcepto).HasName("PK__Concepto__4AFAD6615EBBCBB6");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.Idnotificacion).HasName("PK__Notifica__868713676A18480D");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Notificaciones).HasConstraintName("FK__Notificac__IDUsu__0E6E26BF");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.Idparticipante).HasName("PK__Particip__A316065C98F545A4");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Idproyecto).HasName("PK__Proyecto__CBAEF2A914730282");

            entity.HasOne(d => d.IdadministradorNavigation).WithMany(p => p.ProyectoIdadministradorNavigations).HasConstraintName("FK__Proyecto__IDAdmi__76969D2E");

            entity.HasOne(d => d.IdtipoNavigation).WithMany(p => p.Proyectos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__IDTipo__778AC167");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.ProyectoIdusuarioNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__IDUsua__75A278F5");

            entity.HasMany(d => d.Idareas).WithMany(p => p.Idproyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProyectoArea",
                    r => r.HasOne<AreasConocimiento>().WithMany()
                        .HasForeignKey("Idarea")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDAre__7E37BEF6"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Idproyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPro__7D439ABD"),
                    j =>
                    {
                        j.HasKey("Idproyecto", "Idarea").HasName("PK__Proyecto__FBF3DE8020238636");
                        j.ToTable("Proyecto_Area");
                    });

            entity.HasMany(d => d.Idconceptos).WithMany(p => p.Idproyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProyectoConcepto",
                    r => r.HasOne<Concepto>().WithMany()
                        .HasForeignKey("Idconcepto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDCon__04E4BC85"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Idproyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPro__03F0984C"),
                    j =>
                    {
                        j.HasKey("Idproyecto", "Idconcepto").HasName("PK__Proyecto__CF015FCF27785256");
                        j.ToTable("Proyecto_Concepto");
                    });

            entity.HasMany(d => d.Idparticipantes).WithMany(p => p.Idproyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProyectoParticipante",
                    r => r.HasOne<Participante>().WithMany()
                        .HasForeignKey("Idparticipante")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPar__0A9D95DB"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Idproyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPro__09A971A2"),
                    j =>
                    {
                        j.HasKey("Idproyecto", "Idparticipante").HasName("PK__Proyecto__119F92CC8D1F421F");
                        j.ToTable("Proyecto_Participante");
                    });
        });

        modelBuilder.Entity<TiposTrabajo>(entity =>
        {
            entity.HasKey(e => e.Idtipo).HasName("PK__TiposTra__BEB088A68CECBFA4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuario__523111694D079738");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
