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
    [Migration("20240714150413_InitialCreate")]
    partial class InitialCreate
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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("HoursOut")
                        .HasColumnType("int");

                    b.Property<int>("MinutesIn")
                        .HasColumnType("int");

                    b.Property<int>("MinutesOut")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("ReportedHours")
                        .HasColumnType("int");

                    b.Property<int>("WorkTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("Attendances");
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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RenewalCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("SignDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Contracts");
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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("CivilId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EducationId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
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

                    b.Property<int?>("QuanHuyenId")
                        .HasColumnType("int");

                    b.Property<string>("QuanHuyenMaqh")
                        .HasColumnType("nvarchar(5)");

                    b.Property<int?>("TinhThanhPhoId")
                        .HasColumnType("int");

                    b.Property<string>("TinhThanhPhoMatp")
                        .HasColumnType("nvarchar(5)");

                    b.Property<int?>("XaPhuongThiTranId")
                        .HasColumnType("int");

                    b.Property<string>("XaPhuongThiTranXaid")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("EducationId");

                    b.HasIndex("QuanHuyenMaqh");

                    b.HasIndex("TinhThanhPhoMatp");

                    b.HasIndex("XaPhuongThiTranXaid");

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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("HealthCheckPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsuranceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuePlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Insurances");
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

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

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

                    b.HasIndex("EmployeeId");

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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.QuanHuyen", b =>
                {
                    b.Property<string>("Maqh")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Matp")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("NameQuanHuyen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Maqh");

                    b.HasIndex("Matp");

                    b.ToTable("QuanHuyens");
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

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

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

                    b.HasIndex("EmployeeId");

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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.TinhThanhPho", b =>
                {
                    b.Property<string>("Matp")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("NameCity")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Matp");

                    b.ToTable("TinhThanhPhos");
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

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("int");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDtae")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VacationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.WorkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Coefficient")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkTypes");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.XaPhuongThiTran", b =>
                {
                    b.Property<string>("Xaid")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Maqh")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("NameXaPhuong")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Xaid");

                    b.HasIndex("Maqh");

                    b.ToTable("XaPhuongThiTrans");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Attendance", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Entitie.Employee.WorkType", "WorkType")
                        .WithMany("Attendances")
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("WorkType");
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

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Contract", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
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

                    b.HasOne("Domain.Entities.Entitie.Employee.Education", "Education")
                        .WithMany()
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Entitie.Employee.QuanHuyen", "QuanHuyen")
                        .WithMany("Employees")
                        .HasForeignKey("QuanHuyenMaqh");

                    b.HasOne("Domain.Entities.Entitie.Employee.TinhThanhPho", "TinhThanhPho")
                        .WithMany("Employees")
                        .HasForeignKey("TinhThanhPhoMatp");

                    b.HasOne("Domain.Entities.Entitie.Employee.XaPhuongThiTran", "XaPhuongThiTran")
                        .WithMany("Employees")
                        .HasForeignKey("XaPhuongThiTranXaid");

                    b.Navigation("Branch");

                    b.Navigation("Education");

                    b.Navigation("QuanHuyen");

                    b.Navigation("TinhThanhPho");

                    b.Navigation("XaPhuongThiTran");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Insurance", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.OverTime", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Entitie.Employee.OvertimeType", "OvertimeType")
                        .WithMany()
                        .HasForeignKey("OvertimeTypeId");

                    b.Navigation("Employee");

                    b.Navigation("OvertimeType");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.QuanHuyen", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.TinhThanhPho", "TinhThanhPho")
                        .WithMany("QuanHuyens")
                        .HasForeignKey("Matp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TinhThanhPho");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Sanction", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Entitie.Employee.SanctionType", "SanctionType")
                        .WithMany("Sanctions")
                        .HasForeignKey("SanctionTypeId");

                    b.Navigation("Employee");

                    b.Navigation("SanctionType");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Vacation", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Entitie.Employee.VacationType", "VacationType")
                        .WithMany("Vacastions")
                        .HasForeignKey("VacationTypeId");

                    b.Navigation("Employee");

                    b.Navigation("VacationType");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.XaPhuongThiTran", b =>
                {
                    b.HasOne("Domain.Entities.Entitie.Employee.QuanHuyen", "QuanHuyen")
                        .WithMany("XaPhuongThiTrans")
                        .HasForeignKey("Maqh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuanHuyen");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Branch", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.Departnent", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.GeneralDepartment", b =>
                {
                    b.Navigation("Departnents");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.QuanHuyen", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("XaPhuongThiTrans");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.SanctionType", b =>
                {
                    b.Navigation("Sanctions");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.TinhThanhPho", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("QuanHuyens");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.VacationType", b =>
                {
                    b.Navigation("Vacastions");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.WorkType", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("Domain.Entities.Entitie.Employee.XaPhuongThiTran", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
