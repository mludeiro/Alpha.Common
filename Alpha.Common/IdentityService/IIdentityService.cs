using Alpha.Common.TokenService;
using Refit;

namespace Alpha.Common.Identity;

public interface IIdentityService
{
    [Post("/api/account/register")]
    public Task<ApiResponse<object>> Register([Body]AccountRegister accountRegister);

    [Post("/api/account/login")]
    public Task<ApiResponse<TokenGeneration>> Login([Body]AccountLogin accountLogin);

    [Get("/api/account/me")]
    public Task<ApiResponse<AccountInfo>> Me();

    [Get("/api/account/me")]
    public Task<ApiResponse<AccountInfo>> Me([Header("Authorize")] string authorize);


}