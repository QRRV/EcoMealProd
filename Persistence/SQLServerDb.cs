using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public class SqlServerDb : DbContext
{
    private readonly IConfiguration? _configuration;

    // Parameterless constructor for EF design-time support
    public SqlServerDb()
    {
    }

    // Constructor for runtime DI with configuration
    public SqlServerDb(IConfiguration configuration, DbContextOptions<SqlServerDb> options)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Use configuration if available; otherwise, retrieve from JSON for design-time support
            var connectionString = _configuration?.GetConnectionString("PrimaryDatabase") 
                                   ?? new ConfigurationBuilder()
                                       .SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json")
                                       .Build()
                                       .GetConnectionString("PrimaryDatabase");

            Console.WriteLine("Primary DB Connection String (SqlServerDb):");
            Console.WriteLine(connectionString);

            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

    public DbSet<Canteen> Canteens { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<MealBox> MealBoxes { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Mealbox_Product> MealboxProducts { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();
        modelBuilder.Entity<Mealbox_Product>()
            .HasKey(x => new { x.MealBoxId, x.ProductId });
        modelBuilder.Entity<Mealbox_Product>()
            .HasOne(m => m.MealBox)
            .WithMany(mp => mp.MealboxProducts)
            .HasForeignKey(mi => mi.MealBoxId);

        modelBuilder.Entity<Mealbox_Product>()
            .HasOne(m => m.Product)
            .WithMany(mp => mp.MealboxProducts)
            .HasForeignKey(mi => mi.ProductId);

        modelBuilder.Entity<Canteen>()
            .HasData(
                new Canteen
                {
                    Id = 1, City = "Breda", Location = "la", HotMeals = false
                },
                new Canteen
                {
                    Id = 2, City = "Breda", Location = "ld", HotMeals = true
                },
                new Canteen
                {
                    Id = 3, City = "Tilburg", Location = "Ha", HotMeals = true
                },
                new Canteen
                {
                    Id = 4, City = "Tilburg", Location = "Dc", HotMeals = true
                }
                ,
                new Canteen
                {
                    Id = 5, City = "'s-Hertogenbosch", Location = "Bk", HotMeals = false
                }
                ,
                new Canteen
                {
                    Id = 6, City = "'s-Hertogenbosch", Location = "Lc", HotMeals = true
                }
            );

        modelBuilder.Entity<Product>()
            .HasData(
                new Product
                {
                    Id = 1, Name = "Banaan", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.pexels.com/photos/1093038/pexels-photo-1093038.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Product
                {
                    Id = 2, Name = "Burger", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.pexels.com/photos/1633578/pexels-photo-1633578.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Product
                {
                    Id = 3, Name = "Bier", ContainsAlcohol = true,
                    ImageLink =
                        "https://images.pexels.com/photos/52994/beer-ale-bitter-fermented-52994.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Product
                {
                    Id = 4, Name = "Hertenbiefstuk", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.pexels.com/photos/3743389/pexels-photo-3743389.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Product
                {
                    Id = 5, Name = "Champagnefles", ContainsAlcohol = true,
                    ImageLink =
                        "https://images.pexels.com/photos/1841506/pexels-photo-1841506.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Product
                {
                    Id = 6, Name = "Sushi", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1580822184713-fc5400e7fe10?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80"
                },
                new Product
                {
                    Id = 7, Name = "Patat", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1598679253544-2c97992403ea?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80"
                },
                new Product
                {
                    Id = 8, Name = "Wijn", ContainsAlcohol = true,
                    ImageLink =
                        "https://images.unsplash.com/photo-1474722883778-792e7990302f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=691&q=80"
                },
                new Product
                {
                    Id = 9, Name = "Panini", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1621852004158-f3bc188ace2d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80"
                },
                new Product
                {
                    Id = 10, Name = "Frisdrank", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1581636625402-29b2a704ef13?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=688&q=80"
                }
                ,
                new Product
                {
                    Id = 11, Name = "Soep", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1613844237701-8f3664fc2eff?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=764&q=80"
                },
                new Product
                {
                    Id = 12, Name = "Noodles", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1569718212165-3a8278d5f624?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80"
                },
                new Product
                {
                    Id = 13, Name = "Belegde broodje", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1627308595216-439c00ade0fe?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80"
                },
                new Product
                {
                    Id = 14, Name = "Yoghurt", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1595787572590-545171362a1c?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80"
                }
                ,
                new Product
                {
                    Id = 15, Name = "Melk", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1634141510639-d691d86f47be?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=764&q=80"
                },
                new Product
                {
                    Id = 16, Name = "Chocomel", ContainsAlcohol = false,
                    ImageLink =
                        "https://images.unsplash.com/photo-1553909489-ec2175ef3f52?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=765&q=80"
                }
            );
        modelBuilder.Entity<Student>()
            .HasData(
                new Student
                {
                    Id = 1, FirstName = "Quinn", LastName = "Verschoor", Email = "quinnie@hotmail.nl",
                    BirthDate = new DateTime(2002, 11, 16), PhoneNumber = "0612345678", StudentNumber = 2168424,
                    StudyCity = "Breda"
                },
                new Student
                {
                    Id = 2, FirstName = "Jaron", LastName = "van Well", Email = "jaron@gmail.com",
                    BirthDate = new DateTime(2008, 1, 19), PhoneNumber = "0687654321", StudentNumber = 2168253,
                    StudyCity = "Tilburg"
                }
            );

        modelBuilder.Entity<Employee>()
            .HasData(
                new Employee
                {
                    Id = 1, CanteenId = 1, EmployeeNumber = 12, FirstName = "Lucas", LastName = "het Kleijn",
                    Email = "lucas@gmail.com"
                },
                new Employee
                {
                    Id = 2, CanteenId = 2, EmployeeNumber = 51, FirstName = "Hans Gerard", LastName = "Karremans",
                    Email = "hans@gmail.com"
                },
                new Employee
                {
                    Id = 3, CanteenId = 3, EmployeeNumber = 34, FirstName = "Arno", LastName = "Broeders",
                    Email = "arno@gmail.com"
                },
                new Employee
                {
                    Id = 4, CanteenId = 4, EmployeeNumber = 123, FirstName = "Pascal", LastName = "van Gastel",
                    Email = "pascal@gmail.com"
                },
                new Employee
                {
                    Id = 5, CanteenId = 5, EmployeeNumber = 535, FirstName = "Robin", LastName = "Schellius",
                    Email = "robin@gmail.com"
                },
                new Employee
                {
                    Id = 6, CanteenId = 6, EmployeeNumber = 143, FirstName = "Dion", LastName = "Koeze",
                    Email = "dion@gmail.com"
                }
            );
        modelBuilder.Entity<MealBox>()
            .HasData(
                new MealBox
                {
                    Id = 1, CanteenId = 1, Adult = true, City = "Breda", Name = "Lucas Meal", Price = 52.00m,
                    Type = "Alcoholische drank", StudentId = 2, PickUpTimeMin = DateTime.Now.AddHours(30),
                    PickUpTimeMax = DateTime.Now.AddDays(2)
                },
                new MealBox
                {
                    Id = 2, CanteenId = 2, Adult = true, City = "Breda", Name = "Hans Meal", Price = 200.00m,
                    Type = "Luxemaaltijd", StudentId = 1, PickUpTimeMin = DateTime.Now.AddHours(20),
                    PickUpTimeMax = DateTime.Now.AddDays(1.7)
                },
                new MealBox
                {
                    Id = 3, CanteenId = 1, Adult = false, City = "Breda", Name = "Jaron Meal", Price = 1.00m,
                    Type = "Budgetmeal", PickUpTimeMin = DateTime.Now.AddHours(10),
                    PickUpTimeMax = DateTime.Now.AddDays(1)
                },
                new MealBox
                {
                    Id = 4, CanteenId = 2, Adult = false, City = "Breda", Name = "Best Meal", Price = 10.53m,
                    Type = "Avondmaaltijd", PickUpTimeMin = DateTime.Now.AddHours(4),
                    PickUpTimeMax = DateTime.Now.AddDays(0.8)
                },
                new MealBox
                {
                    Id = 5, CanteenId = 3, Adult = false, City = "'Tilburg", Name = "De goed bezig meal", Price = 9.35m,
                    Type = "Bewust eten", PickUpTimeMin = DateTime.Now.AddHours(5),
                    PickUpTimeMax = DateTime.Now.AddDays(1.2)
                },
                new MealBox
                {
                    Id = 6, CanteenId = 3, Adult = true, City = "Tilburg", Name = "De Luxury meal", Price = 35.0m,
                    Type = "Luxemaaltijd", PickUpTimeMin = DateTime.Now.AddHours(5),
                    PickUpTimeMax = DateTime.Now.AddDays(2)
                },
                new MealBox
                {
                    Id = 7, CanteenId = 4, Adult = false, City = "Tilburg", Name = "Japanese meal", Price = 15.0m,
                    Type = "Bewust eten", PickUpTimeMin = DateTime.Now.AddHours(4),
                    PickUpTimeMax = DateTime.Now.AddDays(1.3)
                },
                new MealBox
                {
                    Id = 8, CanteenId = 4, Adult = false, City = "Tilburg", Name = "De frituur meal", Price = 6.99m,
                    Type = "Avondmaaltijd", PickUpTimeMin = DateTime.Now.AddHours(4),
                    PickUpTimeMax = DateTime.Now.AddDays(1.2)
                },
                new MealBox
                {
                    Id = 9, CanteenId = 5, Adult = false, City = "'s-Hertogenbosch", Name = "De Avans box",
                    Price = 8.00m,
                    Type = "Brood", PickUpTimeMin = DateTime.Now.AddHours(2),
                    PickUpTimeMax = DateTime.Now.AddDays(1)
                },
                new MealBox
                {
                    Id = 10, CanteenId = 5, Adult = true, City = "'s-Hertogenbosch", Name = "Studenten box",
                    Price = 7.21m,
                    Type = "Alcoholische drank", PickUpTimeMin = DateTime.Now.AddHours(2),
                    PickUpTimeMax = DateTime.Now.AddDays(1)
                },
                new MealBox
                {
                    Id = 11, CanteenId = 6, Adult = false, City = "'s-Hertogenbosch", Name = "Soda box", Price = 3.10m,
                    Type = "Drinken", PickUpTimeMin = DateTime.Now.AddHours(2),
                    PickUpTimeMax = DateTime.Now.AddDays(0.9)
                },
                new MealBox
                {
                    Id = 12, CanteenId = 6, Adult = false, City = "'s-Hertogenbosch", Name = "Zuivel box",
                    Price = 4.10m,
                    Type = "Zuivel producten", PickUpTimeMin = DateTime.Now.AddHours(2),
                    PickUpTimeMax = DateTime.Now.AddDays(1.8)
                }
            );

        modelBuilder.Entity<Mealbox_Product>()
            .HasData(
                new Mealbox_Product
                {
                    Id = 1, ProductId = 1, MealBoxId = 1
                },
                new Mealbox_Product
                {
                    Id = 2, ProductId = 3, MealBoxId = 1
                },
                new Mealbox_Product
                {
                    Id = 3, ProductId = 4, MealBoxId = 2
                },
                new Mealbox_Product
                {
                    Id = 4, ProductId = 5, MealBoxId = 2
                },
                new Mealbox_Product
                {
                    Id = 5, ProductId = 1, MealBoxId = 5
                },
                new Mealbox_Product
                {
                    Id = 6, ProductId = 13, MealBoxId = 5
                },
                new Mealbox_Product
                {
                    Id = 7, ProductId = 4, MealBoxId = 6
                }
                ,
                new Mealbox_Product
                {
                    Id = 8, ProductId = 5, MealBoxId = 6
                },
                new Mealbox_Product
                {
                    Id = 9, ProductId = 8, MealBoxId = 6
                }
                ,
                new Mealbox_Product
                {
                    Id = 10, ProductId = 6, MealBoxId = 7
                },
                new Mealbox_Product
                {
                    Id = 11, ProductId = 12, MealBoxId = 7
                },
                new Mealbox_Product
                {
                    Id = 12, ProductId = 10, MealBoxId = 7
                },
                new Mealbox_Product
                {
                    Id = 13, ProductId = 2, MealBoxId = 8
                }
                ,
                new Mealbox_Product
                {
                    Id = 14, ProductId = 7, MealBoxId = 8
                }
                ,
                new Mealbox_Product
                {
                    Id = 15, ProductId = 10, MealBoxId = 8
                },
                new Mealbox_Product
                {
                    Id = 16, ProductId = 11, MealBoxId = 9
                },
                new Mealbox_Product
                {
                    Id = 17, ProductId = 13, MealBoxId = 9
                },
                new Mealbox_Product
                {
                    Id = 18, ProductId = 10, MealBoxId = 9
                },
                new Mealbox_Product
                {
                    Id = 19, ProductId = 2, MealBoxId = 10
                },
                new Mealbox_Product
                {
                    Id = 20, ProductId = 3, MealBoxId = 10
                },
                new Mealbox_Product
                {
                    Id = 21, ProductId = 7, MealBoxId = 10
                },
                new Mealbox_Product
                {
                    Id = 22, ProductId = 9, MealBoxId = 10
                },
                new Mealbox_Product
                {
                    Id = 23, ProductId = 13, MealBoxId = 10
                },
                new Mealbox_Product
                {
                    Id = 24, ProductId = 10, MealBoxId = 11
                },
                new Mealbox_Product
                {
                    Id = 25, ProductId = 13, MealBoxId = 11
                },
                new Mealbox_Product
                {
                    Id = 26, ProductId = 13, MealBoxId = 12
                }
            );
    }
}