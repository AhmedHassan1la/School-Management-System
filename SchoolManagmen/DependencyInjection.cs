
using Asp.Versioning;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Entites;
using SchoolManagmen.Extensions;
using SchoolManagmen.Service;
using SchoolManagmen.Settings;
using SchoolManagmen.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

namespace SchoolManagmen
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services
            , IConfiguration configuration)
        {

            services.AddControllers();
            services.AddCors(options =>
          options.AddDefaultPolicy(builder =>
              builder
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
          )
      );


            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
          throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add Swagger services
            services
                .AddSwaggerServices()
                .AddMapsterConfig()
                .AddAuthConfig(configuration)
                .AddFluentValidationConfig();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEmailSender, EmailService>();

            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IGradeReportService, GradeReportService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddBackgroundJobsConfig(configuration);

            services.AddProblemDetails();
            services.AddHttpContextAccessor();
            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
            services.AddRateLimitingConfig();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;

                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            })
         .AddApiExplorer(options =>
         {
             options.GroupNameFormat = "'v'V";
             options.SubstituteApiVersionInUrl = true;
         });

            return services;

        }


        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Add Swagger services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {



                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.OperationFilter<SwaggerDefaultValues>();
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            return services;
        }

        public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            //  services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }
        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            return services;
        }
        public static IServiceCollection AddAuthConfig(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddSingleton<IJwtProvider, JWTProvider>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IJwtProvider, JWTProvider>();

            //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience
            };
        });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
             
            });

            return services;
        }

        private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
     IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();

            return services;
        }


        private static IServiceCollection AddRateLimitingConfig(this IServiceCollection services)
        {
            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddPolicy(RateLimiters.IpLimiter, httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 2,
                            Window = TimeSpan.FromSeconds(20)
                        }
                    )
                );

                rateLimiterOptions.AddPolicy(RateLimiters.UserLimiter, httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.GetUserId(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 2,
                            Window = TimeSpan.FromSeconds(20)
                        }
                    )
                );

                rateLimiterOptions.AddConcurrencyLimiter(RateLimiters.Concurrency, options =>
                {
                    options.PermitLimit = 1000;
                    options.QueueLimit = 100;
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

           
            });

            return services;
        }

    }

}
