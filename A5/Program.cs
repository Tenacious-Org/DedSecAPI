using A5.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using A5.Service;
using A5.Service.Interfaces;


var builder = WebApplication.CreateBuilder(args);

//Serilog
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddTransient<IOrganisationService,OrganisationService>();
builder.Services.AddTransient<IDepartmentService,DepartmentService>();
builder.Services.AddTransient<IDesignationService,DesignationService>();
builder.Services.AddTransient<IAwardTypeService,AwardTypeService>();
builder.Services.AddTransient<DashboardService>();
builder.Services.AddTransient<StatusService>();
builder.Services.AddTransient<RoleService>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<IAwardService,AwardService>();
builder.Services.AddTransient<MasterRepository>();
builder.Services.AddTransient<AwardRepository>();
builder.Services.AddTransient<IAwardTypeRepository,AwardTypeRepository>();




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "JWTToken_Auth_API", Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling =
Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddHttpLogging(httpLogging=>
{
    httpLogging.LoggingFields=Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); 
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHttpLogging();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

AppDbInitializer.Seed(app);


app.Run();
