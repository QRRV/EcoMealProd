﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(SqlServerDb))]
    partial class SqlServerDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Canteen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HotMeals")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Canteens");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Breda",
                            HotMeals = false,
                            Location = "la"
                        },
                        new
                        {
                            Id = 2,
                            City = "Breda",
                            HotMeals = true,
                            Location = "ld"
                        },
                        new
                        {
                            Id = 3,
                            City = "Tilburg",
                            HotMeals = true,
                            Location = "Ha"
                        },
                        new
                        {
                            Id = 4,
                            City = "Tilburg",
                            HotMeals = true,
                            Location = "Dc"
                        },
                        new
                        {
                            Id = 5,
                            City = "'s-Hertogenbosch",
                            HotMeals = false,
                            Location = "Bk"
                        },
                        new
                        {
                            Id = 6,
                            City = "'s-Hertogenbosch",
                            HotMeals = true,
                            Location = "Lc"
                        });
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EmployeeNumber")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenId = 1,
                            Email = "lucas@gmail.com",
                            EmployeeNumber = 12,
                            FirstName = "Lucas",
                            LastName = "het Kleijn"
                        },
                        new
                        {
                            Id = 2,
                            CanteenId = 2,
                            Email = "hans@gmail.com",
                            EmployeeNumber = 51,
                            FirstName = "Hans Gerard",
                            LastName = "Karremans"
                        },
                        new
                        {
                            Id = 3,
                            CanteenId = 3,
                            Email = "arno@gmail.com",
                            EmployeeNumber = 34,
                            FirstName = "Arno",
                            LastName = "Broeders"
                        },
                        new
                        {
                            Id = 4,
                            CanteenId = 4,
                            Email = "pascal@gmail.com",
                            EmployeeNumber = 123,
                            FirstName = "Pascal",
                            LastName = "van Gastel"
                        },
                        new
                        {
                            Id = 5,
                            CanteenId = 5,
                            Email = "robin@gmail.com",
                            EmployeeNumber = 535,
                            FirstName = "Robin",
                            LastName = "Schellius"
                        },
                        new
                        {
                            Id = 6,
                            CanteenId = 6,
                            Email = "dion@gmail.com",
                            EmployeeNumber = 143,
                            FirstName = "Dion",
                            LastName = "Koeze"
                        });
                });

            modelBuilder.Entity("Domain.MealBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Adult")
                        .HasColumnType("bit");

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PickUpTimeMax")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PickUpTimeMin")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.HasIndex("StudentId");

                    b.ToTable("MealBoxes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adult = true,
                            CanteenId = 1,
                            City = "Breda",
                            Name = "Lucas Meal",
                            PickUpTimeMax = new DateTime(2024, 10, 28, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8833),
                            PickUpTimeMin = new DateTime(2024, 10, 27, 21, 18, 27, 191, DateTimeKind.Local).AddTicks(8801),
                            Price = 52.00m,
                            StudentId = 2,
                            Type = "Alcoholische drank"
                        },
                        new
                        {
                            Id = 2,
                            Adult = true,
                            CanteenId = 2,
                            City = "Breda",
                            Name = "Hans Meal",
                            PickUpTimeMax = new DateTime(2024, 10, 28, 8, 6, 27, 191, DateTimeKind.Local).AddTicks(8838),
                            PickUpTimeMin = new DateTime(2024, 10, 27, 11, 18, 27, 191, DateTimeKind.Local).AddTicks(8836),
                            Price = 200.00m,
                            StudentId = 1,
                            Type = "Luxemaaltijd"
                        },
                        new
                        {
                            Id = 3,
                            Adult = false,
                            CanteenId = 1,
                            City = "Breda",
                            Name = "Jaron Meal",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8842),
                            PickUpTimeMin = new DateTime(2024, 10, 27, 1, 18, 27, 191, DateTimeKind.Local).AddTicks(8840),
                            Price = 1.00m,
                            Type = "Budgetmeal"
                        },
                        new
                        {
                            Id = 4,
                            Adult = false,
                            CanteenId = 2,
                            City = "Breda",
                            Name = "Best Meal",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 10, 30, 27, 191, DateTimeKind.Local).AddTicks(8895),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 19, 18, 27, 191, DateTimeKind.Local).AddTicks(8843),
                            Price = 10.53m,
                            Type = "Avondmaaltijd"
                        },
                        new
                        {
                            Id = 5,
                            Adult = false,
                            CanteenId = 3,
                            City = "'Tilburg",
                            Name = "De goed bezig meal",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 20, 6, 27, 191, DateTimeKind.Local).AddTicks(8899),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 20, 18, 27, 191, DateTimeKind.Local).AddTicks(8898),
                            Price = 9.35m,
                            Type = "Bewust eten"
                        },
                        new
                        {
                            Id = 6,
                            Adult = true,
                            CanteenId = 3,
                            City = "Tilburg",
                            Name = "De Luxury meal",
                            PickUpTimeMax = new DateTime(2024, 10, 28, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8902),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 20, 18, 27, 191, DateTimeKind.Local).AddTicks(8901),
                            Price = 35.0m,
                            Type = "Luxemaaltijd"
                        },
                        new
                        {
                            Id = 7,
                            Adult = false,
                            CanteenId = 4,
                            City = "Tilburg",
                            Name = "Japanese meal",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 22, 30, 27, 191, DateTimeKind.Local).AddTicks(8906),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 19, 18, 27, 191, DateTimeKind.Local).AddTicks(8904),
                            Price = 15.0m,
                            Type = "Bewust eten"
                        },
                        new
                        {
                            Id = 8,
                            Adult = false,
                            CanteenId = 4,
                            City = "Tilburg",
                            Name = "De frituur meal",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 20, 6, 27, 191, DateTimeKind.Local).AddTicks(8909),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 19, 18, 27, 191, DateTimeKind.Local).AddTicks(8908),
                            Price = 6.99m,
                            Type = "Avondmaaltijd"
                        },
                        new
                        {
                            Id = 9,
                            Adult = false,
                            CanteenId = 5,
                            City = "'s-Hertogenbosch",
                            Name = "De Avans box",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8912),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(8911),
                            Price = 8.00m,
                            Type = "Brood"
                        },
                        new
                        {
                            Id = 10,
                            Adult = true,
                            CanteenId = 5,
                            City = "'s-Hertogenbosch",
                            Name = "Studenten box",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8916),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(8914),
                            Price = 7.21m,
                            Type = "Alcoholische drank"
                        },
                        new
                        {
                            Id = 11,
                            Adult = false,
                            CanteenId = 6,
                            City = "'s-Hertogenbosch",
                            Name = "Soda box",
                            PickUpTimeMax = new DateTime(2024, 10, 27, 12, 54, 27, 191, DateTimeKind.Local).AddTicks(8999),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(8997),
                            Price = 3.10m,
                            Type = "Drinken"
                        },
                        new
                        {
                            Id = 12,
                            Adult = false,
                            CanteenId = 6,
                            City = "'s-Hertogenbosch",
                            Name = "Zuivel box",
                            PickUpTimeMax = new DateTime(2024, 10, 28, 10, 30, 27, 191, DateTimeKind.Local).AddTicks(9002),
                            PickUpTimeMin = new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(9001),
                            Price = 4.10m,
                            Type = "Zuivel producten"
                        });
                });

            modelBuilder.Entity("Domain.Mealbox_Product", b =>
                {
                    b.Property<int>("MealBoxId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("MealBoxId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("MealboxProducts");

                    b.HasData(
                        new
                        {
                            MealBoxId = 1,
                            ProductId = 1,
                            Id = 1
                        },
                        new
                        {
                            MealBoxId = 1,
                            ProductId = 3,
                            Id = 2
                        },
                        new
                        {
                            MealBoxId = 2,
                            ProductId = 4,
                            Id = 3
                        },
                        new
                        {
                            MealBoxId = 2,
                            ProductId = 5,
                            Id = 4
                        },
                        new
                        {
                            MealBoxId = 5,
                            ProductId = 1,
                            Id = 5
                        },
                        new
                        {
                            MealBoxId = 5,
                            ProductId = 13,
                            Id = 6
                        },
                        new
                        {
                            MealBoxId = 6,
                            ProductId = 4,
                            Id = 7
                        },
                        new
                        {
                            MealBoxId = 6,
                            ProductId = 5,
                            Id = 8
                        },
                        new
                        {
                            MealBoxId = 6,
                            ProductId = 8,
                            Id = 9
                        },
                        new
                        {
                            MealBoxId = 7,
                            ProductId = 6,
                            Id = 10
                        },
                        new
                        {
                            MealBoxId = 7,
                            ProductId = 12,
                            Id = 11
                        },
                        new
                        {
                            MealBoxId = 7,
                            ProductId = 10,
                            Id = 12
                        },
                        new
                        {
                            MealBoxId = 8,
                            ProductId = 2,
                            Id = 13
                        },
                        new
                        {
                            MealBoxId = 8,
                            ProductId = 7,
                            Id = 14
                        },
                        new
                        {
                            MealBoxId = 8,
                            ProductId = 10,
                            Id = 15
                        },
                        new
                        {
                            MealBoxId = 9,
                            ProductId = 11,
                            Id = 16
                        },
                        new
                        {
                            MealBoxId = 9,
                            ProductId = 13,
                            Id = 17
                        },
                        new
                        {
                            MealBoxId = 9,
                            ProductId = 10,
                            Id = 18
                        },
                        new
                        {
                            MealBoxId = 10,
                            ProductId = 2,
                            Id = 19
                        },
                        new
                        {
                            MealBoxId = 10,
                            ProductId = 3,
                            Id = 20
                        },
                        new
                        {
                            MealBoxId = 10,
                            ProductId = 7,
                            Id = 21
                        },
                        new
                        {
                            MealBoxId = 10,
                            ProductId = 9,
                            Id = 22
                        },
                        new
                        {
                            MealBoxId = 10,
                            ProductId = 13,
                            Id = 23
                        },
                        new
                        {
                            MealBoxId = 11,
                            ProductId = 10,
                            Id = 24
                        },
                        new
                        {
                            MealBoxId = 11,
                            ProductId = 13,
                            Id = 25
                        },
                        new
                        {
                            MealBoxId = 12,
                            ProductId = 13,
                            Id = 26
                        });
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("containsAlcohol")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageLink = "https://images.pexels.com/photos/1093038/pexels-photo-1093038.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                            Name = "Banaan",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 2,
                            ImageLink = "https://images.pexels.com/photos/1633578/pexels-photo-1633578.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                            Name = "Burger",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 3,
                            ImageLink = "https://images.pexels.com/photos/52994/beer-ale-bitter-fermented-52994.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                            Name = "Bier",
                            containsAlcohol = true
                        },
                        new
                        {
                            Id = 4,
                            ImageLink = "https://images.pexels.com/photos/3743389/pexels-photo-3743389.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                            Name = "Hertenbiefstuk",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 5,
                            ImageLink = "https://images.pexels.com/photos/1841506/pexels-photo-1841506.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                            Name = "Champagnefles",
                            containsAlcohol = true
                        },
                        new
                        {
                            Id = 6,
                            ImageLink = "https://images.unsplash.com/photo-1580822184713-fc5400e7fe10?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80",
                            Name = "Sushi",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 7,
                            ImageLink = "https://images.unsplash.com/photo-1598679253544-2c97992403ea?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80",
                            Name = "Patat",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 8,
                            ImageLink = "https://images.unsplash.com/photo-1474722883778-792e7990302f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=691&q=80",
                            Name = "Wijn",
                            containsAlcohol = true
                        },
                        new
                        {
                            Id = 9,
                            ImageLink = "https://images.unsplash.com/photo-1621852004158-f3bc188ace2d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
                            Name = "Panini",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 10,
                            ImageLink = "https://images.unsplash.com/photo-1581636625402-29b2a704ef13?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=688&q=80",
                            Name = "Frisdrank",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 11,
                            ImageLink = "https://images.unsplash.com/photo-1613844237701-8f3664fc2eff?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=764&q=80",
                            Name = "Soep",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 12,
                            ImageLink = "https://images.unsplash.com/photo-1569718212165-3a8278d5f624?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80",
                            Name = "Noodles",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 13,
                            ImageLink = "https://images.unsplash.com/photo-1627308595216-439c00ade0fe?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80",
                            Name = "Belegde broodje",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 14,
                            ImageLink = "https://images.unsplash.com/photo-1595787572590-545171362a1c?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
                            Name = "Yoghurt",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 15,
                            ImageLink = "https://images.unsplash.com/photo-1634141510639-d691d86f47be?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=764&q=80",
                            Name = "Melk",
                            containsAlcohol = false
                        },
                        new
                        {
                            Id = 16,
                            ImageLink = "https://images.unsplash.com/photo-1553909489-ec2175ef3f52?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=765&q=80",
                            Name = "Chocomel",
                            containsAlcohol = false
                        });
                });

            modelBuilder.Entity("Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.Property<string>("StudyCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2002, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "quinnie@hotmail.nl",
                            FirstName = "Quinn",
                            LastName = "Verschoor",
                            PhoneNumber = "0612345678",
                            StudentNumber = 2168424,
                            StudyCity = "Breda"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2008, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jaron@gmail.com",
                            FirstName = "Jaron",
                            LastName = "van Well",
                            PhoneNumber = "0687654321",
                            StudentNumber = 2168253,
                            StudyCity = "Tilburg"
                        });
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.HasOne("Domain.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canteen");
                });

            modelBuilder.Entity("Domain.MealBox", b =>
                {
                    b.HasOne("Domain.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Student", "ReservedBy")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Canteen");

                    b.Navigation("ReservedBy");
                });

            modelBuilder.Entity("Domain.Mealbox_Product", b =>
                {
                    b.HasOne("Domain.MealBox", "MealBox")
                        .WithMany("MealboxProducts")
                        .HasForeignKey("MealBoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Product", "Product")
                        .WithMany("MealboxProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealBox");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.MealBox", b =>
                {
                    b.Navigation("MealboxProducts");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Navigation("MealboxProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
