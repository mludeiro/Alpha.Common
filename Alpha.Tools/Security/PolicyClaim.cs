using System.Collections.ObjectModel;
using System.Security.Claims;

namespace Alpha.Tools.Security;

public class PolicyClaim(string type, string value)
{
    public const string identityUserMe = "Identity.User.Me";/*  */
    public const string identityUserRead = "Identity.User.Read";
    public const string identityUserWrite = "Identity.User.Write";
    public const string identityWeatherRead = "Weather.Weather.Read";

    public static readonly ReadOnlyCollection<PolicyClaim> Values = new(
        [
            new PolicyClaim(identityUserMe, "Read or Write My User"),
            new PolicyClaim(identityUserRead, "Read Users"),
            new PolicyClaim(identityUserWrite, "Create or Update Users"),
            new PolicyClaim(identityWeatherRead, "Read Weather information")
        ]);

    public string Type = type;

    public string Value = value;

    public Claim ToClaim()
    {
        return new Claim(Type, "true");
    }
}