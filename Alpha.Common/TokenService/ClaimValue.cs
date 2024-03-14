using System.Text.Json.Serialization;

namespace Alpha.Common.TokenService;

public record ClaimValue
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}