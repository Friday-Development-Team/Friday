﻿using Friday.Data;
using Friday.Data.IServices;
using Friday.Data.ServiceInstances;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Friday
{
    /// <summary>
    /// Configuration options for the application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Who even reads these?
        /// </summary>
        /// <param name="configuration">Config options</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration options
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures used services to build and run the application
        /// </summary>
        /// <param name="services">List of services</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<ILogsService, LogsService>();
            services.AddScoped<DataInitializer>();

            services.AddDbContext<Context>(Options =>
                Options.UseSqlServer(Configuration.GetConnectionString("Context"))
            );

            services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
            );

            services.AddSwaggerDocument(c =>
            {
                c.DocumentName = "apidocs";
                c.Title = "Friday API";
                c.Version = "v1";
                c.Description = "The Friday API documentation. A seeded set of common Items has been provided. These can be changed or deleted according to the requirements.";
                c.DocumentProcessors.Add(
                    new SecurityDefinitionAppender(
                        "JWT Token",
                        new OpenApiSecurityScheme()
                        {
                            Type = OpenApiSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                            In = OpenApiSecurityApiKeyLocation.Header,
                            Description = "Copy 'Bearer' + valid JWT token into field"
                        })

                );
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            });
            //for OpenAPI 3.0 else AddSwaggerDocument();

            services.AddIdentity<IdentityUser, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true).AddEntityFrameworkStores<Context>();


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.//Options are redundant, set values are default
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                options.User.RequireUniqueEmail = false;

            });

            services.AddAuthorization(c =>
            {
                c.AddPolicy("AdminOnly", pol => pol.RequireRole("Admin"));
                c.AddPolicy("Personnel", pol => pol.RequireRole("Admin", "Catering", "Kitchen"));
                c.AddPolicy("Catering", pol => pol.RequireRole("Admin", "Catering"));
            });

            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
        }

        /// <summary>
        /// Configures the application itself
        /// </summary>
        /// <param name="app">Builder</param>
        /// <param name="env">Environment in which the application is hosted</param>
        /// <param name="initializer">Object that initializes data</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            initializer.InitializeData().Wait();
        }
    }
}
