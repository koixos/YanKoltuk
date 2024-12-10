using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YanKoltukBackend.Data;
using YanKoltukBackend.Hubs;
using YanKoltukBackend.Repositories.Implementations;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Implementations;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("YanKoltukDb");

builder.Services.AddDbContext<YanKoltukDbContext>(options => {
    options.UseSqlServer(connStr);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

builder.Services.AddScoped<UserHelper>();
builder.Services.AddScoped<AuthHelper>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<IStudentServiceService, StudentServiceService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                    context.Token = accessToken;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllClients", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://10.0.2.2")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.MapHub<NotificationHub>("/notificationHub");

app.UseCors("AllowAllClients");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
