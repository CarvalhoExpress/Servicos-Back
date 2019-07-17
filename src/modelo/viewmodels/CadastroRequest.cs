using System.ComponentModel.DataAnnotations;

namespace src.modelo.viewmodels
{
    public class CadastroRequest
    {
        [Required(ErrorMessage = "Nome Obrigátorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Obrigátorio")]
        public string Email { get; set; }

        public PasswordGroup PasswordGroup { get; set; }
    }

    public class PasswordGroup
    {
        [Required(ErrorMessage = "Senha Obrigátorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Senha Obrigátorio")]
        public string ConfirmPassword { get; set; }
    }
}