using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage = "Por favor, informe o email")]
        [EmailAddress(ErrorMessage = "Por favor, informe um email válido")]
        public string? Email{ get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha")]
        public string? Senha { get; set; }
    }
}
