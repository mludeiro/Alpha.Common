using System.Text.Json.Serialization;

namespace Alpha.Common.Identity;

public class AccountRefresh
{
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }
}