using System.Collections.Generic;

namespace src.modelo.entidades
{
    public class Servico
    {
        public Servico(string descricao)
        {
            Descricao = descricao;
            Ativo = true;
        }

        protected Servico()
        {

        }

        public int Id { get; protected set; }

        public string Descricao { get; protected set; }

        public bool Ativo { get; protected set; }

        public ICollection<PrestadorDeServico> PrestadoresDeServicos { get; private set; }
    }
}