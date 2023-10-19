﻿// <auto-generated />
using System;
using ItemManagment.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ItemManagment.Migrations
{
    [DbContext(typeof(ItemDbContext))]
    [Migration("20230813053751_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ItemManagment.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("InternalCode")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ItemGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("MeasureId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PackageQuantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<int>("TaxType")
                        .HasColumnType("int");

                    b.Property<int?>("UnionBarcodeId")
                        .HasColumnType("int");

                    b.Property<decimal>("WholePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WholeQuantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemGroupId");

                    b.HasIndex("MeasureId");

                    b.HasIndex("UnionBarcodeId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BarCode = "1234567890",
                            Cost = 2000m,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InternalCode = 1,
                            IsDeleted = false,
                            ItemGroupId = 1,
                            MeasureId = 1,
                            Name = "Талх Атар,180гр",
                            PackageQuantity = 30m,
                            Price = 2400m,
                            ShopId = 1,
                            TaxType = 2,
                            WholePrice = 2200m,
                            WholeQuantity = 10m
                        },
                        new
                        {
                            Id = 2,
                            BarCode = "1233445566",
                            Cost = 2500m,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InternalCode = 2,
                            IsDeleted = false,
                            ItemGroupId = 1,
                            MeasureId = 1,
                            Name = "Коко кола, Бидний, 300мг",
                            PackageQuantity = 50m,
                            Price = 2800m,
                            ShopId = 1,
                            TaxType = 2,
                            WholePrice = 2600m,
                            WholeQuantity = 30m
                        },
                        new
                        {
                            Id = 3,
                            BarCode = "1234455667",
                            Cost = 2400m,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InternalCode = 3,
                            IsDeleted = false,
                            ItemGroupId = 1,
                            MeasureId = 1,
                            Name = "Сэнгүр лаазтай 0.5л",
                            PackageQuantity = 24m,
                            Price = 3000m,
                            ShopId = 1,
                            TaxType = 2,
                            WholePrice = 2800m,
                            WholeQuantity = 120m
                        });
                });

            modelBuilder.Entity("ItemManagment.Models.ItemGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ItemGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChildGroupId = 0,
                            Code = "001",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Лангуун дээрх бараанууд",
                            IsDeleted = false,
                            Name = "Үндсэн бүлэг",
                            ShopId = 1,
                            Type = 0
                        });
                });

            modelBuilder.Entity("ItemManagment.Models.Measure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Measures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Нэгж Масс хэмжих",
                            IsDeleted = false,
                            Name = "Kilogram",
                            ShopId = 0,
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Нэгж уртаар хэмжих",
                            IsDeleted = false,
                            Name = "Meter",
                            ShopId = 0,
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Нэгж хэсгээр хэмжих",
                            IsDeleted = false,
                            Name = "Piece",
                            ShopId = 0,
                            Type = 0
                        });
                });

            modelBuilder.Entity("ItemManagment.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("ItemManagment.Models.UnionBarcode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMergedItems")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UnionBarcodes");
                });

            modelBuilder.Entity("ItemManagment.Models.Item", b =>
                {
                    b.HasOne("ItemManagment.Models.ItemGroup", "ItemGroup")
                        .WithMany("Items")
                        .HasForeignKey("ItemGroupId");

                    b.HasOne("ItemManagment.Models.Measure", "Measure")
                        .WithMany("Items")
                        .HasForeignKey("MeasureId");

                    b.HasOne("ItemManagment.Models.UnionBarcode", "UnionBarcode")
                        .WithMany("Items")
                        .HasForeignKey("UnionBarcodeId");

                    b.Navigation("ItemGroup");

                    b.Navigation("Measure");

                    b.Navigation("UnionBarcode");
                });

            modelBuilder.Entity("ItemManagment.Models.ItemGroup", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ItemManagment.Models.Measure", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ItemManagment.Models.UnionBarcode", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}