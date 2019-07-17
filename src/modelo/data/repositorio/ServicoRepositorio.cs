using src.modelo.data.contexto;
using src.modelo.interfaces;

namespace src.modelo.data.repositorio
{
    public class ServicoRepositorio : IServicoRepositorio
    {
        private readonly ServicoContexto contexto;

        public ServicoRepositorio(ServicoContexto contexto)
        {
            this.contexto = contexto;
        }
    }
}