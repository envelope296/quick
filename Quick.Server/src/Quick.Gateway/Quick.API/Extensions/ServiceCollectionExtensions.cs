using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.BusinessLogic.Repositories.Implementations;
using Quick.BusinessLogic.Services.Abstractions;
using Quick.BusinessLogic.Services.Implementations;
using Quick.Common.Configuration.Extensions;
using Quick.Common.Configuration.Options;
using Quick.Common.Context.UserContext;
using System.Text;

namespace Quick.API.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetRequired<JwtOptions>(JwtOptions.ConfigurationKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            return services;
        }

        internal static IServiceCollection AddOption<TOption>(this IServiceCollection services, string key) where TOption : class
        {
            services.AddOptions<TOption>()
                .BindConfiguration(key)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            return services;
        }

        internal static IServiceCollection ConfigureOptions(this IServiceCollection services)
        {
            services.AddOption<JwtOptions>(JwtOptions.ConfigurationKey);
            services.AddOption<BotOptions>(BotOptions.ConfigurationKey);
            return services;
        }

        internal static IServiceCollection AddRepositoryImplementations(this IServiceCollection services)
        {
            services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonTypeRepository, LessonTypeRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISubgroupRepository, SubgroupRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        internal static IServiceCollection AddServiceImplementations(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerationService, JwtGenerationService>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        internal static IServiceCollection AddUserContextAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IUserContextAccessor, ClaimsUserContextAccessor>();
            return services;
        }
    }
}
