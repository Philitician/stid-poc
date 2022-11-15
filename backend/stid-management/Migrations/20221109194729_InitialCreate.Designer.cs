﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using stid_management.Data;

#nullable disable

namespace stidmanagement.Migrations
{
    [DbContext(typeof(StidContext))]
    [Migration("20221109194729_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("stid_management.Models.AppRegistration", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("TEXT");

                    b.HasKey("ClientId");

                    b.ToTable("AppRegistrations");
                });

            modelBuilder.Entity("stid_management.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stid");
                });
#pragma warning restore 612, 618
        }
    }
}
