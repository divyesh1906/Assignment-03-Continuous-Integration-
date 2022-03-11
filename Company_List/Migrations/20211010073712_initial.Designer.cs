﻿// <auto-generated />
using System;
using Company_List.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Company_List.Migrations
{
    [DbContext(typeof(TransactionContext))]
    [Migration("20211010073712_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Company_List.Models.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TickerSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyID");

                    b.ToTable("Company");

                    b.HasData(
                        new
                        {
                            CompanyID = 1,
                            Address = "41 Hamilton Street",
                            CompanyName = "Alexa",
                            TickerSymbol = "AlX"
                        },
                        new
                        {
                            CompanyID = 2,
                            Address = "141 Toronto Street",
                            CompanyName = "Google",
                            TickerSymbol = "GG"
                        },
                        new
                        {
                            CompanyID = 3,
                            Address = "321 Microsoft Street",
                            CompanyName = "Microsoft",
                            TickerSymbol = "MSFT"
                        });
                });

            modelBuilder.Entity("Company_List.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double>("SharePrice")
                        .HasColumnType("float");

                    b.Property<string>("TransactionTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TransactionId");

                    b.HasIndex("CompanyID");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            TransactionId = 1,
                            CompanyID = 1,
                            Quantity = 100,
                            SharePrice = 500.0,
                            TransactionTypeId = "S"
                        },
                        new
                        {
                            TransactionId = 2,
                            CompanyID = 2,
                            Quantity = 100,
                            SharePrice = 500.0,
                            TransactionTypeId = "B"
                        },
                        new
                        {
                            TransactionId = 3,
                            CompanyID = 3,
                            Quantity = 100,
                            SharePrice = 500.0,
                            TransactionTypeId = "B"
                        });
                });

            modelBuilder.Entity("Company_List.Models.TransactionType", b =>
                {
                    b.Property<string>("TransactionTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("CommissionFee")
                        .HasColumnType("float");

                    b.Property<string>("TransactionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionTypeId");

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            TransactionTypeId = "S",
                            CommissionFee = 5.9900000000000002,
                            TransactionName = "Sell"
                        },
                        new
                        {
                            TransactionTypeId = "B",
                            CommissionFee = 5.2000000000000002,
                            TransactionName = "Buy"
                        });
                });

            modelBuilder.Entity("Company_List.Models.Transaction", b =>
                {
                    b.HasOne("Company_List.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Company_List.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId");

                    b.Navigation("Company");

                    b.Navigation("TransactionType");
                });
#pragma warning restore 612, 618
        }
    }
}
