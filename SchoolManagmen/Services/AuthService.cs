using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication;
using SchoolManagmen.Entites;
using SchoolManagmen.Helper;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagmen.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        ILogger<AuthService> logger,
         IHttpContextAccessor httpContextAccessor,
         IJwtProvider jwtProvider,
         SignInManager<ApplicationUser> signInManager,
         IEmailSender emailSender
        , ApplicationDbContext context) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly int _refreshTokenExpiryDays = 14;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {

            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserError.InvalidCredentials);

            if (user.IsDisabled)
                return Result.Failure<AuthResponse>(UserError.DisabledUser);


            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);
            if (result.Succeeded)
            {
                var (userRoles, userPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);


                var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration
                });

                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpiration);

                return Result.Success(response);
            }
            var error = result.IsNotAllowed
                  ? UserError.EmailNotConfirmed
                  : result.IsLockedOut
                  ? UserError.LockedUser
                  : UserError.InvalidCredentials;

            return Result.Failure<AuthResponse>(error);


        }
        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure<AuthResponse>(UserError.InvalidJwtToken);


            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure<AuthResponse>(UserError.InvalidJwtToken);
            if (user.IsDisabled)
                return Result.Failure<AuthResponse>(UserError.DisabledUser);
            if (user.LockoutEnd > DateTime.UtcNow)
                return Result.Failure<AuthResponse>(UserError.LockedUser);


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserError.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            var (userRoles, userPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);

            return Result.Success(response);
        }
        public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return false;

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return false;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return false;

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<Result> RegisterAsync(Contracts.Authentication.RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailIsExists)
                return Result.Failure(UserError.DuplicatedEmail);

            var user = request.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(user, request.Password);



            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confirmation code: {code}", code);
                await SendConfirmationEmail(user, code);

                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserError.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserError.DuplicatedConfirmation);

            var code = request.Code;

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException)
            {
                return Result.Failure(UserError.InvalidCode);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, DefaultRoles.Member);

                return Result.Success();

            }

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> ResendConfirmationEmailAsync(Contracts.Authentication.ResendConfirmationEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserError.DuplicatedConfirmation);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);

            await SendConfirmationEmail(user, code);

            return Result.Success();
        }
        public async Task<Result> SendResetPasswordCodeAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Success();
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);
            await SendResetEmail(user, code);

            return Result.Success();


        }
        public async Task<Result> ResetPasswordAsync(Contracts.Authentication.ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserError.InvalidCode);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
        }



        private async Task SendConfirmationEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.FirstName },
                    { "{{action_url}}", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
                }
            );
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Survey Basket: Email Confirmation", emailBody));
            await Task.CompletedTask;
        }


        private async Task SendResetEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("forgetPassword",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.FirstName },
                    { "{{action_url}}", $"{origin}/auth/forgetPassword?emil={user.Email}&code={code}" }
                }
            );
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Survey Basket:Change Password", emailBody));
            await Task.CompletedTask;
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var userPermissions = await _context.Roles
                .Join(_context.RoleClaims,
                    role => role.Id,
                    claim => claim.RoleId,
                    (role, claim) => new { role, claim }
                )
                .Where(x => userRoles.Contains(x.role.Name!))
                .Select(x => x.claim.ClaimValue!)
                .Distinct()
                .ToListAsync(cancellationToken);



            return (userRoles, userPermissions);
        }


    }
}
