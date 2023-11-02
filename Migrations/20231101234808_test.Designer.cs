﻿// <auto-generated />
using System;
using BioLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BioLab.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20231101234808_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BioLab.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CurrencyUnit")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CurrencyId");

                    b.ToTable("Currencys");
                });

            modelBuilder.Entity("BioLab.Models.Nafta", b =>
                {
                    b.Property<int>("NaftaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BlereShiturSelect")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Cmimi")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Leke")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Litra")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("RrugaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("NaftaId");

                    b.HasIndex("RrugaId")
                        .IsUnique();

                    b.ToTable("Naftas");
                });

            modelBuilder.Entity("BioLab.Models.NaftaRruga", b =>
                {
                    b.Property<int>("NaftaRrugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("NaftaId")
                        .HasColumnType("int");

                    b.Property<int>("RrugaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("NaftaRrugaId");

                    b.HasIndex("NaftaId");

                    b.HasIndex("RrugaId");

                    b.ToTable("NaftaRrugas");
                });

            modelBuilder.Entity("BioLab.Models.PagesaDogana", b =>
                {
                    b.Property<int>("PagesaDoganaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("PagesaKryer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RrugaId")
                        .HasColumnType("int");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PagesaDoganaId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("RrugaId");

                    b.ToTable("PagesaDoganas");
                });

            modelBuilder.Entity("BioLab.Models.PagesaNafta", b =>
                {
                    b.Property<int>("PagesaNaftaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("NaftaRrugaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("PagesaKryer")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PagesaNaftaId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("NaftaRrugaId");

                    b.ToTable("PagesaNafta");
                });

            modelBuilder.Entity("BioLab.Models.PagesaPikaShkarkimit", b =>
                {
                    b.Property<int>("PagesaPikaShkarkimitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("PagesaKryer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PikaShkarkimiId")
                        .HasColumnType("int");

                    b.Property<int?>("RrugaId")
                        .HasColumnType("int");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PagesaPikaShkarkimitId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PikaShkarkimiId");

                    b.HasIndex("RrugaId");

                    b.ToTable("PagesaPikaShkarkimits");
                });

            modelBuilder.Entity("BioLab.Models.PagesaShoferit", b =>
                {
                    b.Property<int>("PagesaShoferitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("PagesaKryer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ShoferRrugaId")
                        .HasColumnType("int");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PagesaShoferitId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ShoferRrugaId");

                    b.ToTable("PagesaShoferits");
                });

            modelBuilder.Entity("BioLab.Models.PikaRruga", b =>
                {
                    b.Property<int>("PikaRrugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PikaShkarkimiId")
                        .HasColumnType("int");

                    b.Property<int>("RrugaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PikaRrugaId");

                    b.HasIndex("PikaShkarkimiId");

                    b.HasIndex("RrugaId");

                    b.ToTable("PikaRrugas");
                });

            modelBuilder.Entity("BioLab.Models.PikaRrugaPagesa", b =>
                {
                    b.Property<int>("PikaRrugaPagesaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("PagesaKryer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PikaRrugaId")
                        .HasColumnType("int");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PikaRrugaPagesaId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PikaRrugaId");

                    b.ToTable("PikaRrugaPagesas");
                });

            modelBuilder.Entity("BioLab.Models.PikaShkarkimi", b =>
                {
                    b.Property<int>("PikaShkarkimiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Model")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PikaShkarkimiId");

                    b.ToTable("PikaShkarkimis");
                });

            modelBuilder.Entity("BioLab.Models.Rruga", b =>
                {
                    b.Property<int>("RrugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Dogana")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Fitime")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("FitimeEkstra")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("Model")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("NaftaPerTuShiturLitra")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("NaftaShpenzuarLitra")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PagesaShoferit")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Shpenzime")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Xhiro")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("shenime")
                        .HasColumnType("longtext");

                    b.Property<decimal>("shpenzimeEkstra")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("RrugaId");

                    b.ToTable("Rrugas");
                });

            modelBuilder.Entity("BioLab.Models.RrugaFitime", b =>
                {
                    b.Property<int>("RrugaFitimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("RrugaId")
                        .HasColumnType("int");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RrugaFitimeId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("RrugaId");

                    b.ToTable("RrugaFitimes");
                });

            modelBuilder.Entity("BioLab.Models.RrugaFitimeEkstra", b =>
                {
                    b.Property<int>("RrugaFitimeEkstraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("PagesaKryer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RrugaId")
                        .HasColumnType("int");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RrugaFitimeEkstraId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("RrugaId");

                    b.ToTable("RrugaFitimeEkstras");
                });

            modelBuilder.Entity("BioLab.Models.RrugaShpenzimeEkstra", b =>
                {
                    b.Property<int>("RrugaShpenzimeEkstraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("PagesaKryer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RrugaId")
                        .HasColumnType("int");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RrugaShpenzimeEkstraId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("RrugaId");

                    b.ToTable("RrugaShpenzimeEkstras");
                });

            modelBuilder.Entity("BioLab.Models.Shofer", b =>
                {
                    b.Property<int>("ShoferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Model")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ShoferId");

                    b.ToTable("Shofers");
                });

            modelBuilder.Entity("BioLab.Models.ShoferRruga", b =>
                {
                    b.Property<int>("ShoferRrugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RrugaId")
                        .HasColumnType("int");

                    b.Property<int>("ShoferId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ShoferRrugaId");

                    b.HasIndex("RrugaId");

                    b.HasIndex("ShoferId");

                    b.ToTable("ShoferRrugas");
                });

            modelBuilder.Entity("BioLab.Models.ZbritShtoGjendja", b =>
                {
                    b.Property<int>("ZbritShtoGjendjaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pagesa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Shenime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ShpenzimXhiro")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ZbritShtoGjendjaId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("ZbritShtoGjendjas");
                });

            modelBuilder.Entity("BioLab.Models.Nafta", b =>
                {
                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithOne("Nafta")
                        .HasForeignKey("BioLab.Models.Nafta", "RrugaId");

                    b.Navigation("Rruga");
                });

            modelBuilder.Entity("BioLab.Models.NaftaRruga", b =>
                {
                    b.HasOne("BioLab.Models.Nafta", "Nafta")
                        .WithMany("NaftaRrugas")
                        .HasForeignKey("NaftaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithMany("NaftaRrugas")
                        .HasForeignKey("RrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nafta");

                    b.Navigation("Rruga");
                });

            modelBuilder.Entity("BioLab.Models.PagesaDogana", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("PagesaDoganas")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithMany("PagesaDoganas")
                        .HasForeignKey("RrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Rruga");
                });

            modelBuilder.Entity("BioLab.Models.PagesaNafta", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("PagesaNaftas")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.NaftaRruga", "NaftaRruga")
                        .WithMany()
                        .HasForeignKey("NaftaRrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("NaftaRruga");
                });

            modelBuilder.Entity("BioLab.Models.PagesaPikaShkarkimit", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("PagesaPikaShkarkimits")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.PikaShkarkimi", "Pika")
                        .WithMany("PagesaPikaShkarkimits")
                        .HasForeignKey("PikaShkarkimiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Rruga", null)
                        .WithMany("PagesaPikaShkarkimits")
                        .HasForeignKey("RrugaId");

                    b.Navigation("Currency");

                    b.Navigation("Pika");
                });

            modelBuilder.Entity("BioLab.Models.PagesaShoferit", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("PagesaShoferits")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.ShoferRruga", "Shofer")
                        .WithMany("PagesaShoferits")
                        .HasForeignKey("ShoferRrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Shofer");
                });

            modelBuilder.Entity("BioLab.Models.PikaRruga", b =>
                {
                    b.HasOne("BioLab.Models.PikaShkarkimi", "PikaShkarkimi")
                        .WithMany("PikaRrugas")
                        .HasForeignKey("PikaShkarkimiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithMany("PikaRrugas")
                        .HasForeignKey("RrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PikaShkarkimi");

                    b.Navigation("Rruga");
                });

            modelBuilder.Entity("BioLab.Models.PikaRrugaPagesa", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("PikaRrugaPagesa")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.PikaRruga", "Pika")
                        .WithMany("PikaRrugaPagesa")
                        .HasForeignKey("PikaRrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Pika");
                });

            modelBuilder.Entity("BioLab.Models.RrugaFitime", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("RrugaFitimes")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithMany("RrugaFitimes")
                        .HasForeignKey("RrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Rruga");
                });

            modelBuilder.Entity("BioLab.Models.RrugaFitimeEkstra", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("RrugaFitimeEkstras")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithMany("RrugaFitimeEkstras")
                        .HasForeignKey("RrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Rruga");
                });

            modelBuilder.Entity("BioLab.Models.RrugaShpenzimeEkstra", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany("RrugaShpenzimeEkstras")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithMany("RrugaShpenzimeEkstras")
                        .HasForeignKey("RrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Rruga");
                });

            modelBuilder.Entity("BioLab.Models.ShoferRruga", b =>
                {
                    b.HasOne("BioLab.Models.Rruga", "Rruga")
                        .WithMany("ShoferRrugas")
                        .HasForeignKey("RrugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BioLab.Models.Shofer", "Shofer")
                        .WithMany("shoferRrugas")
                        .HasForeignKey("ShoferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rruga");

                    b.Navigation("Shofer");
                });

            modelBuilder.Entity("BioLab.Models.ZbritShtoGjendja", b =>
                {
                    b.HasOne("BioLab.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("BioLab.Models.Currency", b =>
                {
                    b.Navigation("PagesaDoganas");

                    b.Navigation("PagesaNaftas");

                    b.Navigation("PagesaPikaShkarkimits");

                    b.Navigation("PagesaShoferits");

                    b.Navigation("PikaRrugaPagesa");

                    b.Navigation("RrugaFitimeEkstras");

                    b.Navigation("RrugaFitimes");

                    b.Navigation("RrugaShpenzimeEkstras");
                });

            modelBuilder.Entity("BioLab.Models.Nafta", b =>
                {
                    b.Navigation("NaftaRrugas");
                });

            modelBuilder.Entity("BioLab.Models.PikaRruga", b =>
                {
                    b.Navigation("PikaRrugaPagesa");
                });

            modelBuilder.Entity("BioLab.Models.PikaShkarkimi", b =>
                {
                    b.Navigation("PagesaPikaShkarkimits");

                    b.Navigation("PikaRrugas");
                });

            modelBuilder.Entity("BioLab.Models.Rruga", b =>
                {
                    b.Navigation("Nafta");

                    b.Navigation("NaftaRrugas");

                    b.Navigation("PagesaDoganas");

                    b.Navigation("PagesaPikaShkarkimits");

                    b.Navigation("PikaRrugas");

                    b.Navigation("RrugaFitimeEkstras");

                    b.Navigation("RrugaFitimes");

                    b.Navigation("RrugaShpenzimeEkstras");

                    b.Navigation("ShoferRrugas");
                });

            modelBuilder.Entity("BioLab.Models.Shofer", b =>
                {
                    b.Navigation("shoferRrugas");
                });

            modelBuilder.Entity("BioLab.Models.ShoferRruga", b =>
                {
                    b.Navigation("PagesaShoferits");
                });
#pragma warning restore 612, 618
        }
    }
}
