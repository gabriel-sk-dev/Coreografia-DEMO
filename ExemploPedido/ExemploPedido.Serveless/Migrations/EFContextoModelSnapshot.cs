﻿// <auto-generated />
using System;
using ExemploPedido.Serveless.Dominio.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExemploPedido.Serveless.Migrations
{
    [DbContext(typeof(EFContexto))]
    partial class EFContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExemploPedido.Serveless.Dominio.Boleto", b =>
                {
                    b.Property<string>("NossoNumero")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClienteId");

                    b.Property<string>("PedidoId");

                    b.Property<decimal>("Valor");

                    b.Property<DateTime>("Vencimento");

                    b.HasKey("NossoNumero");

                    b.HasIndex("PedidoId");

                    b.ToTable("Boletos");
                });

            modelBuilder.Entity("ExemploPedido.Serveless.Dominio.Pedido", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClienteId");

                    b.Property<DateTime>("CriadoEm");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ExemploPedido.Serveless.Dominio.Pedido+Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PedidoId");

                    b.Property<Guid>("PrdutoId");

                    b.Property<int>("Quantidade");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoItens");
                });

            modelBuilder.Entity("ExemploPedido.Serveless.Dominio.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PedidoId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("ExemploPedido.Serveless.Dominio.Boleto", b =>
                {
                    b.HasOne("ExemploPedido.Serveless.Dominio.Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("ExemploPedido.Serveless.Dominio.Pedido+Item", b =>
                {
                    b.HasOne("ExemploPedido.Serveless.Dominio.Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExemploPedido.Serveless.Dominio.Transacao", b =>
                {
                    b.HasOne("ExemploPedido.Serveless.Dominio.Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId");
                });
#pragma warning restore 612, 618
        }
    }
}
