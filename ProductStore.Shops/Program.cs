using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductStore.Shops.Shop.BLL.Services.Implementations;
using ProductStore.Shops.Shop.BLL.Services.Interfaces;
using ProductStore.Shops.Shops.DAL;
using ProductStore.Shops.Shops.DAL.Repositories.Implementations;
using ProductStore.Shops.Shops.DAL.Repositories.Implementations.ManyToManyRepo;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces.ManyToManyInterfaces;
using ProductStore.Shops.Shops.Domain.Domain.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<ShopsContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"))
        .EnableSensitiveDataLogging());

//Репозитории
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IShopRepo, ShopRepo>();
builder.Services.AddScoped<IProductTypeRepo, ProductTypeRepo>();
builder.Services.AddScoped<IProductsWithTypesRepo, ProductsWithTypesRepo>();
builder.Services.AddScoped<IProductsShopsRepo, ProductsShopsRepo>();

//Сервисы
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["AuthSettings:Audience"],
            ValidIssuer = builder.Configuration["AuthSettings:Issuer"]
        };
    });


var app = builder.Build();


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