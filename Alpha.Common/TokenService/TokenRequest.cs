using System.Text.Json.Serialization;

namespace Alpha.Common.TokenService;

public record TokenRequest
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("userId")]
    public required string UserId { get; set; }

    [JsonPropertyName("userName")]
    public required string UserName { get; set; }

    [JsonPropertyName("claimValues")]
    public required List<ClaimValue> ClaimValues { get; set; }
}