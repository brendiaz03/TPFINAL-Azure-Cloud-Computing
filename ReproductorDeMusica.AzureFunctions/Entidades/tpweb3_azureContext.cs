using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReproductorDeMusica.AzureFunctions.Entidades
{
    public partial class tpweb3_azureContext : DbContext
    {
        public tpweb3_azureContext()
        {
        }

        public tpweb3_azureContext(DbContextOptions<tpweb3_azureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailRegistro> EmailRegistros { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=pw3-servidor.database.windows.net;Database=tpweb3_azure;User=pw3Admin;Password=Admin242;Trusted_Connection=True;Encrypt=False;Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailRegistro>(entity =>
            {
                entity.ToTable("EmailRegistro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.EsEnviado).HasColumnName("esEnviado");

                entity.Property(e => e.FechaCreada)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreada");

                entity.Property(e => e.FechaProxima)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaProxima");

                entity.Property(e => e.TipoPlan)
                    .HasMaxLength(255)
                    .HasColumnName("tipoPlan");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(255)
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
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

                entity.Property(e => e.ImagenUsuario).HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombreUsuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
