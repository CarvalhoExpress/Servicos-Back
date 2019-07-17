using System.ComponentModel.DataAnnotations;

namespace src.modelo.viewmodels
{
    public class PreCadastroRequest
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cidade { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Servico { get; set; }
    }
}