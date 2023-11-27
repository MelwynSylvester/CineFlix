using System.Collections.Immutable;
using CineFlix.Interfaces;
using CineFlix.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Configuration;
using CineFlix.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CineFlixDbContext>(options=> {
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
});



builder.Services.AddScoped<IHomeRepository, HomeRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession();

builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();

var _jwtsetting = builder.Configuration.GetSection("JWTSettings");
builder.Services.Configure<JWTSettings>(_jwtsetting);

var authKey = builder.Configuration.GetValue<string>("JWTSettings:SecretKey");

builder.Services.AddAuthentication(item=>{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item => {
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authKey)),
        ValidateIssuer=false,
        ValidateAudience = false
    };
});

builder.Services.AddSwaggerGen(c=> 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CineFlixAuthorization", Version="v1"});
});

builder.Services.AddSingleton<HomeRepository>(new HomeRepository(new CineFlixDbContext(new DbContextOptions<CineFlixDbContext>())));

builder.Services.AddAutoMapper(x => x.AddProfile(new CineFlixMapper()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c=> {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","My API V1");
    });
}

app.UseSwagger();
app.UseSwaggerUI(c=> {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","My API V1");
    });

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //Checks if the user existing

app.UseAuthorization(); // this will check valid role

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
    name: "Login",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{controller=Admin}/{action=AddProduct}/{id?}");

app.MapControllerRoute(
    name: "Security",
    pattern: "{controller=Security}/{action=Get}/{id?}");


app.Run();
