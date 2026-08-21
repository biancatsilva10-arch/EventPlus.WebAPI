using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
builder.Services.AddScoped<IInstiuicao, TipoEventoRepository>();
builder.Services.AddScoped<IInstituicao, InstituicaoRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();


//AUTENTICAÇÃO Jwt
//Configura como a API vai validar os tokens recebidos nas requisições
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem emitiu o token
        ValidateIssuer = true,
        ValidIssuer = "EventPlus.WebAPI",

        //valida para quem o token foi emitido
        ValidateAudience = true,
        ValidAudience = "EventPlus.WebAPI",
        //valida se o token ainda está dentro do prazo de validade 
        ValidateLifetime = true,
        //define a tolerancia de clock entre servidores
        ClockSkew = TimeSpan.FromMinutes(5),
        //chave secreta utilizada para validar a assinatur do token
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("eventos-chave-autenticacao-webapi-dev")
        )

    };
});

//registra o serviço de autorizaçao mapeia automaticamnete os controllers da pasta
builder.Services.AddAuthorization();

//Registra o serviço de controller(mapeia automaticamente os controllers da pasta/Controllers)
builder.Services.AddControllers();

var app = builder.Build();
 
//mapeia as rotas definidas no controllers com os atributos route 

// redireciona http para https automaticamente
app.UseHttpsRedirection();

//ativa autenticação
app.UseAuthentication();

//ativa autorizaçao 
app.UseAuthorization();


//Mapeia as rotas definidas nos Controllers com os atributos [Route]: api/[controller]
app.MapControllers();


app.Run();
