using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.modelo.data.contexto;
using src.modelo.entidades;
using src.modelo.interfaces;

namespace src.modelo.data.repositorio
{
    public class PrestadorDeServicoRepositorio : IPrestadorDeServicoRepositorio
    {
        private readonly ServicoContexto _contexto;

        public PrestadorDeServicoRepositorio(ServicoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task Atualizar(PrestadorDeServico prestadorDeServico)
        {
            _contexto.Update(prestadorDeServico);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<PrestadorDeServico>> BuscarCadastrosAtivos(string busca)
        {
            return await _contexto.PrestadorDeServico.AsNoTracking().Include(x => x.Servico).Where(x => x.Ativo && x.Servico.Descricao.Contains(busca)).ToListAsync();
        }

        public async Task<IEnumerable<PrestadorDeServico>> BuscarCadastrosPendentesDeAprovacao()
        {
            return await _contexto.PrestadorDeServico.AsNoTracking().Where(x => !x.Ativo).ToListAsync();
        }

        public async Task Gravar(PrestadorDeServico prestadorDeServico)
        {
            await _contexto.AddAsync(prestadorDeServico);
            await _contexto.SaveChangesAsync();
        }

        public async Task<PrestadorDeServico> ObterPrestadorDeServicoPorEmail(string email)
        {
            return await _contexto.PrestadorDeServico.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<PrestadorDeServico> ObterPrestadorDeServicoPorId(int id)
        {
            return await _contexto.PrestadorDeServico.Include(x => x.Usuario).Include(x => x.Servico).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Remover(PrestadorDeServico prestadorDeServico)
        {
            _contexto.Remove(prestadorDeServico);
            await _contexto.SaveChangesAsync();
        }
    }
}