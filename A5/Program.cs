using A5.Data;
using Microsoft.EntityFrameworkCore;
using A5.Data.Service;
using Serilog;
using A5.Data.Repository;
using System.Reflection;
using A5.Data.Service.Interfaces;

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
builder.Services.AddTransient<DepartmentService>();
builder.Services.AddTransient<DesignationService>();
builder.Services.AddTransient<AwardTypeService>();
builder.Services.AddTransient<StatusService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<IAwardService,AwardService>();
builder.Services.AddTransient<MasterRepository>();
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
// builder.Services.AddSwaggerGen();
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
//AppDbInitializer.SeedRolesAsync(app).Wait();

app.Run();
