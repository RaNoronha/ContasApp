using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContasApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Meses = ObterMeses();
            ViewBag.Anos = ObterAnos();
            return View();
        }

        private List<SelectListItem> ObterMeses()
        {
            var lista = new List<SelectListItem>();

            lista.Add(new SelectListItem { Value = "1", Text = "Janeiro" });
            lista.Add(new SelectListItem { Value = "2", Text = "Fevereiro" });
            lista.Add(new SelectListItem { Value = "3", Text = "Março" });
            lista.Add(new SelectListItem { Value = "4", Text = "Abril" });
            lista.Add(new SelectListItem { Value = "5", Text = "Maio" });
            lista.Add(new SelectListItem { Value = "6", Text = "Junho" });
            lista.Add(new SelectListItem { Value = "7", Text = "Julho" });
            lista.Add(new SelectListItem { Value = "8", Text = "Agosto" });
            lista.Add(new SelectListItem { Value = "9", Text = "Setembro" });
            lista.Add(new SelectListItem { Value = "10", Text = "Outubro" });
            lista.Add(new SelectListItem { Value = "11", Text = "Novembro" });
            lista.Add(new SelectListItem { Value = "12", Text = "Dezembro" });

            return lista;
        }

        private List<SelectListItem> ObterAnos()
        {
            var lista = new List<SelectListItem>();

            lista.Add(new SelectListItem { Value = "2021", Text = "2021" });
            lista.Add(new SelectListItem { Value = "2022", Text = "2022" });
            lista.Add(new SelectListItem { Value = "2023", Text = "2023" });
            lista.Add(new SelectListItem { Value = "2024", Text = "2024" });
            lista.Add(new SelectListItem { Value = "2025", Text = "2025" });

            return lista;
        }
    }

}
