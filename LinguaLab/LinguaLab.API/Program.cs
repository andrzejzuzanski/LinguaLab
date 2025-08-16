using LinguaLab.API.Endpoints;
using LinguaLab.Application.Interfaces;
using LinguaLab.Application.Services;
using LinguaLab.Infrastructure.Data;
using LinguaLab.Infrastructure.Repositories;
using LinguaLab.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi.Models;

namespace LinguaLab.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Add services to the container.
            builder.Services.AddDbContext<LinguaLabDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LinguaLabConnection")));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IWordRepository, WordRepository>();
            builder.Services.AddScoped<IWordService, WordService>();
            builder.Services.AddScoped<IWordProgressRepository, WordProgressRepository>();
            builder.Services.AddScoped<ILearningService, LearningService>();
            builder.Services.AddScoped<IReviewLogRepository, ReviewLogRepository>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
            builder.Services.AddScoped<IAchievementService, AchievementService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseCors(myAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAuthEndpoints();
            app.MapUsersEndpoints();
            app.MapCategoriesEndpoints();
            app.MapWordsEndpoints();
            app.MapLearningEndpoints();
            app.MapAnalyticsEndpoints();

            app.Run();
        }
    }
}
