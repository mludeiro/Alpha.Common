using System.Text.Json.Serialization;

namespace Alpha.Common.Token;

public class ClaimValue
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}