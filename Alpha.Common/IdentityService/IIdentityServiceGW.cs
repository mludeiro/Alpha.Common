using Alpha.Common.TokenService;
using Refit;

namespace Alpha.Common.Identity;

public interface IIdentityServiceGW
{
    [Post("/api/account/register")]
    public Task<ApiResponse<string>> Register([Body]string accountRegister);

    [Post("/api/account/login")]
    public Task<ApiResponse<string>> Login([Body]string accountLogin);

    [Get("/api/account/me")]
    public Task<ApiResponse<string>> Me();

    [Get("/api/account/me")]
    public Task<ApiResponse<string>> Me([Header("Authorization")] string authorization);


}