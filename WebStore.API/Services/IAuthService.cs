﻿
namespace WebStore.API.Services;

public interface IAuthService
{
	Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
	Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
	Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
	Task<Result> ResendEmailConfirmationAsync(ResendEmailConfirmationRequest request);
	Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);

	Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
	Task<Result> SendResetPasswordCodeAsync(string email);
	Task<Result> ResetPasswordAsync(ResetPasswordRequest request);

}
