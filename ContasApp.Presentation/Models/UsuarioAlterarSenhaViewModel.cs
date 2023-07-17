using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class UsuarioAlterarSenhaViewModel
    {
        [Required(ErrorMessage ="Por favor, entra com senha atual")]
        public string? SenhaAtual { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)(?!.*\s).{8,}$", ErrorMessage = "Informe uma senha com letras maíscula, letras minísculas, números, símbolos e no mínimo 8 dígitos")]
        [Required(ErrorMessage ="Pro favor, informe a nova senha")]
        public string? NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem, por favor verifique")]
        [Required(ErrorMessage = "Por favor, confirme a senha")]
        public string? NovaSenhaConfirmacao { get; set; }
    }
}
