using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private readonly ITipoEvento _tipoEvento;
        public TipoEventoController(ITipoEvento tipoEvento)
        {
            _tipoEvento = tipoEvento;
        }
  
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _tipoEvento.Listar();

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
            var tipoEventoBuscado = await
                _tipoEvento.BuscarPorId(id);
            if (tipoEventoBuscado == null)
            {
                return NotFound("Tipo de evento não encontrado");
            }
            return Ok(tipoEventoBuscado);
            {

            }
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] TipoEventoDTO dto)
        {
            var tipoEvento = new TipoEvento
            {
                TituloTipoEvento = dto.Titulo
            };

            await _tipoEvento.Cadastrar(tipoEvento);

            return StatusCode(201, tipoEvento);
            {

            }




       