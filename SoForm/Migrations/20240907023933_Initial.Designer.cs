﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoForm.Data;

#nullable disable

namespace SoForm.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240907023933_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SoForm.Models.ProcessDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Llegada")
                        .HasColumnType("int");

                    b.Property<int>("Prioridad")
                        .HasColumnType("int");

                    b.Property<string>("Proceso")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Rafaga")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Process");
                });
#pragma warning restore 612, 618
        }
    }
}
