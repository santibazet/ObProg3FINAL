﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaGym2.Datos;

#nullable disable

namespace PruebaGym2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruebaGym2.Models.Ejercicio", b =>
                {
                    b.Property<int>("IdEjercicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEjercicio"));

                    b.Property<string>("DescripcionEjercicio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreEjercicio")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("RutinaIdRutina")
                        .HasColumnType("int");

                    b.Property<int?>("TipoMaquinaId")
                        .HasColumnType("int");

                    b.HasKey("IdEjercicio");

                    b.HasIndex("RutinaIdRutina");

                    b.HasIndex("TipoMaquinaId");

                    b.ToTable("Ejercicios");
                });

            modelBuilder.Entity("PruebaGym2.Models.Local", b =>
                {
                    b.Property<int>("IdLocal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLocal"));

                    b.Property<string>("Ciudad")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdResponsable")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("IdLocal");

                    b.HasIndex("IdResponsable");

                    b.ToTable("Locales");
                });

            modelBuilder.Entity("PruebaGym2.Models.Maquina", b =>
                {
                    b.Property<int>("IdMaquina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMaquina"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<bool>("Disponible")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdLocal")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoMaquina")
                        .HasColumnType("int");

                    b.Property<int>("PrecioCompra")
                        .HasColumnType("int");

                    b.Property<int>("VidaUtil")
                        .HasColumnType("int");

                    b.HasKey("IdMaquina");

                    b.HasIndex("IdLocal");

                    b.HasIndex("IdTipoMaquina");

                    b.ToTable("Maquinas");
                });

            modelBuilder.Entity("PruebaGym2.Models.Responsable", b =>
                {
                    b.Property<int>("idResponsable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idResponsable"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("idResponsable");

                    b.ToTable("Responsables");
                });

            modelBuilder.Entity("PruebaGym2.Models.Rutina", b =>
                {
                    b.Property<int>("IdRutina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRutina"));

                    b.Property<double?>("CalificacionPromedio")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("TipoRutina")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRutina");

                    b.ToTable("Rutinas");
                });

            modelBuilder.Entity("PruebaGym2.Models.RutinaEjercicio", b =>
                {
                    b.Property<int>("IdRutina")
                        .HasColumnType("int");

                    b.Property<int>("IdEjercicio")
                        .HasColumnType("int");

                    b.Property<int>("Repeticiones")
                        .HasColumnType("int");

                    b.Property<int>("Sets")
                        .HasColumnType("int");

                    b.HasKey("IdRutina", "IdEjercicio");

                    b.HasIndex("IdEjercicio");

                    b.ToTable("RutinaEjercicios");
                });

            modelBuilder.Entity("PruebaGym2.Models.Socio", b =>
                {
                    b.Property<int>("IdSocio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSocio"));

                    b.Property<int>("IdLocal")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreSocio")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("Telefono")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("TipoSocio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSocio");

                    b.HasIndex("IdLocal");

                    b.ToTable("Socios");
                });

            modelBuilder.Entity("PruebaGym2.Models.SocioRutina", b =>
                {
                    b.Property<int>("IdSocio")
                        .HasColumnType("int");

                    b.Property<int>("IdRutina")
                        .HasColumnType("int");

                    b.Property<int?>("Calificacion")
                        .HasColumnType("int");

                    b.HasKey("IdSocio", "IdRutina");

                    b.HasIndex("IdRutina");

                    b.ToTable("SociosRutinas");
                });

            modelBuilder.Entity("PruebaGym2.Models.TipoMaquina", b =>
                {
                    b.Property<int>("IdTipoMaquina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoMaquina"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreTipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdTipoMaquina");

                    b.ToTable("TipoMaquinas");
                });

            modelBuilder.Entity("PruebaGym2.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmarContraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("PruebaGym2.Models.Ejercicio", b =>
                {
                    b.HasOne("PruebaGym2.Models.Rutina", null)
                        .WithMany("EjerciciosRutina")
                        .HasForeignKey("RutinaIdRutina");

                    b.HasOne("PruebaGym2.Models.TipoMaquina", "TipoMaquina")
                        .WithMany()
                        .HasForeignKey("TipoMaquinaId");

                    b.Navigation("TipoMaquina");
                });

            modelBuilder.Entity("PruebaGym2.Models.Local", b =>
                {
                    b.HasOne("PruebaGym2.Models.Responsable", "Responsable")
                        .WithMany()
                        .HasForeignKey("IdResponsable")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Responsable");
                });

            modelBuilder.Entity("PruebaGym2.Models.Maquina", b =>
                {
                    b.HasOne("PruebaGym2.Models.Local", "Local")
                        .WithMany("MaquinasDeLocal")
                        .HasForeignKey("IdLocal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PruebaGym2.Models.TipoMaquina", "TipoMaquina")
                        .WithMany("Maquinas")
                        .HasForeignKey("IdTipoMaquina")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Local");

                    b.Navigation("TipoMaquina");
                });

            modelBuilder.Entity("PruebaGym2.Models.RutinaEjercicio", b =>
                {
                    b.HasOne("PruebaGym2.Models.Ejercicio", "Ejercicio")
                        .WithMany()
                        .HasForeignKey("IdEjercicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PruebaGym2.Models.Rutina", "Rutina")
                        .WithMany("RutinaEjercicios")
                        .HasForeignKey("IdRutina")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ejercicio");

                    b.Navigation("Rutina");
                });

            modelBuilder.Entity("PruebaGym2.Models.Socio", b =>
                {
                    b.HasOne("PruebaGym2.Models.Local", "Local")
                        .WithMany("SociosAfiliados")
                        .HasForeignKey("IdLocal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Local");
                });

            modelBuilder.Entity("PruebaGym2.Models.SocioRutina", b =>
                {
                    b.HasOne("PruebaGym2.Models.Rutina", "Rutina")
                        .WithMany("SocioRutinas")
                        .HasForeignKey("IdRutina")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PruebaGym2.Models.Socio", "Socio")
                        .WithMany("SocioRutinas")
                        .HasForeignKey("IdSocio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rutina");

                    b.Navigation("Socio");
                });

            modelBuilder.Entity("PruebaGym2.Models.Local", b =>
                {
                    b.Navigation("MaquinasDeLocal");

                    b.Navigation("SociosAfiliados");
                });

            modelBuilder.Entity("PruebaGym2.Models.Rutina", b =>
                {
                    b.Navigation("EjerciciosRutina");

                    b.Navigation("RutinaEjercicios");

                    b.Navigation("SocioRutinas");
                });

            modelBuilder.Entity("PruebaGym2.Models.Socio", b =>
                {
                    b.Navigation("SocioRutinas");
                });

            modelBuilder.Entity("PruebaGym2.Models.TipoMaquina", b =>
                {
                    b.Navigation("Maquinas");
                });
#pragma warning restore 612, 618
        }
    }
}