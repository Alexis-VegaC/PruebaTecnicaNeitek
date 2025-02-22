﻿// <auto-generated />
using System;
using BlazorAppAlexis.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorAppAlexis.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlazorAppAlexis.Shared.Meta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaCreada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PorcentajeCumplimiento")
                        .HasColumnType("float");

                    b.Property<int>("TareasCompletadas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Metas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FechaCreada = new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4401),
                            Nombre = "Meta 1",
                            PorcentajeCumplimiento = 0.0,
                            TareasCompletadas = 0
                        },
                        new
                        {
                            Id = 2,
                            FechaCreada = new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4416),
                            Nombre = "Meta 2",
                            PorcentajeCumplimiento = 0.0,
                            TareasCompletadas = 0
                        });
                });

            modelBuilder.Entity("BlazorAppAlexis.Shared.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("EsImportante")
                        .HasColumnType("bit");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreada")
                        .HasColumnType("datetime2");

                    b.Property<int>("MetaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MetaId");

                    b.ToTable("Tareas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EsImportante = false,
                            Estado = "Abierta",
                            FechaCreada = new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4520),
                            MetaId = 1,
                            Nombre = "Tarea 1.1"
                        },
                        new
                        {
                            Id = 2,
                            EsImportante = true,
                            Estado = "Abierta",
                            FechaCreada = new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4524),
                            MetaId = 1,
                            Nombre = "Tarea 1.2"
                        },
                        new
                        {
                            Id = 3,
                            EsImportante = false,
                            Estado = "Abierta",
                            FechaCreada = new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4525),
                            MetaId = 2,
                            Nombre = "Tarea 2.1"
                        });
                });

            modelBuilder.Entity("BlazorAppAlexis.Shared.Tarea", b =>
                {
                    b.HasOne("BlazorAppAlexis.Shared.Meta", "Meta")
                        .WithMany("Tareas")
                        .HasForeignKey("MetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meta");
                });

            modelBuilder.Entity("BlazorAppAlexis.Shared.Meta", b =>
                {
                    b.Navigation("Tareas");
                });
#pragma warning restore 612, 618
        }
    }
}
