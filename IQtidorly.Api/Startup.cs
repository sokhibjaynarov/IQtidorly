using AutoMapper;
using IQtidorly.Api.AutoMapper;
using IQtidorly.Api.Data;
using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Data.Repositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Extensions;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace IQtidorly.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(db => db.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.ConfigureSwagger(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowAnyOrigins",
                                  policy =>
                                  {
                                      policy
                                        .AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                                  });
            });

            services.AddControllers()
                .AddNewtonsoftJson(opts =>
                    opts.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));

            #region JWT Authentication
            var key = Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:Key").Value);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion

            services.AddMemoryCache();
            AddRepositories(services);
            AddServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, IServiceProvider serviceProvider)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "IQtidorly.Api v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("MyAllowAnyOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed data
            SeedData(serviceProvider).Wait();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAgeGroupService, AgeGroupService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
            services.AddScoped<ISubjectChapterRepository, SubjectChapterRepository>();
            services.AddScoped<IAgeGroupRepository, AgeGroupRepository>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
        }

        private static async Task SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await ContextSeed.SeedRolesAsync(userManager, roleManager);
                await ContextSeed.SeedUsersAsync(userManager, roleManager);
                await ContextSeed.SeedSubjectsAndChaptersAsync(context);
                await ContextSeed.SeedBooksAndAuthorsAsync(context);
                await ContextSeed.SeedAgeGroupsAsync(context);
            }
        }
    }
}
