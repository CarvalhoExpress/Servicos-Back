using System.Collections.Generic;
using System.Threading.Tasks;
using src.modelo.entidades;

namespace src.modelo.interfaces
{
    public interface IPrestadorDeServicoRepositorio
    {
        Task<IEnumerable<PrestadorDeServico>> BuscarCadastrosPendentesDeAprovacao();
        
        Task<IEnumerable<PrestadorDeServico>> BuscarCadastrosAtivos(string busca);

        Task<PrestadorDeServico> ObterPrestadorDeServicoPorEmail(string email);
        
        Task<PrestadorDeServico> ObterPrestadorDeServicoPorId(int id);
        
        Task Gravar(PrestadorDeServico prestadorDeServico);
        
        Task Atualizar(PrestadorDeServico usuarioNoBanco);
        
        Task Remover(PrestadorDeServico prestadorDeServico);
    }
}