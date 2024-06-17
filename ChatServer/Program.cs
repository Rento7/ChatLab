using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using ChatDb;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;
using ChatServer.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer;

public class Program
{
    const string DevelopmentSwagger = "DevelopmentSwagger";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.EnvironmentName == DevelopmentSwagger)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
            (options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        if (builder.Environment.EnvironmentName != DevelopmentSwagger)
        {
            builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddSignalR();
        }

        builder.Services.AddDbContext<ChatContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IChatRepository, ChatRepository>();

        var app = builder.Build();

        //init db
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ChatContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }

        if (app.Environment.EnvironmentName == DevelopmentSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(o =>
            {
                o.DefaultModelRendering(ModelRendering.Model);
                o.DefaultModelExpandDepth(1);
            });
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();

        if (app.Environment.EnvironmentName != DevelopmentSwagger)
        { 
            app.UseAuthentication(); 
            app.UseAuthorization();  

            app.MapPost("/login", AuthorizationApi.Login);

            app.MapHub<ChatHub>("/chat");
        }
        else
        {
            var chatApiGroup = app.MapGroup("/chatapi");
            chatApiGroup.MapGet("/users", TestChatApi.GetUsers);
            chatApiGroup.MapGet("/chats", TestChatApi.GetChats);
        }

        app.Run();
    }
}
