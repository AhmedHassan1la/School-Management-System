namespace SchoolManagmen.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RegisterAsync(Contracts.Authentication.RegisterRequest request, CancellationToken cancellationToken = default);
        Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmailAsync(Contracts.Authentication.ResendConfirmationEmailRequest request);
        Task<Result> SendResetPasswordCodeAsync(string email);
        Task<Result> ResetPasswordAsync(Contracts.Authentication.ResetPasswordRequest request);


    }
}
