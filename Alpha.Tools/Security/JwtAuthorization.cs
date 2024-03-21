
using Microsoft.AspNetCore.Authorization;

namespace Alpha.Tools.Security;

public static class JwtAuthorization
{
    public static AuthorizationBuilder AddAlphaAuthorizationPolicies(this AuthorizationBuilder builder)
    {
        foreach( var claim in PolicyClaim.Values )
        {
            builder = builder.AddPolicy(claim.Type, authBuilder => { authBuilder.RequireClaim(claim.Type); });
        }

        return builder;
    }
}