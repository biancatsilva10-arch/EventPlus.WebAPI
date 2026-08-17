using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//configuração do EFCore - Banco de dados
builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    //corta o ciclo do usuario -> TipoUsuario -> Usuario ->....
    //colocando um null no ponto onde a referencia se repete
    { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

//Injeção de dependencia 
//AddScoped significa que uma instamcia nova é criada por requisição http
//Isso garante que cada requisição teha seu proprio contexto isolado

builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<ITipoEvento, TipoEventoRepository>();
builder.Services.AddScoped<IInstituicao, InstituicaoRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
//Registrar o serviço de controllers (mapeia automaticamente os controllers da pasta / controllers)

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
//Mapeia as rotas definidas nos Controllers com os atributos [Route]: api/[controller]

app.Run();
