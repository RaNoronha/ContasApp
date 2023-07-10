using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ContasApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        #region Método da view Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuariorepository = new UsuarioRepository();

                    if(usuariorepository.PesquisaEmaileSenha(model.Email, model.Senha) != null)
                    {
                        var json = JsonConvert.SerializeObject(usuariorepository.PesquisaEmaileSenha(model.Email, model.Senha));
                        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, json) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Mensagem"] = "ACESSO NEGADO!! Email e/ou Senha inválidos";
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }
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
                    var usuariorepository = new UsuarioRepository();
                    if(usuariorepository.PesquisaEmail(model.Email) != null)
                    {
                        TempData["Mensagem"] = "Email já cadastrado!!";
                    }
                    else
                    {
                        usuario.Id = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;
                        usuario.DataHoraCriacao = DateTime.Now;

                        usuariorepository.Cadastrar(usuario);

                        TempData["Mensagem"] = "Parabéns, sua conta de usuário foi cadastrada com sucesso!";
                    }
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

        #region Logout

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        #endregion

    }
}
