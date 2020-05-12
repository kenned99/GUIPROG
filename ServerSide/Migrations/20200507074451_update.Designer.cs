﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerSide;

namespace ServerSide.Migrations
{
    [DbContext(typeof(MembersDBContext))]
    [Migration("20200507074451_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.3.20181.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServerSide.Model.GpsLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Accuracy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Longtitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("GpsLocation");
                });

            modelBuilder.Entity("ServerSide.Model.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LastKnownLocationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<byte[]>("salt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("LastKnownLocationId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("ServerSide.Model.Member", b =>
                {
                    b.HasOne("ServerSide.Model.GpsLocation", "LastKnownLocation")
                        .WithMany()
                        .HasForeignKey("LastKnownLocationId");
                });
#pragma warning restore 612, 618
        }
    }
}