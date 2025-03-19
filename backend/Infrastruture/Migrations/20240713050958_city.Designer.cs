﻿// <auto-generated />
using System;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastruture.Migrations
{
    [DbContext(typeof(AplicationContext))]
    [Migration("20240713050958_city")]
    partial class city
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.6.24327.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Entitie.Account.AplictionUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AplictionUsers");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Account.RefreshTokenInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Account.SystemRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SystemRoles");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Account.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartnentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartnentId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Citys");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Departnent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GeneralDepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneralDepartmentId");

                    b.ToTable("Departners");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("CivilId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("TowId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.GeneralDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GeneralDepartment");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.OverTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CivilId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OvertimTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("OvertimeTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OvertimeTypeId");

                    b.ToTable("OverTime");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.OvertimeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OvertimeTypes");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Sanction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CivilId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Punishment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PunishmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SanctionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SanctionTypeId");

                    b.ToTable("Sanctions");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.SanctionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SanctionTypes");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Tow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Tows");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Vacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CivilId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("int");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDtae")
                        .HasColumnType("datetime2");

                    b.Property<int>("VacationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VacationTypeId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.VacationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VacastionTypes");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Branch", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Departnent", "Departnent")
                        .WithMany("Branches")
                        .HasForeignKey("DepartnentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departnent");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Departnent", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.GeneralDepartment", "GeneralDepartment")
                        .WithMany("Departnents")
                        .HasForeignKey("GeneralDepartmentId");

                    b.Navigation("GeneralDepartment");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Employee", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId");

                    b.HasOne("Domain.Entities.Entitie.Employee.Tow", "Tow")
                        .WithMany("Employees")
                        .HasForeignKey("TowId");

                    b.Navigation("Branch");

                    b.Navigation("Tow");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.OverTime", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.OvertimeType", "OvertimeType")
                        .WithMany()
                        .HasForeignKey("OvertimeTypeId");

                    b.Navigation("OvertimeType");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Sanction", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.SanctionType", "SanctionType")
                        .WithMany("Sanctions")
                        .HasForeignKey("SanctionTypeId");

                    b.Navigation("SanctionType");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Tow", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.City", "City")
                        .WithMany("Tows")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Vacation", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.VacationType", "VacationType")
                        .WithMany("Vacastions")
                        .HasForeignKey("VacationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VacationType");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Branch", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.City", b =>
                {
                    b.Navigation("Tows");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Departnent", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.GeneralDepartment", b =>
                {
                    b.Navigation("Departnents");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.SanctionType", b =>
                {
                    b.Navigation("Sanctions");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Tow", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.VacationType", b =>
                {
                    b.Navigation("Vacastions");
                });
#pragma warning restore 612, 618
        }
    }
}
