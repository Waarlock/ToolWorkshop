﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToolWorkshop.Data;

#nullable disable

namespace ToolWorkshop.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220601080518_fix")]
    partial class fix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Catalog", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("PlanogramId")
                        .HasColumnType("int");

                    b.Property<int>("SKU")
                        .HasColumnType("int");

                    b.Property<int>("ToolId")
                        .HasColumnType("int");

                    b.Property<Guid>("ToolImageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("PlanogramId");

                    b.HasIndex("ToolId");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.HasIndex("Name", "StateId")
                        .IsUnique()
                        .HasFilter("[StateId] IS NOT NULL");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Movement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("End_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start_DateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Movements");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Movement_Detail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Catalogid")
                        .HasColumnType("int");

                    b.Property<int?>("MovementId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Retun_Remarks")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("Temporal_MovementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Catalogid");

                    b.HasIndex("MovementId");

                    b.HasIndex("Temporal_MovementId");

                    b.ToTable("Movement_Details");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Planogram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Planograms");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name", "CountryId")
                        .IsUnique()
                        .HasFilter("[CountryId] IS NOT NULL");

                    b.ToTable("States");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Temporal_Movement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("End_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ToolId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.HasIndex("UserId");

                    b.ToTable("Temporal_Movements");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("EAN")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<float>("Stock")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.ToolCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ToolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ToolId", "CategoryId")
                        .IsUnique()
                        .HasFilter("[ToolId] IS NOT NULL AND [CategoryId] IS NOT NULL");

                    b.ToTable("ToolCategories");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.ToolImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ToolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.ToTable("ToolImages");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolWorkshop.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Catalog", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.Planogram", "Planogram")
                        .WithMany("Catalogs")
                        .HasForeignKey("PlanogramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolWorkshop.Data.Entities.Tool", "Tool")
                        .WithMany()
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Planogram");

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.City", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId");

                    b.Navigation("State");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Movement_Detail", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.Catalog", "Catalog")
                        .WithMany("MovementDetails")
                        .HasForeignKey("Catalogid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolWorkshop.Data.Entities.Movement", "Movement")
                        .WithMany("Details")
                        .HasForeignKey("MovementId");

                    b.HasOne("ToolWorkshop.Data.Entities.Temporal_Movement", "Temporal_Movement")
                        .WithMany("Details")
                        .HasForeignKey("Temporal_MovementId");

                    b.Navigation("Catalog");

                    b.Navigation("Movement");

                    b.Navigation("Temporal_Movement");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Planogram", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.Warehouse", "Warehouse")
                        .WithMany("Planograms")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.State", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Temporal_Movement", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.Tool", "Tool")
                        .WithMany()
                        .HasForeignKey("ToolId");

                    b.HasOne("ToolWorkshop.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Tool");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.ToolCategory", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.Category", "Category")
                        .WithMany("ToolCategories")
                        .HasForeignKey("CategoryId");

                    b.HasOne("ToolWorkshop.Data.Entities.Tool", "Tool")
                        .WithMany("ToolCategories")
                        .HasForeignKey("ToolId");

                    b.Navigation("Category");

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.ToolImage", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.Tool", "Tool")
                        .WithMany("ToolImages")
                        .HasForeignKey("ToolId");

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.User", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Warehouse", b =>
                {
                    b.HasOne("ToolWorkshop.Data.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Catalog", b =>
                {
                    b.Navigation("MovementDetails");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Category", b =>
                {
                    b.Navigation("ToolCategories");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.City", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Movement", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Planogram", b =>
                {
                    b.Navigation("Catalogs");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.State", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Temporal_Movement", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Tool", b =>
                {
                    b.Navigation("ToolCategories");

                    b.Navigation("ToolImages");
                });

            modelBuilder.Entity("ToolWorkshop.Data.Entities.Warehouse", b =>
                {
                    b.Navigation("Planograms");
                });
#pragma warning restore 612, 618
        }
    }
}