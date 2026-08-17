using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;
        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }


        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _usuario.Listar();

                return Ok(tipos);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var UsuarioBuscado = await
                _usuario.BuscarPorId(id);
            if (UsuarioBuscado == null)
            {
                return NotFound("Tipo de usuário não encontrado");
            }
            return Ok(UsuarioBuscado);
        }


    
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioDTO dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,                
                Email = dto.Email,               
                Senha = dto.Senha,               
                IdTipoUsuario = dto.IdTipoUsuario 
            };
            await _usuario.Atualizar(id, usuario);

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha, //obs: criptografia dentro do repositorio
                    IdTipoUsuario = dto.IdTipoUsuario
                };
                await _usuario.Cadastrar(usuario);

                return StatusCode(201, usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

