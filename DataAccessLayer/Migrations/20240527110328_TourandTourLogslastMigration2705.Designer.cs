﻿// <auto-generated />
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(TourPlannerDbContext))]
    [Migration("20240527110328_TourandTourLogslastMigration2705")]
    partial class TourandTourLogslastMigration2705
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.DTOs.TourDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildFriendliness")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Directions")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<string>("Distance")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EndLocation")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EstimatedTime")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Popularity")
                        .HasColumnType("integer");

                    b.Property<string>("StartLocation")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TransportType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("DataAccessLayer.DTOs.TourLogsDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<string>("Distance")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("TotalTime")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TourId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("TourLogs");
                });

            modelBuilder.Entity("DataAccessLayer.DTOs.TourLogsDTO", b =>
                {
                    b.HasOne("DataAccessLayer.DTOs.TourDTO", "Tour")
                        .WithMany("TourLogsList")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("DataAccessLayer.DTOs.TourDTO", b =>
                {
                    b.Navigation("TourLogsList");
                });
#pragma warning restore 612, 618
        }
    }
}