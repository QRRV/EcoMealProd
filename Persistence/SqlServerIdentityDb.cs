using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public class SqlServerIdentityDb : IdentityDbContext
{
    private readonly IConfiguration _configuration = null!;

    
    public SqlServerIdentityDb()
    {
        
    }

    public SqlServerIdentityDb(IConfiguration configuration, DbContextOptions<SqlServerIdentityDb> options)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration?.GetConnectionString("SecondaryDatabase") 
                                   ?? "YourFallbackConnectionString";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = 1.ToString(), Name = "Student"
                },
                new IdentityRole
                {
                    Id = 2.ToString(), Name = "Employee"
                }
            );

        var user1 = new IdentityUser
        {
            Id = 1.ToString(), Email = "quinnie@hotmail.nl", UserName = "quinnie@hotmail.nl",
            NormalizedUserName = "QUINNIE@HOTMAIL.NL", NormalizedEmail = "QUINNIE@HOTMAIL.NL", EmailConfirmed = false
        };
        var user2 = new IdentityUser
        {
            Id = 2.ToString(), Email = "jaron@gmail.com", UserName = "jaron@gmail.com",
            NormalizedUserName = "JARON@GMAIL.COM", NormalizedEmail = "JARON@GMAIL.COM", EmailConfirmed = false
        };
        var user3 = new IdentityUser
        {
            Id = 3.ToString(), Email = "lucas@gmail.com", UserName = "lucas@gmail.com",
            NormalizedUserName = "LUCAS@GMAIL.COM", NormalizedEmail = "LUCAS@GMAIL.COM", EmailConfirmed = false
        };
        var user4 = new IdentityUser
        {
            Id = 4.ToString(), Email = "hans@gmail.com", UserName = "hans@gmail.com",
            NormalizedUserName = "HANS@GMAIL.COM", NormalizedEmail = "HANS@GMAIL.COM", EmailConfirmed = false
        };
        var user5 = new IdentityUser
        {
            Id = 5.ToString(), Email = "arno@gmail.com", UserName = "arno@gmail.com",
            NormalizedUserName = "ARNO@GMAIL.COM", NormalizedEmail = "ARNO@GMAIL.COM", EmailConfirmed = false
        };
        var user6 = new IdentityUser
        {
            Id = 6.ToString(), Email = "pascal@gmail.com", UserName = "pascal@gmail.com",
            NormalizedUserName = "PASCAL@GMAIL.COM", NormalizedEmail = "JPASCAL@GMAIL.COM", EmailConfirmed = false
        };
        var user7 = new IdentityUser
        {
            Id = 7.ToString(), Email = "robin@gmail.com", UserName = "robin@gmail.com",
            NormalizedUserName = "ROBIN@GMAIL.COM", NormalizedEmail = "ROBIN@GMAIL.COM", EmailConfirmed = false
        };
        var user8 = new IdentityUser
        {
            Id = 8.ToString(), Email = "dion@gmail.com", UserName = "dion@gmail.com",
            NormalizedUserName = "DION@GMAIL.COM", NormalizedEmail = "DION@GMAIL.COM", EmailConfirmed = false
        };

        var passwordHasher = new PasswordHasher<IdentityUser>();
        user1.PasswordHash = passwordHasher.HashPassword(user1, "Test123#");
        user2.PasswordHash = passwordHasher.HashPassword(user2, "Test123#");
        user3.PasswordHash = passwordHasher.HashPassword(user3, "Test123#");
        user4.PasswordHash = passwordHasher.HashPassword(user4, "Test123#");
        user5.PasswordHash = passwordHasher.HashPassword(user5, "Test123#");
        user6.PasswordHash = passwordHasher.HashPassword(user6, "Test123#");
        user7.PasswordHash = passwordHasher.HashPassword(user7, "Test123#");
        user8.PasswordHash = passwordHasher.HashPassword(user8, "Test123#");
        modelBuilder.Entity<IdentityUser>().HasData(user1);
        modelBuilder.Entity<IdentityUser>().HasData(user2);
        modelBuilder.Entity<IdentityUser>().HasData(user3);
        modelBuilder.Entity<IdentityUser>().HasData(user4);
        modelBuilder.Entity<IdentityUser>().HasData(user5);
        modelBuilder.Entity<IdentityUser>().HasData(user6);
        modelBuilder.Entity<IdentityUser>().HasData(user7);
        modelBuilder.Entity<IdentityUser>().HasData(user8);
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = 1.ToString(), UserId = 1.ToString()
                },
                new IdentityUserRole<string>
                {
                    RoleId = 1.ToString(), UserId = 2.ToString()
                },
                new IdentityUserRole<string>
                {
                    RoleId = 2.ToString(), UserId = 3.ToString()
                },
                new IdentityUserRole<string>
                {
                    RoleId = 2.ToString(), UserId = 4.ToString()
                },
                new IdentityUserRole<string>
                {
                    RoleId = 2.ToString(), UserId = 5.ToString()
                },
                new IdentityUserRole<string>
                {
                    RoleId = 2.ToString(), UserId = 6.ToString()
                },
                new IdentityUserRole<string>
                {
                    RoleId = 2.ToString(), UserId = 7.ToString()
                },
                new IdentityUserRole<string>
                {
                    RoleId = 2.ToString(), UserId = 8.ToString()
                }
            );
    }
}