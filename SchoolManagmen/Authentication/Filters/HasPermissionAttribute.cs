using Microsoft.AspNetCore.Authorization;

namespace SchoolManagmen.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}