﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductStore.Shops.Shops.DAL;

#nullable disable

namespace ProductStore.Shops.Migrations
{
    [DbContext(typeof(ShopsContext))]
    [Migration("20230627142955_manytomanyfirst")]
    partial class manytomanyfirst
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels.ProductsWithTypes", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "TypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("products_types", (string)null);
                });

            modelBuilder.Entity("ProductStore.Shops.Shops.Domain.Domain.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("ProductStore.Shops.Shops.Domain.Domain.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("product_types", (string)null);
                });

            modelBuilder.Entity("ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels.ProductsWithTypes", b =>
                {
                    b.HasOne("ProductStore.Shops.Shops.Domain.Domain.Models.Product", "Product")
                        .WithMany("ProductsWithTypes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductStore.Shops.Shops.Domain.Domain.Models.ProductType", "ProductType")
                        .WithMany("ProductsWithTypes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("ProductStore.Shops.Shops.Domain.Domain.Models.Product", b =>
                {
                    b.Navigation("ProductsWithTypes");
                });

            modelBuilder.Entity("ProductStore.Shops.Shops.Domain.Domain.Models.ProductType", b =>
                {
                    b.Navigation("ProductsWithTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
