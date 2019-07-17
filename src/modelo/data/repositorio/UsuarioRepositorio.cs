using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.modelo.data.contexto;
using src.modelo.entidades;
using src.modelo.interfaces;

namespace src.modelo.data.repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ServicoContexto _contexto;

        public UsuarioRepositorio(ServicoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task GravarUsuario(Usuario usuario)
        {
            await _contexto.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Usuario> BuscarUsuario(string userName, string password)
        {
            return await _contexto.Usuario.FirstOrDefaultAsync(x => x.Email.Equals(userName) && x.Password.Equals(password));
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            return await _contexto.Usuario.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task Atualizar(Usuario usuario)
        {
            await _contexto.AddAsync(usuario);
            _contexto.Update(usuario);
        }
    }
}