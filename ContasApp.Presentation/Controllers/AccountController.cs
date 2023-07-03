using Microsoft.AspNetCore.Mvc;

namespace ContasApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        #region Método da view Login

        public IActionResult Login()
        {
            return View();
        }

        #endregion

        #region Método da view Register

        public IActionResult Register()
        {
            return View();
        }

        #endregion

        #region Método da view ForgotPassword

        public IActionResult ForgotPassword()
        {
            return View();
        }

        #endregion

    }
}
