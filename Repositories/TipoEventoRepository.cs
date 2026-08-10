using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoEventoRepository : ITipoEvento
    {
        private readonly EventContext _context;
        public TipoEventoRepository(EventContext contexto)
        {
            _context = contexto;

        }

        public async Task Atualizar(Guid id, TipoEvento tipoEvento)
        {
            var tipoEventoBuscado = await _context.TipoEvento.FindAsync(id);
            if (tipoEventoBuscado != null)
            {
                tipoEventoBuscado.TituloTipoEvento = tipoEvento.TituloTipoEvento;
                _context.TipoEvento.Update(tipoEventoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TipoEvento?> BuscarPorId(Guid id)
        {
            return await _context.TipoEvento.FirstOrDefaultAsync(t => t.IdTipoEvento == id);
        }

        public async Task Cadastrar(TipoEvento tipoEvento)
        {
            await _context.TipoEvento.AddAsync(tipoEvento);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var tipoEventoBuscado = await _context.TipoUsuario.FindAsync(id);
            if (tipoEventoBuscado != null)
            {
                _context.TipoUsuario.Remove(tipoEventoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TipoEvento>> Listar()
        {
            return await _context.TipoEvento.AsNoTracking().ToListAsync();


        }
    }
}
