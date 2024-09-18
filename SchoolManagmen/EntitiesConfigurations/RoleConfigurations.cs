﻿using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Entites;

namespace SchoolManagmen.EntitiesConfigurations;

public class RoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        //Default Data
        builder.HasData([
            new ApplicationRole
            {
                Id = DefaultRoles.AdminRoleId,
                Name = DefaultRoles.Admin,
                NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp
            },
            new ApplicationRole
            {
                Id = DefaultRoles.MemberRoleId,
                Name = DefaultRoles.Member,
                NormalizedName = DefaultRoles.Member.ToUpper(),
                ConcurrencyStamp = DefaultRoles.MemberRoleConcurrencyStamp,
                IsDefault = true
            }
        ]);
    }

}

