using Microsoft.AspNetCore.Identity;
using SchoolManagmen.Abstractions.Consts;

namespace SchoolManagmen.EntitiesConfigurations
{
    public class UserRoleConfigurations : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //Default Data
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = DefaultUsers.AdminId,
                RoleId = DefaultRoles.AdminRoleId
            });
        }

    }
}
