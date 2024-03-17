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


    private static AuthenticationBuilder AddAlphaWithoutSignatureAuthentication( IServiceCollection builder, JwtOptions jwtoptions)
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
                    ValidIssuer = jwtoptions.Issuer,
                    ValidAudience = jwtoptions.Audience,
                    // Do not validate signature
                    ValidateIssuerSigningKey = false,
                    SignatureValidator = (string token, TokenValidationParameters parameters) => new JwtSecurityToken(token)
                };
            });
    }

    private static AuthenticationBuilder AddAlphaWithSignatureAuthentication( IServiceCollection builder, JwtOptions jwtoptions)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.Key!)),

            ValidateIssuer = true,
            ValidIssuer = jwtoptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtoptions.Audience,

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