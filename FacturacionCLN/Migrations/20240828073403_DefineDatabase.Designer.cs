﻿// <auto-generated />
using System;
using FacturacionCLN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FacturacionCLN.Migrations
{
    [DbContext(typeof(FacturacionDbContext))]
    [Migration("20240828073403_DefineDatabase")]
    partial class DefineDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FacturacionCLN.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CodigoPais")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("FacturacionCLN.Models.DetalleFactura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdFactura")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUnitarioCordoba")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioUnitarioDolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubtotalCordoba")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubtotalDolar")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdFactura");

                    b.HasIndex("IdProducto");

                    b.ToTable("DetallesFactura");
                });

            modelBuilder.Entity("FacturacionCLN.Models.Factura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("IVACordoba")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IVADolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<decimal>("MontoTotalCordoba")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MontoTotalDolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubTotalCordoba")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubTotalDolar")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("FacturacionCLN.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.Property<decimal>("PrecioCordoba")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioDolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("FacturacionCLN.Models.TasaCambio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("date");

                    b.Property<decimal>("Tasa")
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.ToTable("TasaCambios");
                });

            modelBuilder.Entity("FacturacionCLN.Models.DetalleFactura", b =>
                {
                    b.HasOne("FacturacionCLN.Models.Factura", "Factura")
                        .WithMany("DetallesFactura")
                        .HasForeignKey("IdFactura")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FacturacionCLN.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factura");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("FacturacionCLN.Models.Factura", b =>
                {
                    b.HasOne("FacturacionCLN.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("FacturacionCLN.Models.Factura", b =>
                {
                    b.Navigation("DetallesFactura");
                });
#pragma warning restore 612, 618
        }
    }
}
