using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Palladin.Presentation.API.Authorization;
using Palladin.Presentation.API.Filters;
using Palladin.Services.LogicService.Contracts;
using Palladin.Services.LogicService.Options;
using Palladin.Services.LogicService.Services;
using System.Text;

namespace Palladin.Presentation.API.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            configuration.Bind(nameof(appSettings), appSettings);
            services.AddSingleton(appSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(mvcConfig => mvcConfig.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddSingleton(tokenValidationParameters);
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("MustBeAwesome", pol =>
                {
                    pol.AddRequirements(new WorksForCompanyRequirement(appSettings.DomainName));
                });
            });
            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();

            services
                .Configure<AppSettings>(configuration)
                //.AddSingleton(x => x.GetRequiredService<IOptions<AppSettings>>().Value);
                .AddSingleton(appSettings);


            services.AddScoped<IIdentityService, IdentityService>();

            //services.AddSingleton<IUriService>(provider =>
            //{
            //    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            //    var request = accessor.HttpContext.Request;
            //    var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
            //    return new UriSevice(absoluteUri);
            //})
        }
    }
}
