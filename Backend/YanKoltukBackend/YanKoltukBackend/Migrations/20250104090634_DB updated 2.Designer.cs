﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YanKoltukBackend.Data;

#nullable disable

namespace YanKoltukBackend.Migrations
{
    [DbContext(typeof(YanKoltukDbContext))]
    [Migration("20250104090634_DB updated 2")]
    partial class DBupdated2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdminId");

                    b.HasIndex("UserId");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Manager", b =>
                {
                    b.Property<int>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerId"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ManagerId");

                    b.HasIndex("AdminId");

                    b.HasIndex("UserId");

                    b.ToTable("Manager", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Parent", b =>
                {
                    b.Property<int>("ParentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParentId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ParentId");

                    b.HasIndex("IdNo")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Parent", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.ParentNotification", b =>
                {
                    b.Property<int>("ParentNotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParentNotificationId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("ParentNotificationId");

                    b.HasIndex("ParentId");

                    b.ToTable("ParentNotification", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("DepartureLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverIdNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DriverName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StewardessIdNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StewardessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StewardessPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StewardessPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ServiceId");

                    b.HasIndex("DriverIdNo")
                        .IsUnique()
                        .HasFilter("[DriverIdNo] IS NOT NULL");

                    b.HasIndex("ManagerId");

                    b.HasIndex("Plate")
                        .IsUnique();

                    b.HasIndex("StewardessIdNo")
                        .IsUnique()
                        .HasFilter("[StewardessIdNo] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.ServiceLog", b =>
                {
                    b.Property<int>("ServiceLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceLogId"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("DropOffTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("PickupTime")
                        .HasColumnType("time");

                    b.Property<int?>("StudentServiceId")
                        .HasColumnType("int");

                    b.HasKey("ServiceLogId");

                    b.HasIndex("StudentServiceId");

                    b.ToTable("ServiceLog", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("IdNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("SchoolNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StudentId");

                    b.HasIndex("IdNo")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.HasIndex("SchoolNo")
                        .IsUnique();

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.StudentService", b =>
                {
                    b.Property<int>("StudentServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentServiceId"));

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<string>("DriverNote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExcludedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExcludedStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("SortIndex")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("StudentServiceId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentService", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Admin", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Manager", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.Admin", "Admin")
                        .WithMany("Managers")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YanKoltukBackend.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Parent", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.ParentNotification", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.Parent", "Parent")
                        .WithMany("Notifications")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Service", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.Manager", "Manager")
                        .WithMany("Services")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YanKoltukBackend.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Manager");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.ServiceLog", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.StudentService", "StudentService")
                        .WithMany("ServiceLogs")
                        .HasForeignKey("StudentServiceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("StudentService");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Student", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.Parent", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.StudentService", b =>
                {
                    b.HasOne("YanKoltukBackend.Models.Entities.Service", "Service")
                        .WithMany("StudentServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("YanKoltukBackend.Models.Entities.Student", "Student")
                        .WithOne("StudentService")
                        .HasForeignKey("YanKoltukBackend.Models.Entities.StudentService", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Admin", b =>
                {
                    b.Navigation("Managers");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Manager", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Parent", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Service", b =>
                {
                    b.Navigation("StudentServices");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.Student", b =>
                {
                    b.Navigation("StudentService");
                });

            modelBuilder.Entity("YanKoltukBackend.Models.Entities.StudentService", b =>
                {
                    b.Navigation("ServiceLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
