using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public LoginController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            // 1. Busca o usuário pelo e-mail e valida a senha
            var usuarioEncontrado = await _usuario.BuscarPorEmailESenha(dto.Email, dto.Senha);

            // 2. Se as credenciais forem inválidas, retorna 401
            if (usuarioEncontrado == null)
            {
                return Unauthorized("Email ou senha inválidos!");
            }

            // 3. Criar a lista de Claims
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioEncontrado.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                new Claim("nome", usuarioEncontrado.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 4. Criar a chave de segurança
            var chaveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eventos-chave-autenticacao-webapi-dev"));

            // 5. Definir o algoritmo de assinatura
            var credenciais = new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);

            // 6. Montar o token JWT
            var token = new JwtSecurityToken(
                issuer: "EventPlus.WebAPI",
                audience: "EventPlus.WebAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credenciais
            );

            // 7. Converter o token para string e retornar
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                expiracao = token.ValidTo,
                usuario = new
                {
                    idUsuario = usuarioEncontrado.IdUsuario,
                    nome = usuarioEncontrado.Nome,
                    email = usuarioEncontrado.Email
                }
            });
        }
    }
}
