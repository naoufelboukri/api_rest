﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using quest_web;

#nullable disable

namespace quest_web.Migrations
{
    [DbContext(typeof(APIDbContext))]
    partial class APIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("quest_web.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("creation_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("updated_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
