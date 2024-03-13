using System;
using _5s.Context;
using _5s.Repositories;
using _5s.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;


namespace _5s
{
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ConfigureServices(builder.Services);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CorsPolicy");
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    static void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddControllers().ConfigureApiBehaviorOptions(x => { x.SuppressMapClientErrors = true; });
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            // Add header documentation in swagger
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "5s Methodology",
                Description = "An API that evaluates using the 5s methodology in a workplace.",
            });

            // Feed generated xml api docs to swagger
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<DapperContext>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IBuildingService, BuildingService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IRedTagService, RedTagService>();
        services.AddScoped<ISpaceService, SpaceService>();
        services.AddScoped<ISpaceImageService, SpaceImageService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<IRatingsRepository, RatingsRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IRedTagRepository, RedTagRepository>();
        services.AddScoped<ISpaceRepository, SpaceRepository>();
        services.AddScoped<ISpaceImageRepository, SpaceImageRepository>();
    }
}
}