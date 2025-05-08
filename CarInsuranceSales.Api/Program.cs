using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.DataAccess;
using CarInsuranceSales.DataAccess.Configuration;
using CarInsuranceSales.DataAccess.Repository;
using CarInsuranceSales.DocumentDataProviders;
using CarInsuranceSales.Interfaces;
using CarInsuranceSales.PolicyProviders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDocumentDataProvider, MindeeDocumentDataProvider>();
builder.Services.AddScoped<IPolicyProvider, OpenAiPolicyProvider>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddDbContext<ApplicationContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<AppOptions>(builder.Configuration);
builder.Services.Configure<DbOptions>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
