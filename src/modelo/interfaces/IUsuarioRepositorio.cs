using System.Threading.Tasks;
using src.modelo.entidades;

namespace src.modelo.interfaces
{
    public interface IUsuarioRepositorio
    {
        Task GravarUsuario(Usuario usuario);
        
        Task Atualizar(Usuario usuario);

        Task<Usuario> BuscarUsuario(string userName, string password);

        Task<Usuario> BuscarUsuarioPorEmail(string email);
    }
}