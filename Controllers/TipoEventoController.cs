using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
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
                var tipoEvento = new TipoEventoDTO
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
    }
}
