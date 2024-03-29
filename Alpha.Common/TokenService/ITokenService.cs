using Refit;

namespace Alpha.Common.TokenService;
    
public interface ITokenService
{
    [Post("/api/token")]
    public Task<ApiResponse<TokenGeneration?>> GetToken([Body]TokenRequest tokenRequest);

    [Get("/api/check")]
    public Task<ApiResponse<object>> CheckToken([Header("Authorization")] string authorization);
}