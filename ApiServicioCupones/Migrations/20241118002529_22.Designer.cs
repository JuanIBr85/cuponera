﻿// <auto-generated />
using System;
using ApiServicioCupones.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiServicioCupones.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20241118002529_22")]
    partial class _22
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiServicioCupones.Models.ArticuloModel", b =>
                {
                    b.Property<int>("Id_Articulo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Articulo"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion_Articulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Categoria")
                        .HasColumnType("int");

                    b.Property<string>("Nombre_Articulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Articulo");

                    b.HasIndex("Id_Categoria");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.CategoriaModel", b =>
                {
                    b.Property<int>("Id_Categoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Categoria"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Categoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.CuponModel", b =>
                {
                    b.Property<int>("Id_Cupon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Cupon"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<int>("Id_Tipo_Cupon")
                        .HasColumnType("int");

                    b.Property<decimal>("ImportePromo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PorcentajeDto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id_Cupon");

                    b.HasIndex("Id_Tipo_Cupon");

                    b.ToTable("Cupones");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.Cupon_CategoriaModel", b =>
                {
                    b.Property<int>("Id_Cupones_Categorias")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Cupones_Categorias"));

                    b.Property<int>("Id_Categoria")
                        .HasColumnType("int");

                    b.Property<int>("Id_Cupon")
                        .HasColumnType("int");

                    b.HasKey("Id_Cupones_Categorias");

                    b.HasIndex("Id_Categoria");

                    b.HasIndex("Id_Cupon");

                    b.ToTable("Cupones_Categorias");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.Cupon_ClienteModel", b =>
                {
                    b.Property<string>("NroCupon")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaAsignado")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Cupon")
                        .HasColumnType("int");

                    b.HasKey("NroCupon");

                    b.ToTable("Cupones_Clientes");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.Cupon_DetalleModel", b =>
                {
                    b.Property<int>("Id_Cupon")
                        .HasColumnType("int");

                    b.Property<int>("Id_Articulo")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("Id_Cupon", "Id_Articulo");

                    b.ToTable("Cupones_Detalle");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.Cupon_HistorialModel", b =>
                {
                    b.Property<int>("Id_Cupon")
                        .HasColumnType("int");

                    b.Property<string>("NroCupon")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaUso")
                        .HasColumnType("datetime2");

                    b.HasKey("Id_Cupon", "NroCupon");

                    b.ToTable("Cupones_Historial");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.PrecioModel", b =>
                {
                    b.Property<int>("Id_Precio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Precio"));

                    b.Property<int>("Id_Articulo")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id_Precio");

                    b.HasIndex("Id_Articulo")
                        .IsUnique();

                    b.ToTable("Precios");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.Tipo_CuponModel", b =>
                {
                    b.Property<int>("Id_Tipo_Cupon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Tipo_Cupon"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Tipo_Cupon");

                    b.ToTable("Tipo_Cupon");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.ArticuloModel", b =>
                {
                    b.HasOne("ApiServicioCupones.Models.CategoriaModel", "Categoria")
                        .WithMany("Articulos")
                        .HasForeignKey("Id_Categoria")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.CuponModel", b =>
                {
                    b.HasOne("ApiServicioCupones.Models.Tipo_CuponModel", "Tipo_Cupon")
                        .WithMany()
                        .HasForeignKey("Id_Tipo_Cupon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tipo_Cupon");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.Cupon_CategoriaModel", b =>
                {
                    b.HasOne("ApiServicioCupones.Models.CategoriaModel", "Categoria")
                        .WithMany("Cupones_Categorias")
                        .HasForeignKey("Id_Categoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiServicioCupones.Models.CuponModel", "Cupon")
                        .WithMany("Cupones_Categorias")
                        .HasForeignKey("Id_Cupon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Cupon");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.PrecioModel", b =>
                {
                    b.HasOne("ApiServicioCupones.Models.ArticuloModel", "Articulo")
                        .WithOne("Precio")
                        .HasForeignKey("ApiServicioCupones.Models.PrecioModel", "Id_Articulo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.ArticuloModel", b =>
                {
                    b.Navigation("Precio");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.CategoriaModel", b =>
                {
                    b.Navigation("Articulos");

                    b.Navigation("Cupones_Categorias");
                });

            modelBuilder.Entity("ApiServicioCupones.Models.CuponModel", b =>
                {
                    b.Navigation("Cupones_Categorias");
                });
#pragma warning restore 612, 618
        }
    }
}
