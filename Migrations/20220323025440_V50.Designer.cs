﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace nesto.Migrations
{
    [DbContext(typeof(FakultetContext))]
    [Migration("20220323025440_V50")]
    partial class V50
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdministratorAnketa", b =>
                {
                    b.Property<int>("AdminAnetaID")
                        .HasColumnType("int");

                    b.Property<int>("AnketaAdministratorID")
                        .HasColumnType("int");

                    b.HasKey("AdminAnetaID", "AnketaAdministratorID");

                    b.HasIndex("AnketaAdministratorID");

                    b.ToTable("AdministratorAnketa");
                });

            modelBuilder.Entity("AnketaFakultet", b =>
                {
                    b.Property<int>("AnketaFakultetiID")
                        .HasColumnType("int");

                    b.Property<int>("FakultetAnketeID")
                        .HasColumnType("int");

                    b.HasKey("AnketaFakultetiID", "FakultetAnketeID");

                    b.HasIndex("FakultetAnketeID");

                    b.ToTable("AnketaFakultet");
                });

            modelBuilder.Entity("Models.Administrator", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Korisnicko_ime")
                        .IsRequired()
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.HasKey("ID");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("Models.Anketa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Entitet")
                        .HasColumnType("int");

                    b.Property<string>("Info")
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.Property<string>("Link")
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.Property<string>("Mail")
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.Property<string>("Naziv")
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.Property<string>("Telefon")
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.HasKey("ID");

                    b.ToTable("Anketa");
                });

            modelBuilder.Entity("Models.Fakultet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Info")
                        .HasMaxLength(666)
                        .HasColumnType("nvarchar(666)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int?>("administratorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("administratorID");

                    b.ToTable("Fakultet");
                });

            modelBuilder.Entity("Models.Odgovor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Komentar")
                        .HasMaxLength(666)
                        .HasColumnType("nvarchar(666)");

                    b.Property<string>("Tekst_odgovora")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("pitanjeID")
                        .HasColumnType("int");

                    b.Property<int?>("studentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("pitanjeID");

                    b.HasIndex("studentID");

                    b.ToTable("Odgovor");
                });

            modelBuilder.Entity("Models.Pitanje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Moguci_odgovori")
                        .HasMaxLength(666)
                        .HasColumnType("nvarchar(666)");

                    b.Property<string>("Tekst_pitanja")
                        .IsRequired()
                        .HasMaxLength(666)
                        .HasColumnType("nvarchar(666)");

                    b.Property<int?>("anketaID")
                        .HasColumnType("int");

                    b.Property<int>("tip_pitanja")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("anketaID");

                    b.ToTable("Pitanje");
                });

            modelBuilder.Entity("Models.Popunjavanje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Popunjena")
                        .HasColumnType("bit");

                    b.Property<int?>("anketaID")
                        .HasColumnType("int");

                    b.Property<int?>("studentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("anketaID");

                    b.HasIndex("studentID");

                    b.ToTable("Popunjavanje");
                });

            modelBuilder.Entity("Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("AdministratorAnketa", b =>
                {
                    b.HasOne("Models.Anketa", null)
                        .WithMany()
                        .HasForeignKey("AdminAnetaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Administrator", null)
                        .WithMany()
                        .HasForeignKey("AnketaAdministratorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnketaFakultet", b =>
                {
                    b.HasOne("Models.Fakultet", null)
                        .WithMany()
                        .HasForeignKey("AnketaFakultetiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Anketa", null)
                        .WithMany()
                        .HasForeignKey("FakultetAnketeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Fakultet", b =>
                {
                    b.HasOne("Models.Administrator", "administrator")
                        .WithMany("AdminFakultet")
                        .HasForeignKey("administratorID");

                    b.Navigation("administrator");
                });

            modelBuilder.Entity("Models.Odgovor", b =>
                {
                    b.HasOne("Models.Pitanje", "pitanje")
                        .WithMany("PitanjeOdgovori")
                        .HasForeignKey("pitanjeID");

                    b.HasOne("Models.Student", "student")
                        .WithMany("StudentOdgovori")
                        .HasForeignKey("studentID");

                    b.Navigation("pitanje");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Models.Pitanje", b =>
                {
                    b.HasOne("Models.Anketa", "anketa")
                        .WithMany("AnketaPitanja")
                        .HasForeignKey("anketaID");

                    b.Navigation("anketa");
                });

            modelBuilder.Entity("Models.Popunjavanje", b =>
                {
                    b.HasOne("Models.Anketa", "anketa")
                        .WithMany("AnketaPopunjavanje")
                        .HasForeignKey("anketaID");

                    b.HasOne("Models.Student", "student")
                        .WithMany("StudentPopunjavanje")
                        .HasForeignKey("studentID");

                    b.Navigation("anketa");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Models.Administrator", b =>
                {
                    b.Navigation("AdminFakultet");
                });

            modelBuilder.Entity("Models.Anketa", b =>
                {
                    b.Navigation("AnketaPitanja");

                    b.Navigation("AnketaPopunjavanje");
                });

            modelBuilder.Entity("Models.Pitanje", b =>
                {
                    b.Navigation("PitanjeOdgovori");
                });

            modelBuilder.Entity("Models.Student", b =>
                {
                    b.Navigation("StudentOdgovori");

                    b.Navigation("StudentPopunjavanje");
                });
#pragma warning restore 612, 618
        }
    }
}
