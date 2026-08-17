using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces
{

    /// <summary>
    /// Interface o repositorio para a entidade TipoEvento
    /// Contrato tipoevento, metodos deverao ser implementados dentro do repositorio
    /// 
    /// </summary>
    public interface ITipoEvento
    {
        Task Cadastrar(TipoEvento tipoEvento);

        Task Atualizar(Guid id, TipoEvento tipoEvento);

        Task Deletar(Guid id);

        Task<List<TipoEvento>> Listar();

        Task<TipoEvento?> BuscarPorId(Guid id);
       
    }
}