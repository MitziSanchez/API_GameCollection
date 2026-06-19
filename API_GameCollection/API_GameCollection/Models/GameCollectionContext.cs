using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_GameCollection.Models;

public partial class GameCollectionContext : DbContext
{
    public GameCollectionContext()
    {
    }

    public GameCollectionContext(DbContextOptions<GameCollectionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coleccion> Coleccions { get; set; }

    public virtual DbSet<DetalleColeccion> DetalleColeccions { get; set; }

    public virtual DbSet<EstadoJuego> EstadoJuegos { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Videojuego> Videojuegos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=GameCollection;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coleccion>(entity =>
        {
            entity.HasKey(e => e.ColeccionId).HasName("PK__Coleccio__FC447A26BBD5527C");

            entity.ToTable("Coleccion");

            entity.HasIndex(e => e.UsuarioId, "UQ__Coleccio__2B3DE7B995781DBD").IsUnique();

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithOne(p => p.Coleccion)
                .HasForeignKey<Coleccion>(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coleccion_Usuario");
        });

        modelBuilder.Entity<DetalleColeccion>(entity =>
        {
            entity.HasKey(e => e.DetalleColeccionId).HasName("PK__DetalleC__D28822C84CE5FC25");

            entity.ToTable("DetalleColeccion");

            entity.Property(e => e.Calificacion)
                .HasComment("Calificación considera valores de 0 a 10")
                .HasColumnType("decimal(3, 1)");
            entity.Property(e => e.FechaCalificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Coleccion).WithMany(p => p.DetalleColeccions)
                .HasForeignKey(d => d.ColeccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleColeccion_Coleccion");

            entity.HasOne(d => d.EstadoJuego).WithMany(p => p.DetalleColeccions)
                .HasForeignKey(d => d.EstadoJuegoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleColeccion_EstadoJuego");

            entity.HasOne(d => d.Videojuego).WithMany(p => p.DetalleColeccions)
                .HasForeignKey(d => d.VideojuegoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleColeccion_Videojuego");
        });

        modelBuilder.Entity<EstadoJuego>(entity =>
        {
            entity.HasKey(e => e.EstadoJuegoId).HasName("PK__EstadoJu__4D12980D3AF4FBB0");

            entity.ToTable("EstadoJuego");

            entity.Property(e => e.Nombre).HasMaxLength(15);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PK__Genero__A99D02483CE3A235");

            entity.ToTable("Genero");

            entity.Property(e => e.Nombre).HasMaxLength(60);
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaId).HasName("PK__Platafor__B83567ED1DF38C82");

            entity.ToTable("Plataforma");

            entity.Property(e => e.Nombre).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8286B98AE");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A1967A1109E").IsUnique();

            entity.Property(e => e.Correo).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Videojuego>(entity =>
        {
            entity.HasKey(e => e.VideojuegoId).HasName("PK__Videojue__D6B5FCA9AD58C91A");

            entity.ToTable("Videojuego");

            entity.Property(e => e.Titulo).HasMaxLength(250);

            entity.HasOne(d => d.Genero).WithMany(p => p.Videojuegos)
                .HasForeignKey(d => d.GeneroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videojuego_Genero");

            entity.HasOne(d => d.Plataforma).WithMany(p => p.Videojuegos)
                .HasForeignKey(d => d.PlataformaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videojuego_Plataforma");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
