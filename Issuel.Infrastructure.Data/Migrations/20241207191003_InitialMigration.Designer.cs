﻿// <auto-generated />
using System;
using Issuel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Issuel.Infrastructure.Data.Migrations
{
    [DbContext(typeof(IssueDbContext))]
    [Migration("20241207191003_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "priority", new[] { "undefined", "red", "gray", "green" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "status", new[] { "undefined", "to_do", "in_progress", "done" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Issuel.Domain.Entities.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Deadline");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<TimeSpan?>("Estimate")
                        .HasColumnType("interval")
                        .HasColumnName("Estimate");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("Priority");

                    b.Property<TimeSpan?>("Spent")
                        .HasColumnType("interval")
                        .HasColumnName("Spent");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Issue", (string)null);
                });

            modelBuilder.Entity("Issuel.Domain.Entities.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("Label", (string)null);
                });

            modelBuilder.Entity("Issuel.Domain.Entities.Label", b =>
                {
                    b.HasOne("Issuel.Domain.Entities.Issue", null)
                        .WithMany("Labels")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Issuel.Domain.Entities.Issue", b =>
                {
                    b.Navigation("Labels");
                });
#pragma warning restore 612, 618
        }
    }
}