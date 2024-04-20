
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StockDomainLayer.Models;
using StockRepositoryLayer.Data;
using StockServiceLayer;
using StockServiceLayer.Contract;
using StockServiceLayer.Implementation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(C =>
{
    C.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwtToken_Auth_API",
        Version = "v1"
    });
    C.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Here Enter JWT Token with bearer format loke bearer[space] token"
    });
    C.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("connSrting"));
});
builder.Services.AddScoped<IRepository<Order>,Repository<Order>>();
builder.Services.AddScoped<IRepository<History>, Repository<History>>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IAuth,AuthService>();
builder.Services.AddScoped<IHistory, HistoryService>();
builder.Services.AddScoped<YahooApi>();

//identy

builder.Services.AddIdentity<User, IdentityRole>().
    AddEntityFrameworkStores<AppDbContext>().
    AddDefaultTokenProviders();

//Authorization
//Jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

//Auto mapper
builder.Services.AddAutoMapper(typeof(AuthService).Assembly);


//signalR
builder.Services.AddSignalR();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});
//hangfire
builder.Services.AddHangfire(conf =>
{
    conf.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("connSrting"));
});
builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.UseHttpsRedirection();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<StockHub>("/stocks");
});
//app.MapControllers();
app.Run();
