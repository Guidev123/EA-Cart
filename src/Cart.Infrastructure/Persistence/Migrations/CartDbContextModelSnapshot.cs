﻿// <auto-generated />
using System;
using Cart.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cart.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(CartDbContext))]
    partial class CartDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cart.Core.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("VARCHAR(160)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(160)");

                    b.Property<decimal>("Price")
                        .HasColumnType("MONEY");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems", (string)null);
                });

            modelBuilder.Entity("Cart.Core.Entities.CustomerCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Discount")
                        .HasColumnType("MONEY");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("VoucherIsUsed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Carts", (string)null);
                });

            modelBuilder.Entity("Cart.Core.Entities.CartItem", b =>
                {
                    b.HasOne("Cart.Core.Entities.CustomerCart", "CustomerCart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerCart");
                });

            modelBuilder.Entity("Cart.Core.Entities.CustomerCart", b =>
                {
                    b.OwnsOne("Cart.Core.ValueObjects.Voucher", "Voucher", b1 =>
                        {
                            b1.Property<Guid>("CustomerCartId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("VARCHAR(160)")
                                .HasColumnName("Code");

                            b1.Property<int>("DiscountType")
                                .HasColumnType("int")
                                .HasColumnName("DiscountType");

                            b1.Property<decimal?>("DiscountValue")
                                .HasColumnType("MONEY")
                                .HasColumnName("DiscountValue");

                            b1.Property<decimal?>("Percentual")
                                .HasColumnType("MONEY")
                                .HasColumnName("Percentual");

                            b1.HasKey("CustomerCartId");

                            b1.ToTable("Carts");

                            b1.WithOwner()
                                .HasForeignKey("CustomerCartId");
                        });

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Cart.Core.Entities.CustomerCart", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}
