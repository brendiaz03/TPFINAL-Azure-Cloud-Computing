using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReproductorDeMusica.Entidades.Entidades;

public partial class Tpweb3AzureContext : DbContext
{
    public Tpweb3AzureContext()
    {
    }

    public Tpweb3AzureContext(DbContextOptions<Tpweb3AzureContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cancion> Cancions { get; set; }

    public virtual DbSet<ListaCancione> ListaCanciones { get; set; }

    public virtual DbSet<ListaReproduccion> ListaReproduccions { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=pw3-servidor.database.windows.net;Database=tpweb3_azure;User=pw3Admin;Password=Admin242;Trusted_Connection=True;Encrypt=False;Integrated Security=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cancion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_cancion");

            entity.ToTable("Cancion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Album)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("album");
            entity.Property(e => e.Artista)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("artista");
            entity.Property(e => e.Creador).HasColumnName("creador");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.CreadorNavigation).WithMany(p => p.Cancions)
                .HasForeignKey(d => d.Creador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_creador");
        });

        modelBuilder.Entity<ListaCancione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_lista_canciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCancion).HasColumnName("idCancion");
            entity.Property(e => e.IdListaReproduccion).HasColumnName("idListaReproduccion");

            entity.HasOne(d => d.IdCancionNavigation).WithMany(p => p.ListaCanciones)
                .HasForeignKey(d => d.IdCancion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cancion");

            entity.HasOne(d => d.IdListaReproduccionNavigation).WithMany(p => p.ListaCanciones)
                .HasForeignKey(d => d.IdListaReproduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_listaReproduccion");
        });

        modelBuilder.Entity<ListaReproduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_listaReproduccion");

            entity.ToTable("ListaReproduccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCreacion).HasColumnName("fechaCreacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ListaReproduccions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idUsuario");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_plan");

            entity.ToTable("Plan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.TipoPlan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoPlan");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_usuario");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdPlan).HasColumnName("idPlan");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idPlan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
