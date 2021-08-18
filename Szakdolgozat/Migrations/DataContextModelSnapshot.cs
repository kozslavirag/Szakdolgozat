﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Szakdolgozat.Context;

namespace Szakdolgozat.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Szakdolgozat.Models.BusinessModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OnlineBusiness")
                        .HasColumnType("int");

                    b.Property<int>("Retail")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("Szakdolgozat.Models.CateringModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("SalesVolume")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Catering");
                });

            modelBuilder.Entity("Szakdolgozat.Models.EmployeeModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FemaleEmployee")
                        .HasColumnType("int");

                    b.Property<int>("MaleEmployee")
                        .HasColumnType("int");

                    b.Property<int>("NumberofEmployee")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Szakdolgozat.Models.GDPModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GDP")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("GDP");
                });

            modelBuilder.Entity("Szakdolgozat.Models.IndustryModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("SalesAmount")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Industry");
                });

            modelBuilder.Entity("Szakdolgozat.Models.PriceModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GasOilPrice")
                        .HasColumnType("int");

                    b.Property<int>("PetrolPrice")
                        .HasColumnType("int");

                    b.Property<int>("StapleFoodPrice")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("Szakdolgozat.Models.TourismModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("NightForeignGuest")
                        .HasColumnType("int");

                    b.Property<int>("NightHungarianGuest")
                        .HasColumnType("int");

                    b.Property<int>("SpendForeignGuest")
                        .HasColumnType("int");

                    b.Property<int>("SpendHungarianGuest")
                        .HasColumnType("int");

                    b.Property<int>("TripForeignGuest")
                        .HasColumnType("int");

                    b.Property<int>("TripHungarianGuest")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Tourism");
                });

            modelBuilder.Entity("Szakdolgozat.Models.UnemployedModel", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FemaleUnemployed")
                        .HasColumnType("int");

                    b.Property<int>("MaleUnemployed")
                        .HasColumnType("int");

                    b.Property<int>("NumberofUnemployed")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Unemployed");
                });
#pragma warning restore 612, 618
        }
    }
}
