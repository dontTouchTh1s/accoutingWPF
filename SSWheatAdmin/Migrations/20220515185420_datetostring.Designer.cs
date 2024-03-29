﻿// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSWheatAdmin.DataBase.DbContexts;

namespace SSWheatAdmin.Migrations
{
    [DbContext(typeof(InvestmentFundDbContext))]
    [Migration("20220515185420_datetostring")]
    partial class datetostring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.16");

            modelBuilder.Entity("SSWheatAdmin.DTOs.AccountDTO", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreateDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Credit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OwnerNationalId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.HasIndex("OwnerNationalId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SSWheatAdmin.DTOs.PeopleDTO", b =>
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

            modelBuilder.Entity("SSWheatAdmin.DTOs.AccountDTO", b =>
                {
                    b.HasOne("SSWheatAdmin.DTOs.PeopleDTO", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerNationalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SSWheatAdmin.DTOs.PeopleDTO", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
