using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Model.Intermediate;
using Bora.API.Shared.Configuration;
using Bora.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Bora.API.Shared.Persistence;

public class AppDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Users
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(user => user.Firstname);
        modelBuilder.Entity<User>().Property(user => user.Lastname);
        modelBuilder.Entity<User>().Property(user => user.Username);
        modelBuilder.Entity<User>().Property(user => user.HashedPassword);
        modelBuilder.Entity<User>().Property(user => user.Email);
        modelBuilder.Entity<User>().Property(user => user.Username);
        modelBuilder.Entity<User>().Property(user => user.Sex);
        modelBuilder.Entity<User>().Property(user => user.Birthday);
        modelBuilder.Entity<User>().Property(user => user.Cellphone);
        
        modelBuilder.Entity<User>().Property(user => user.DateExperience);
        modelBuilder.Entity<User>()
            .HasMany(user => user.UserRoles)
            .WithOne(userRole => userRole.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired() // Optional: Configure the relationship as required or optional
            .OnDelete(DeleteBehavior.Cascade);

        // Roles
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<Role>().HasKey(role => role.Id);
        modelBuilder.Entity<Role>().Property(role => role.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Role>().Property(role => role.RoleType);
        modelBuilder.Entity<Role>()
            .HasMany(role => role.UserRoles)
            .WithOne(userRole => userRole.Role)
            .HasForeignKey(userRole => userRole.RoleId)
            .IsRequired() // Optional: Configure the relationship as required or optional
            .OnDelete(DeleteBehavior.Cascade);
        
        // UserRoles
        modelBuilder.Entity<UserRole>().ToTable("UserRoles");
        modelBuilder.Entity<UserRole>().HasKey(role => new { role.RoleId, role.UserId });
        // modelBuilder.Entity<UserRole>().Property(role => role.UserId);
        // modelBuilder.Entity<UserRole>().Property(role => role.RoleId);
        modelBuilder.Entity<UserRole>()
            .HasOne<User>(userRole => userRole.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(userRole => userRole.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne<Role>(userRole => userRole.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.RoleId);
        



        modelBuilder.ConvertAllToSnakeCase();
       
        // Applying Default Data
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        
        
       
        
    }
}