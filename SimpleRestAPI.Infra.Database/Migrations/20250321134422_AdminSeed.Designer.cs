﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleRestAPI.Infra.Database.Context;

#nullable disable

namespace SimpleRestAPI.Infra.Database.Migrations
{
    [DbContext(typeof(SimpleRestDB))]
    [Migration("20250321134422_AdminSeed")]
    partial class AdminSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SimpleRestAPI.Domain.Entities.Employees.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("DocNumber")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ManagerName")
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f18534d4-8679-47f4-9b37-25e85bc35f2f"),
                            Active = true,
                            BirthDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = "Admin City",
                            CreatedAt = new DateTime(2025, 3, 21, 10, 44, 22, 253, DateTimeKind.Local).AddTicks(8369),
                            Deleted = false,
                            DocNumber = "admin",
                            Email = "ispadoto@gmail.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            ManagerName = "Himself",
                            Password = "admin",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("SimpleRestAPI.Domain.Entities.EmployeesPhones.EmployeePhone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("PhoneType")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("EmployeePhone");
                });
#pragma warning restore 612, 618
        }
    }
}
