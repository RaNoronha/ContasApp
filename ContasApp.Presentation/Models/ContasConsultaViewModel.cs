using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class ContasConsultaViewModel
    {
        [Required(ErrorMessage="Por favor, informe a data de início")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage ="Por favor, informe a data fim")]
        public DateTime? DataFim { get; set; }
    }
}
