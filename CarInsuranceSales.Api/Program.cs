using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.DocumentDataProviders;
using CarInsuranceSales.Interfaces;
using CarInsuranceSales.PolicyProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDocumentDataProvider, MindeeDocumentDataProvider>();
builder.Services.AddScoped<IPolicyProvider, FakePolicyProvider>();

builder.Services.Configure<AppOptions>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
