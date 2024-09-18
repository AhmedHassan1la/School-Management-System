using SchoolManagmen.Entites;

namespace SchoolManagmen.Authentication
{
    public interface IJwtProvider
    {
        (string token, int expiresin) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
        string? ValidateToken(string token);

    }
}
