using System.Text.Json.Serialization;

namespace Alpha.Common.Token;

public class TokenGeneration
{
    [JsonPropertyName("token")]
    public string? Token {get; set;}
}