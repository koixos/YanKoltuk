using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YanKoltukBackend.Data;
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

builder.Services.AddScoped<UserHelper>();
builder.Services.AddScoped<AuthHelper>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IParentService, ParentService>();
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
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
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

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
