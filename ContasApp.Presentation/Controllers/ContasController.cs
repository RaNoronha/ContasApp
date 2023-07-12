using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        #region Cadastro

        public IActionResult Cadastro()
        {
            ViewBag.Categorias = ObterCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContasCadastroViewModel model)
        {
           if(ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta();
                    var contarepository = new ContaRepository();
                    var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

                    conta.Id = Guid.NewGuid();
                    conta.Nome = model.Nome;
                    conta.Tipo = model.Tipo.Value;
                    conta.Valor = model.Valor.Value;
                    conta.Data = model.Data;
                    conta.Observacoes = model.Observacoes;
                    conta.CategoriaId = model.CategoriaId.Value;
                    conta.UsuarioId = usuario.Id;

                    contarepository.Cadastrar(conta);

                    TempData["Sucesso"] = "CONTA CADASTRADA COM SUCESSO!!!";
                    ModelState.Clear();

                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de cadastro, por favor verifique.";
            }

            ViewBag.Categorias = ObterCategorias();
            return View();
        }

        #endregion

        #region Consulta

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(ContasConsultaViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);
                    var contarepository = new ContaRepository();
                    var contas = contarepository.PesquisarDataUsuario(model.DataInicio, model.DataFim, usuario.Id);

                    if(contas.Count > 0)
                    {
                        ViewBag.Contas = contas;
                    }
                    else
                    {
                        TempData["MensagemAlerta"] = "Nenhuma conta foi encontrada para o período de datas selecionado";
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de consulta, por favor verifique.";
            }
            return View();
        }
        #endregion

        #region Edição

        public IActionResult Edicao()
        {
            return View();
        }
        #endregion

        private List<SelectListItem> ObterCategorias()
        {
            var lista = new List<SelectListItem>();
            var categoriarepository = new CategoriaRepository();
            var categorias = categoriarepository.TodasCategorias();

            foreach(var item in categorias)
            {
                lista.Add(new SelectListItem {Value = item.Id.ToString(), Text = item.Descricao});
            }

            return lista;
        }
    }
}
