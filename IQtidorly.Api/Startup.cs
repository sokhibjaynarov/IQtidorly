using AutoMapper;
using IQtidorly.Api.AutoMapper;
using IQtidorly.Api.Data;
using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Data.Repositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Middlewares;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Npgsql;
using Scalar.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            // Configure the DbContext to use Npgsql with additional options
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var dataSourceBuilder = new NpgsqlDataSourceBuilder(Configuration.GetConnectionString("DefaultConnection"));
                dataSourceBuilder.EnableDynamicJson();
                options.UseNpgsql(dataSourceBuilder.Build());
            });

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            // Password settings
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            });

            services.AddControllers();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile(new HttpContextAccessor()));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

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
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddHttpContextAccessor();

            #region JWT Authentication
            var key = System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:Key").Value);
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

            services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
            services.AddMemoryCache();
            AddRepositories(services);
            AddServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, IServiceProvider serviceProvider)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("MyAllowAnyOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<SetLanguageMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapOpenApi();
                endpoints.MapScalarApiReference(options =>
                {
                    options.Title = "IQtidorly.Api v1";

                    options.Authentication = new ScalarAuthenticationOptions
                    {
                        ApiKey = new ApiKeyOptions
                        {
                            Token = "your-api-key"
                        }
                    };
                });
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
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IBookAuthorService, BookAuthorService>();
            services.AddScoped<ISubjectChapterService, SubjectChapterService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuizSubmissionService, QuizSubmissionService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRequestLanguageHelper, RequestLanguageHelper>();
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
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuizQuestionRepository, QuizQuestionRepository>();
            services.AddScoped<IQuizParticipantRepository, QuizParticipantRepository>();
            services.AddScoped<IQuizSubmissionRepository, QuizSubmissionRepository>();
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

    internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
    {
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var authenticationSchemes = await _authenticationSchemeProvider.GetAllSchemesAsync();

            if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
            {
                var requirements = new Dictionary<string, OpenApiSecurityScheme>
                {
                    ["Bearer"] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        In = ParameterLocation.Header,
                        BearerFormat = "JWT"
                    }
                };

                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = requirements;

                foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
                {
                    operation.Value.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }] = Array.Empty<string>()
                    });
                }
            }
        }
    }
}
