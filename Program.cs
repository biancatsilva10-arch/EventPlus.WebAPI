using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//configuração do EFCore - Banco de dados
builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Injeção de dependencia 
//AddScoped significa que uma instamcia nova é criada por requisição http
//Isso garante que cada requisição teha seu proprio contexto isolado

builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<ITipoEvento, TipoEventoRepository>();
builder.Services.AddScoped<IInstituicao, InstituicaoRepository>();

//Registrar o serviço de controllers (mapeia automaticamente os controllers da pasta / controllers)

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
//Mapeia as rotas definidas nos Controllers com os atributos [Route]: api/[controller]

app.Run();
