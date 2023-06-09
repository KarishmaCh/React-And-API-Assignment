﻿// <auto-generated />
using System;
using MatterWebAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MatterWebAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230424050008_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MatterWebAPI.Models.Attorney", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("JurisdictionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("JurisdictionId");

                    b.ToTable("Attorneys");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttorneyId")
                        .HasColumnType("int");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HoursWorked")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MatterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttorneyId");

                    b.HasIndex("MatterId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Jurisdiction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jurisdictions");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Matter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BillingAttorneyId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ClientId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("JurisdictionId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResponsibleAttorneyId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillingAttorneyId");

                    b.HasIndex("ClientId");

                    b.HasIndex("JurisdictionId");

                    b.HasIndex("ResponsibleAttorneyId");

                    b.ToTable("Matters");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Attorney", b =>
                {
                    b.HasOne("MatterWebAPI.Models.Jurisdiction", "Jurisdiction")
                        .WithMany("Attorneys")
                        .HasForeignKey("JurisdictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jurisdiction");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Invoice", b =>
                {
                    b.HasOne("MatterWebAPI.Models.Attorney", "Attorney")
                        .WithMany()
                        .HasForeignKey("AttorneyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MatterWebAPI.Models.Matter", "Matter")
                        .WithMany("Invoices")
                        .HasForeignKey("MatterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Attorney");

                    b.Navigation("Matter");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Matter", b =>
                {
                    b.HasOne("MatterWebAPI.Models.Attorney", "BillingAttorney")
                        .WithMany()
                        .HasForeignKey("BillingAttorneyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MatterWebAPI.Models.Client", "Client")
                        .WithMany("Matters")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MatterWebAPI.Models.Jurisdiction", "Jurisdiction")
                        .WithMany("Matters")
                        .HasForeignKey("JurisdictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MatterWebAPI.Models.Attorney", "ResponsibleAttorney")
                        .WithMany()
                        .HasForeignKey("ResponsibleAttorneyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BillingAttorney");

                    b.Navigation("Client");

                    b.Navigation("Jurisdiction");

                    b.Navigation("ResponsibleAttorney");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Client", b =>
                {
                    b.Navigation("Matters");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Jurisdiction", b =>
                {
                    b.Navigation("Attorneys");

                    b.Navigation("Matters");
                });

            modelBuilder.Entity("MatterWebAPI.Models.Matter", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
