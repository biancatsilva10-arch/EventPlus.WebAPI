using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//configuração do EFCore - Banco de dados
builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Injeção de dependencia 
//AddScoped significa que uma instamcia nova é criada por requisição http
//Isso garante que cada requisição teha seu proprio contexto isolado

builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
