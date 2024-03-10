using Newtonsoft.Json;

namespace Alpha.Common.Identity;

public class AccountInfo
{
    [JsonProperty("firstName")]
    public string? FirstName { get; set; }

    [JsonProperty("lastName")]
    public string? LastName { get; set; }

    [JsonProperty("isAdmin")]
    public bool IsAdmin { get; set; }

    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("userName")]
    public required string UserName { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("emailConfirmed")]
    public bool EmailConfirmed { get; set; }

    [JsonProperty("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [JsonProperty("phoneNumberConfirmed")]
    public bool PhoneNumberConfirmed { get; set; }

    [JsonProperty("twoFactorEnabled")]
    public bool TwoFactorEnabled { get; set; }

    [JsonProperty("lockoutEnd")]
    public DateTime LockoutEnd { get; set; }

    [JsonProperty("lockoutEnabled")]
    public bool LockoutEnabled { get; set; }

    [JsonProperty("accessFailedCount")]
    public int AccessFailedCount { get; set; }
}
