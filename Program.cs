using AutoMapper;
using CompanyAPI.DbContext;
using CompanyAPI.Logging;
using CompanyAPI.Mappings;
using CompanyAPI.Repositories.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseIISIntegration();

builder.Services.AddCors();

// Configuração para desserialização do JSON com mais de uma classe na consulta 
builder.Services.AddControllers().AddJsonOptions(options =>{
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//Configurações do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1" , new OpenApiInfo { 
        Title = "API Estudo - Minha Empresa" ,
        Version = "1" 
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory , xmlFile);
    if (!File.Exists(xmlPath))
        File.Create(xmlPath);
    c.IncludeXmlComments(xmlPath);
}
);

//AutoMapper
var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
IMapper mapper = mappingConfig.CreateMapper();

//Services
builder.Services.AddScoped<IUnitOfWorks , UnitOfWorks>();
builder.Services.AddSingleton(mapper);

//Conexão com o SGBD
builder.Services.AddDbContext<AppDbContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddProvider(new CostumerLoggerProvider(new CostumerLoggerProviderConfiguration {
    logLevel = LogLevel.Information
}));

if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseAuthorization();

var cultureInfo = new CultureInfo("pt-BR");
cultureInfo.NumberFormat.CurrencySymbol = "R$";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.MapControllers();

app.Run();
