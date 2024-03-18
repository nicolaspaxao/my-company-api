using CompanyAPI.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

// CONFIGURESERVICE() ---------------------------------------------
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseIISIntegration();

// Configuração para desserialização do JSON com mais de uma classe na consulta 
builder.Services.AddControllers().AddJsonOptions(options =>{
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

//Configurações do Swagger
builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1" , new OpenApiInfo { Title = "API - Minha Empresa" , Version = "1" });

    //Adiciona o campo para inserir o token
    //c.AddSecurityDefinition("Bearer" , new OpenApiSecurityScheme()
    //{
    //    Name = "Authorization" ,
    //    Type = SecuritySchemeType.ApiKey ,
    //    Scheme = "Bearer" ,
    //    BearerFormat = "JWT" ,
    //    In = ParameterLocation.Header ,
    //    Description = "Header de autorização JWT usando o esquema Bearer.\r\r\r\n" +
    //    "Informe 'Bearer'[espaço] e o seu token, \r\n\r\n Exemplo: \'Bearer 123abcdef\'"
    //});

    //Adicionar comentários nos endpoints
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory , xmlFilename));

    //Adiciona barreira para acesso ao Swagger, requerindo o token
    // c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    //    {
    //        new OpenApiSecurityScheme {
    //            Reference = new OpenApiReference {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        new string [] {}
    //    }
    //});
}
);

//1. Recuperando a string de conexão
string sqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//2. Configuração do serviço do EF no contexto para o container DI nativo 
builder.Services.AddDbContext<AppDbContext>(options =>{
    options.UseSqlServer(sqlServerConnection);
});

//Adição do CORS
builder.Services.AddCors();

// CONFIGURE() ---------------------------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI();
}

// MIDDLEWARES ---------------------------------------------
//adiciona middleware para redirecionar para https
app.UseHttpsRedirection();

// adiciona o middleware que habilita a autorização
app.UseAuthorization();

app.UseCors(options => options.AllowAnyOrigin());

// adiciona o middleware que executa o endpoint do request atual
app.MapControllers();

app.Run();
