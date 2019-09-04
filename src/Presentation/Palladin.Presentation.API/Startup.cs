using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Palladin.Presentation.API.Helpers;
using Palladin.Services.LogicService;
using Palladin.Services.LogicService.AuthenticationLogic;
using Palladin.Services.LogicService.MenusLogic;
using Palladin.Services.LogicService.ProjectLogic;
using Palladin.Services.LogicService.VulnerabilityLogic;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Palladin.Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMapper mapper = new MapperConfiguration(mc => mc
                .AddProfile(new MappingProfile()))
                .CreateMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:3000"));
            //});

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var conString = Encoding.ASCII.GetBytes(appSettings.ConString);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("token-fallen", "true");
                        }
                        return Task.CompletedTask;
                    },
                };
            });

            //configure DI for application services
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IMenuLogic, MenuLogic>();
            services.AddScoped<IVulnerabilityLogic, VulnerabilityLogic>();
            services.AddScoped<IProjectLogic, ProjectLogic>();
            services.AddScoped<IJwtServices, JwtServices>();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
