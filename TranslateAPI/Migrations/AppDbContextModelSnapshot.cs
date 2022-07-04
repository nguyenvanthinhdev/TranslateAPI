﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TranslateAPI.ConText;

#nullable disable

namespace TranslateAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TranslateAPI.Entities.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"), 1L, 1);

                    b.Property<int?>("Active")
                        .HasColumnType("int");

                    b.Property<string>("AddressIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTimeIP")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NumberOfUsers")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfUses")
                        .HasColumnType("int");

                    b.HasKey("AddressID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("TranslateAPI.Entities.Manager", b =>
                {
                    b.Property<int>("ManagerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerID"), 1L, 1);

                    b.Property<int>("NumberIpSystem")
                        .HasColumnType("int");

                    b.Property<double>("NumberOfCoinSystem")
                        .HasColumnType("float");

                    b.Property<double>("NumberOfRemainingCoin")
                        .HasColumnType("float");

                    b.Property<double>("NumberOfUsedCoin")
                        .HasColumnType("float");

                    b.Property<int>("NumberOfUsersSystem")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfUsesSystem")
                        .HasColumnType("int");

                    b.HasKey("ManagerID");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("TranslateAPI.Entities.Translate", b =>
                {
                    b.Property<int>("TranslateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TranslateID"), 1L, 1);

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeTranslates")
                        .HasColumnType("datetime2");

                    b.Property<string>("TranslateCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("inpLanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("outLanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TranslateID");

                    b.HasIndex("UserID");

                    b.ToTable("Translates");
                });

            modelBuilder.Entity("TranslateAPI.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<int?>("Active")
                        .HasColumnType("int");

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<double>("Coin")
                        .HasColumnType("float");

                    b.Property<DateTime?>("CreateTimeUser")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NumberOfuses")
                        .HasColumnType("int");

                    b.Property<int>("PypeUser")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserID");

                    b.HasIndex("AddressID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TranslateAPI.Entities.Translate", b =>
                {
                    b.HasOne("TranslateAPI.Entities.User", "User")
                        .WithMany("Translates")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TranslateAPI.Entities.User", b =>
                {
                    b.HasOne("TranslateAPI.Entities.Address", "Address")
                        .WithMany("Users")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("TranslateAPI.Entities.Address", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TranslateAPI.Entities.User", b =>
                {
                    b.Navigation("Translates");
                });
#pragma warning restore 612, 618
        }
    }
}