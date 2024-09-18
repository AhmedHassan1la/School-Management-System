

using Microsoft.AspNetCore.Identity;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Entites;

namespace SchoolManagmen.EntitesConfigurations;


public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsMany(x => x.RefreshTokens)
                     .ToTable("RefreshTokens")
                     .WithOwner()
                     .HasForeignKey("UserId");

        //Default Data

        var passwordHasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(new ApplicationUser
        {
            Id = DefaultUsers.AdminId,
            FirstName = "SchoolManagmen",
            LastName = "Admin",
            UserName = DefaultUsers.AdminEmail,
            NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.AdminPassword)
        });

    }
}



