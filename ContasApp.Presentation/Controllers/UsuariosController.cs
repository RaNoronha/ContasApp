using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(UsuarioAlterarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

                    if (usuarioRepository.PesquisaEmaileSenha(usuario.Email, model.SenhaAtual) != null)
                    {
                        usuarioRepository.Atualizar(usuario.Id, model.NovaSenha);

                        TempData["Sucesso"] = "Senha de acesso atualizada com sucesso";
                    }
                    else
                    {
                        TempData["MensagemAlerta"] = "Senha atual inválida, por favor verifique";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }

            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de consulta, por favor verifique.";
            }
            return View();

        }
    }
}
