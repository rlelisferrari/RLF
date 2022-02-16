﻿// <auto-generated />
using System;
using DATA.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DATA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DOMAIN.Models.Atleta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Atletas");
                });

            modelBuilder.Entity("DOMAIN.Models.Equipamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipamentos");
                });

            modelBuilder.Entity("DOMAIN.Models.EquipamentoTipoEquipamento", b =>
                {
                    b.Property<int>("EquipamentoId")
                        .HasColumnType("int");

                    b.Property<int>("TipoEquipamentoId")
                        .HasColumnType("int");

                    b.HasKey("EquipamentoId", "TipoEquipamentoId");

                    b.HasIndex("TipoEquipamentoId");

                    b.ToTable("EquipamentoTipoEquipamento");
                });

            modelBuilder.Entity("DOMAIN.Models.Jogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adversario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Jogos");
                });

            modelBuilder.Entity("DOMAIN.Models.Ordem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TipoOrdemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoOrdemId");

                    b.ToTable("Ordens");
                });

            modelBuilder.Entity("DOMAIN.Models.ScoutGeral", b =>
                {
                    b.Property<int>("idAtleta")
                        .HasColumnType("int");

                    b.Property<int>("idJogo")
                        .HasColumnType("int");

                    b.Property<int>("assistencia")
                        .HasColumnType("int");

                    b.Property<int>("cartaAmarelo")
                        .HasColumnType("int");

                    b.Property<int>("cartaVermelho")
                        .HasColumnType("int");

                    b.Property<int>("gol")
                        .HasColumnType("int");

                    b.HasKey("idAtleta", "idJogo");

                    b.HasIndex("idJogo");

                    b.ToTable("ScoutsGerais");
                });

            modelBuilder.Entity("DOMAIN.Models.TipoEquipamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoEquipamento");
                });

            modelBuilder.Entity("DOMAIN.Models.TipoOrdem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoOrdens");
                });

            modelBuilder.Entity("DOMAIN.Models.VeiculoOlx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("Cambio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataPublicacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direcao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Km")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Potencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("VeiculosOlx");
                });

            modelBuilder.Entity("DOMAIN.Models.EquipamentoTipoEquipamento", b =>
                {
                    b.HasOne("DOMAIN.Models.Equipamento", "Equipamento")
                        .WithMany("EquipamentoTipoEquipamento")
                        .HasForeignKey("EquipamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DOMAIN.Models.TipoEquipamento", "TipoEquipamento")
                        .WithMany("EquipamentoTipoEquipamento")
                        .HasForeignKey("TipoEquipamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DOMAIN.Models.Ordem", b =>
                {
                    b.HasOne("DOMAIN.Models.TipoOrdem", "TipoOrdem")
                        .WithMany()
                        .HasForeignKey("TipoOrdemId");
                });

            modelBuilder.Entity("DOMAIN.Models.ScoutGeral", b =>
                {
                    b.HasOne("DOMAIN.Models.Atleta", "atleta")
                        .WithMany("ScoutGeral")
                        .HasForeignKey("idAtleta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DOMAIN.Models.Jogo", "jogo")
                        .WithMany("ScoutGeral")
                        .HasForeignKey("idJogo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
