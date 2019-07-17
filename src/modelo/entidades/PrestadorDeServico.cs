using System;

namespace src.modelo.entidades
{
    public class PrestadorDeServico
    {
        public PrestadorDeServico(string nome, string sobrenome, string email, string telefone, string cidade, int servicoId)
        {
            DefinirCidade(cidade);
            DefinirEmail(email);
            DefinirNome(nome);
            DefinirSobrenome(sobrenome);
            DefinirTelefone(telefone);
            DefinirServico(servicoId);
        }


        protected PrestadorDeServico()
        {

        }

        public int Id { get; private set; }

        public int? UsuarioId { get; private set; }

        public int ServicoId { get; private set; }

        public string Nome { get; private set; }

        public string Sobrenome { get; private set; }

        public string Email { get; private set; }

        public string Telefone { get; private set; }

        public string Cidade { get; private set; }

        public string Rua { get; private set; }

        public string Numero { get; private set; }

        public string Bairro { get; private set; }

        public string Cep { get; private set; }

        public byte[] Documento { get; private set; }

        public byte[] Selfie { get; private set; }

        public string Rg { get; private set; }

        public string Cpf { get; private set; }

        public bool Ativo { get; private set; }

        public Usuario Usuario { get; private set; }

        public Servico Servico { get; private set; }

        private void DefinirCidade(string cidade)
        {
            if (!string.IsNullOrWhiteSpace(cidade)) Cidade = cidade;
        }

        private void DefinirNome(string nome)
        {
            if (!string.IsNullOrWhiteSpace(nome)) Nome = nome;
        }

        private void DefinirSobrenome(string sobrenome)
        {
            if (!string.IsNullOrWhiteSpace(sobrenome)) Sobrenome = sobrenome;
        }

        private void DefinirEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email)) Email = email;
        }

        private void DefinirTelefone(string telefone)
        {
            if (!string.IsNullOrWhiteSpace(telefone)) Telefone = telefone;
        }

        public void AlterarSituacaoCadastral(bool intencaoDeAprovacao)
        {
            Ativo = intencaoDeAprovacao;
        }

        public void AlterarEndereco(string cep, string rua, string bairro, string numero)
        {
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Numero = numero;
        }

        public void AlterarDocumentos(byte[] documento, byte[] selfie)
        {
            Documento = documento;
            Selfie = selfie;
        }

        public void AlterarInformacoesPessoais(PrestadorDeServico objeto)
        {
            DefinirCidade(objeto.Cidade);
            DefinirEmail(objeto.Email);
            DefinirNome(objeto.Nome);
            DefinirSobrenome(objeto.Sobrenome);
            DefinirTelefone(objeto.Telefone);
        }

        public void AlterarDados(string rg, string cpf)
        {
            Rg = rg;
            Cpf = cpf;
        }

        public void DefinirUsuario(Usuario usuario)
        {
            Usuario = usuario;
            UsuarioId = usuario.Id;
        }

        public void DefinirServico(int servicoId)
        {
            ServicoId = servicoId;
        }
    }
}