using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoEventoRepository : ITipoEvento
    {
        private readonly EventContext _context;

        public TipoEventoRepository(EventContext context)
        {
            _context = context;
        }
        //  guid id : id do objeto buscado
        // tipoEvento novotipo : objeto com as novas informações 
        // tipoEveto : classe 
        // novoTipo: objeto de classe
        //tituloTipoEvento = propriedade do obejto
        public async Task Atualizar(Guid id, TipoEvento novoTipo)
        {
           var tipoBuscado = await _context.TipoEvento.FindAsync(id);
            if (tipoBuscado != null)
            {
                tipoBuscado.TituloTipoEvento = novoTipo.TituloTipoEvento;
            }
           await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(TipoEvento tipoEvento)
        {
           await _context.TipoEvento.AddAsync(tipoEvento);
           await _context.SaveChangesAsync();

        }

       
       public async Task<TipoEvento?> BuscarPorId(Guid id)
        {
            return await _context.TipoEvento.FirstOrDefaultAsync(t => t.IdTipoEvento == id);
        }


        Task<List<TipoEvento>> ITipoEvento.Listar()
        {
            return _context.TipoEvento.AsNoTracking().ToListAsync();
        }

        public async Task Deletar(Guid id)
        {
            var tipoBuscado = await _context.TipoEvento.FindAsync(id);

            if (tipoBuscado != null)
            {
                _context.TipoEvento.Remove(tipoBuscado);
            }

            await _context.SaveChangesAsync();

        }

      
    }
}