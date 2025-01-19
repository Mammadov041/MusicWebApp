using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicService.Business.Abstract;
using MusicService.Business.Concrete;
using MusicService.DataAccess.Abstract;
using MusicService.DataAccess.Concrete.EntitiyFramework;
using MusicService.Entities.Entities;
using StackExchange.Redis;
using System.Text;

namespace MusicWebApp.MusicService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MusicServiceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure());
                options.UseLazyLoadingProxies();
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(cmp =>
            {
                var configuration = ConfigurationOptions.Parse("redis:6379", true);
                configuration.AbortOnConnectFail = true;
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddAWSService<IAmazonS3>();
            builder.Services.AddSingleton<IS3Service, S3Service>();

            builder.Services.AddScoped<IRedisService,RedisService>();
            builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();

            builder.Services.AddScoped<IMusicDal, EFMusicDal>();
            builder.Services.AddScoped<IPlaylistDal,EFPlaylistDal>();
            builder.Services.AddScoped<IMusicPlaylistDal, EFMusicPlaylistDal>();

            builder.Services.AddScoped<IMusicService,MusicService_>();
            builder.Services.AddScoped<IPlayListService, PlaylistService>();
            builder.Services.AddScoped<IMusicPlaylistService, MusicPlaylistService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
