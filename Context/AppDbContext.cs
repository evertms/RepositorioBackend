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
            entity.HasKey(e => e.Idarea).HasName("PK__AreasCon__05D2C2920CD5DDBF");
        });

        modelBuilder.Entity<Concepto>(entity =>
        {
            entity.HasKey(e => e.Idconcepto).HasName("PK__Concepto__4AFAD66148EC0AD5");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.Idnotificacion).HasName("PK__Notifica__8687136781D897A0");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Notificacions).HasConstraintName("FK__Notificac__IDUsu__0A9D95DB");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.Idparticipante).HasName("PK__Particip__A316065C0AADE380");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Idproyecto).HasName("PK__Proyecto__CBAEF2A94B0E96B4");

            entity.HasOne(d => d.IdadministradorNavigation).WithMany(p => p.ProyectoIdadministradorNavigations).HasConstraintName("FK__Proyecto__IDAdmi__72C60C4A");

            entity.HasOne(d => d.IdtipoNavigation).WithMany(p => p.Proyectos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__IDTipo__73BA3083");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.ProyectoIdusuarioNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__IDUsua__71D1E811");

            entity.HasMany(d => d.Idareas).WithMany(p => p.Idproyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProyectoArea",
                    r => r.HasOne<AreasConocimiento>().WithMany()
                        .HasForeignKey("Idarea")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDAre__7A672E12"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Idproyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPro__797309D9"),
                    j =>
                    {
                        j.HasKey("Idproyecto", "Idarea").HasName("PK__Proyecto__FBF3DE80A36D1904");
                        j.ToTable("Proyecto_Area");
                    });

            entity.HasMany(d => d.Idconceptos).WithMany(p => p.Idproyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProyectoConcepto",
                    r => r.HasOne<Concepto>().WithMany()
                        .HasForeignKey("Idconcepto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDCon__01142BA1"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Idproyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPro__00200768"),
                    j =>
                    {
                        j.HasKey("Idproyecto", "Idconcepto").HasName("PK__Proyecto__CF015FCF205F42CD");
                        j.ToTable("Proyecto_Concepto");
                    });

            entity.HasMany(d => d.Idparticipantes).WithMany(p => p.Idproyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProyectoParticipante",
                    r => r.HasOne<Participante>().WithMany()
                        .HasForeignKey("Idparticipante")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPar__06CD04F7"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Idproyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proyecto___IDPro__05D8E0BE"),
                    j =>
                    {
                        j.HasKey("Idproyecto", "Idparticipante").HasName("PK__Proyecto__119F92CCCC586FB2");
                        j.ToTable("Proyecto_Participante");
                    });
        });

        modelBuilder.Entity<TiposTrabajo>(entity =>
        {
            entity.HasKey(e => e.Idtipo).HasName("PK__TiposTra__BEB088A605B6493A");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuario__52311169BD5DA9D8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
