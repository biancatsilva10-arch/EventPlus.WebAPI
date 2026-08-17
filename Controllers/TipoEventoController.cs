using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")] //http://localhost:7098/api/TipoEvento 
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private readonly ITipoEvento _tipoEvento;
        public TipoEventoController(ITipoEvento tipoEvento)
        {
            _tipoEvento = tipoEvento;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(TipoEventoDTO dto)
        {
            try
            {
                var tipoEvento = new TipoEvento
                {
                    TituloTipoEvento = dto.TituloTipoEvento
                };

                await _tipoEvento.Cadastrar(tipoEvento);

                return StatusCode(201, tipoEvento);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }




        }



        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                //o que esperamos que de certo

                var tipos = await _tipoEvento.Listar();
                return Ok(tipos);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                //se der errado, não vai quebrar nosso codigo, vai ter um tratamento de erro
                throw;
            }

        }

        // a/pi/TipoEvento/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {

            try
            {
                var tipo = await _tipoEvento.BuscarPorId(id);

                if (tipo == null)
                {
                    return NotFound();

                }
                return Ok(tipo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] TipoEventoDTO dto)
        {
            try
            {
                var tipoEvento = new TipoEvento
                {

                    TituloTipoEvento = dto.TituloTipoEvento
                };

                await _tipoEvento.Atualizar(id, tipoEvento);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary></summary>
        /// Remove uma categoria de evento
        /// <param name="id">Id do objeto a ser excluido 
        /// </param>
        /// <returns> Status COde NoContente der certo e 400 caso haja exceção
        /// </returns>

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _tipoEvento.Deletar(id);
                return NoContent();
            }
            catch (Exception e)
            {
                {
                    return BadRequest(e.Message);
                }
            }

        }
    }
}
