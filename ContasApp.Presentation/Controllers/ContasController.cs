using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        #region Cadastro

        public IActionResult Cadastro()
        {
            return View();
        }

        #endregion

        #region Consulta

        public IActionResult Consulta()
        {
            return View();
        }

        #endregion

        #region Edição

        public IActionResult Edicao()
        {
            return View();
        }
        #endregion

    }
}
