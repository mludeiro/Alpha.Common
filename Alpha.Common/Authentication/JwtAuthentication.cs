using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Alpha.Common.Authentication;

public static class JwtAuthentication
{
    public static AuthenticationBuilder AddAlphaAuthentication( this IServiceCollection builder, JwtOptions options)
    {
        return string.IsNullOrEmpty(options.Key) ?
            AddAlphaWithoutSignatureAuthentication(builder, options)
            : AddAlphaWithSignatureAuthentication(builder, options);
    }


    private static AuthenticationBuilder AddAlphaWithoutSignatureAuthentication( IServiceCollection builder, JwtOptions options)
    {
        return builder
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = options.ClaimsIssuer,
                    ValidAudience = options.Audience,
                    // Do not validate signature
                    ValidateIssuerSigningKey = false,
                    SignatureValidator = (string token, TokenValidationParameters parameters) => new JwtSecurityToken(token)
                };
            });
    }

    private static AuthenticationBuilder AddAlphaWithSignatureAuthentication( IServiceCollection builder, JwtOptions options)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key!)),

            ValidateIssuer = true,
            ValidIssuer = options.Issuer,

            ValidateAudience = true,
            ValidAudience = options.Audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        return builder
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
            });
    }


}