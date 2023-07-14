using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ContasApp.Presentation.Models
{
    public class ContasEdicaoViewModel
    {
        public Guid Id { get; set; }

        [MinLength(8, ErrorMessage = "Por favor informe  no mínimo 8 caracteres")]
        [MaxLength(150, ErrorMessage = "Por favor informe no máximo 150 caracteres")]
        [Required(ErrorMessage = "Por favor, informe o nome da conta")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da conta")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da conta")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo da conta")]
        public int? Tipo { get; set; }

        [MaxLength(200, ErrorMessage = "Por favor, preencha com máximo de 200 caracteres")]
        [Required(ErrorMessage = "Por favor, preeencha as observações")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "Por favor, informe a categoria da conta")]
        public Guid? CategoriaId { get; set; }
    }
}
