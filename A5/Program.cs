using A5.Data;
using Microsoft.EntityFrameworkCore;
using A5.Data.Service;
using Serilog;
using A5.Data.Repository;

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
builder.Services.AddTransient<OrganisationService>();
builder.Services.AddTransient<DepartmentService>();
builder.Services.AddTransient<DesignationService>();
builder.Services.AddTransient<AwardTypeService>();
builder.Services.AddTransient<StatusService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<AwardService>();
builder.Services.AddTransient<MasterRepository>();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling =
Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

AppDbInitializer.Seed(app);

app.Run();
