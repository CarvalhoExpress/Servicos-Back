using System.ComponentModel.DataAnnotations;

namespace src.modelo.viewmodels
{
    public class AprovacaoRequest
    {
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public int Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string Email { get; set; }

        public bool IntencaoDeAprovacao { get; set; }
    }
}