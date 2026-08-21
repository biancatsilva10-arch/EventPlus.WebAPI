using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicao _instituicaoRepository;

        public InstituicaoController(IInstituicao instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _instituicaoRepository.Listar();
                return Ok(lista);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            try
            {
                var instituicao = await _instituicaoRepository.BuscarPorId(id);

                if (instituicao == null)
                {
                    return NotFound("Instituição não encontrada.");
                }

                return Ok(instituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] InstituicaoDTO dto)
        {
            try
            {
                var novaInstituicao = new Instituicao
                {
                    CNPJ = dto.CNPJ,
                    NomeFantasia = dto.NomeFantasia,
                    Endereco = dto.Endereco
                };

                await _instituicaoRepository.Cadastrar(novaInstituicao);

                return StatusCode(201, novaInstituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] InstituicaoDTO dto)
        {
            try
            {
                var instituicao = new Instituicao
                {
                    CNPJ = dto.CNPJ,
                    NomeFantasia = dto.NomeFantasia,
                    Endereco = dto.Endereco
                };

                await _instituicaoRepository.Atualizar(id, instituicao);

                return Ok(instituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _instituicaoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}