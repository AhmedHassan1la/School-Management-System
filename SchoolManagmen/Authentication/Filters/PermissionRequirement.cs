using Microsoft.AspNetCore.Authorization;

namespace SchoolManagmen.Authentication.Filters;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}