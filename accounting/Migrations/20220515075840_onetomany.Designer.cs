﻿// <auto-generated />
using System;
using accounting.DataBase.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace accounting.Migrations
{
    [DbContext(typeof(InvestmentFundDbContext))]
    [Migration("20220515075840_onetomany")]
    partial class onetomany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.16");

            modelBuilder.Entity("accounting.DTOs.AccountDTO", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Credit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PeopleDTONationalId")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.HasIndex("PeopleDTONationalId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("accounting.DTOs.PeopleDTO", b =>
                {
                    b.Property<string>("NationalId")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonalAccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.HasKey("NationalId");

                    b.ToTable("Peoples");
                });

            modelBuilder.Entity("accounting.DTOs.AccountDTO", b =>
                {
                    b.HasOne("accounting.DTOs.PeopleDTO", null)
                        .WithMany("Accounts")
                        .HasForeignKey("PeopleDTONationalId");
                });

            modelBuilder.Entity("accounting.DTOs.PeopleDTO", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
