using System.Security.Cryptography;
using System.Text;

namespace src.modelo.entidades
{
    public class Usuario
    {
        public Usuario(string nome, string email, string senha, string perfil)
        {
            DefinirNome(nome);
            DefinirEmail(email);
            DefinirPassword(senha);
            DefinirPerfilDeAcesso(perfil);
        }

        public Usuario(string email, string senha)
        {
            DefinirEmail(email);
            DefinirPassword(senha);
        }

        protected Usuario()
        {

        }

        public int Id { get; protected set; }

        public string Nome { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; private set; }

        public string PerfilDeAcesso { get; private set; }

        public PrestadorDeServico PrestadorDeServico { get; private set; }

        protected void DefinirEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email)) Email = email;
        }

        protected void DefinirNome(string nome)
        {
            if (!string.IsNullOrWhiteSpace(nome)) Nome = nome;
        }

        protected void DefinirPerfilDeAcesso(string perfil)
        {
            if (!string.IsNullOrWhiteSpace(perfil)) PerfilDeAcesso = perfil;
        }

        public void DefinirPassword(string senha)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            Password = sBuilder.ToString();
        }

        internal void AlterarPerfil(string perfilDeAcesso)
        {
            PerfilDeAcesso = perfilDeAcesso;
        }
    }
}