using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
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

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario();

                    usuario.Id = Guid.NewGuid();
                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = model.Senha;
                    usuario.DataHoraCriacao = DateTime.Now;

                    var usuariorepository = new UsuarioRepository();
                    usuariorepository.Cadastrar(usuario);

                    TempData["Mensagem"] = "Parabéns, sua conta de usuário foi cadastrada com sucesso!";
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

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
