using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class InstituicaoRepository : IInstituicao
    {
        private readonly EventContext _context;
        public InstituicaoRepository(EventContext context)
        {
            _context = context;
        }


        public Task Atualizar(Guid id, Instituicao instituicao)
        {
            throw new NotImplementedException();
        }

        public async Task Cadastrar(Instituicao instituicao)
        {
            await _context.Instituicao.AddAsync(instituicao);
            await _context.SaveChangesAsync();

        }

        public async Task Deletar(Instituicao instituicao)
        {
            throw new NotImplementedException();
        }

        Task<Instituicao?> IInstituicao.BuscarPorId(Guid id)
        {
            throw new NotImplementedException();
        }


        Task<List<Instituicao>> IInstituicao.Listar()
        {
            return _context.Instituicao.AsNoTracking().ToListAsync();
        }

        public Task Deletar(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Cadastrar(object instituicao)
        {
            throw new NotImplementedException();
        }

        internal async Task Listar()
        {
            throw new NotImplementedException();
        }

        internal async Task BuscarPorId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
