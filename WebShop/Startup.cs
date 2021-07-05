using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueCliMiddleware;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services;
using WebShop.Services.ERPService;
using WebShop.Services.Interfaces;

namespace WebShop
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

            services.AddDbContext<ApplicationDbContext>(options =>
                // options.UseSqlite(
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //authentication
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(options => {
              var secretKey = Encoding.UTF8.GetBytes(Configuration["JwtConfig:SecretKey"]);
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  RequireExpirationTime = false,
                  ValidateLifetime = true,
                  ValidAlgorithms = new[] { "HS512" }
              };
          });
            //JWT Config
            JwtConfig jwtConfig = new();
            Configuration.GetSection("JwtConfig").Bind(jwtConfig);
            services.AddSingleton(jwtConfig);
            //Authentication Service
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddHttpClient<ERPAuthService>();
            services.AddTransient<ERPAuthHandler>();
            services.AddHttpClient<IERPService, ERPService>("ERPServiceClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["ERPService:BaseUrl"]);
            }).AddHttpMessageHandler<ERPAuthHandler>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //options.Password.RequiredUniqueChars = 1;
            });


            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp/";
                else
                    spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve", port: 9000);
                }

            });
        }
    }
}
