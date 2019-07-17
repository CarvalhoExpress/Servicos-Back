using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace src.modelo.viewmodels
{
    public class FinalizarSolicitacaoRequest
    {
        [EmailAddress]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public IFormFile Documento { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public IFormFile Selfie { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cpf { get; set; }
    }
}