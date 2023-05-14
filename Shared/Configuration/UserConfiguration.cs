using Bora.API.Security.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bora.API.Shared.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = new Guid("ed159590-32de-4828-a20b-61c0b026b556"), 
                Firstname = "Leonardo", 
                Lastname = "Grau", 
                Email = "leonardo.grau@outlook.com.pe",
                Username = "admin", 
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234"),
                Cellphone = 940756679,
                DateExperience = new DateTime(2023, 04, 12),
                Birthday = new DateTime(2000, 01, 22),
                Description = "Oh no.",
                Career = "Software Engineer"
            },
            new User
            {
                Id = new Guid("e9651cd2-f4b5-4bd4-c4eb-08db52bd364f"), 
                Firstname = "Fabrizio", 
                Lastname = "Barra", 
                Email = "fabrizio.barra@outlook.com.pe",
                Username = "elvergaslargas19", 
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234"),
                Cellphone = 943746691,
                DateExperience = new DateTime(2023, 04, 12),
                Birthday = new DateTime(2000, 01, 22),
                Description = "Por que mejor no me chupas el pene?",
                Career = "System Engineer"
            },
            new User
            {
                Id = new Guid("6dbc9cb6-8623-4f64-b984-4dc51fa2ed98"), 
                Firstname = "Moises", 
                Lastname = "Chambi", 
                Email = "chambilin@outlook.com.pe",
                Username = "didacto", 
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234"),
                Cellphone = 920569129,
                DateExperience = new DateTime(2023, 04, 12),
                Birthday = new DateTime(2000, 01, 22),
                Description = "Oh no.",
                Career = "Medic"
            });
    }
}