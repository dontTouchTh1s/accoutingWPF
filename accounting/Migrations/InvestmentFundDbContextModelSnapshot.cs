﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using accounting.DbContexts;

namespace accounting.Migrations
{
    [DbContext(typeof(InvestmentFundDbContext))]
    partial class InvestmentFundDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.16");

            modelBuilder.Entity("accounting.DTOs.AccountDTO", b =>
                {
                    b.Property<Guid>("accountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Credit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("accountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("accounting.DTOs.PeopleDTO", b =>
                {
                    b.Property<string>("NationalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonalAccountNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("NationalId");

                    b.ToTable("Peoples");
                });
#pragma warning restore 612, 618
        }
    }
}
