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
            var model = new ContasConsultaViewModel();

            try
            {
                var ultimoDiaDoMes = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);

                model.DataInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                model.DataFim = new DateTime(DateTime.Today.Year, DateTime.Today.Month, ultimoDiaDoMes);

                var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

                var contarepository = new ContaRepository();

                ViewBag.Contas = contarepository.PesquisarDataUsuario(model.DataInicio.Value, model.DataFim.Value, usuario.Id);
            }
            catch(Exception e)
            {
                TempData["MenssagemErro"] = e.Message;
            }

            return View(model);
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
                    TempData["MensagemErro"] = e.Message;
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

        public IActionResult Edicao(Guid id)
        {
            var model = new ContasEdicaoViewModel();
            var contaRepository = new ContaRepository();
            var conta = contaRepository.PesquisaId(id);

            try
            {               
                model.Id = conta.Id;
                model.Nome = conta.Nome;
                model.Data = conta.Data;
                model.Valor = conta.Valor;
                model.Tipo = conta.Tipo;
                model.Observacoes = conta.Observacoes;
                model.CategoriaId = conta.CategoriaId;

                ViewBag.Categorias = ObterCategorias();
            }
            catch(Exception e)
            {
                TempData["MenssagemErro"] = e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(ContasEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta();
                    var contarepository = new ContaRepository();
                    var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

                    conta.Id = model.Id;
                    conta.Nome = model.Nome;
                    conta.Tipo = model.Tipo.Value;
                    conta.Valor = model.Valor.Value;
                    conta.Data = model.Data;
                    conta.Observacoes = model.Observacoes;
                    conta.CategoriaId = model.CategoriaId.Value;
                    conta.UsuarioId = usuario.Id;

                    contarepository.Alterar(conta);

                    TempData["Sucesso"] = "CONTA ALTERADA COM SUCESSO!!!";
                    
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de cadastro, por favor verifique.";
            }

            ViewBag.Categorias = ObterCategorias();

            return RedirectToAction("Consulta");
        }
        #endregion

        #region Exclusão

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var contarepository = new ContaRepository();
                var conta = contarepository.PesquisaId(id);
                contarepository.Apagar(conta);

                TempData["Sucesso"] = $"'{conta.Nome}', excluído com sucesso.";
            }
            catch (Exception e)
            {
                TempData["MenssagemErro"] = e.Message;
            }
            return RedirectToAction("Consulta");
        }

        #endregion

        #region Categorias

        private List<SelectListItem> ObterCategorias()
        {
            var lista = new List<SelectListItem>();
            var categoriarepository = new CategoriaRepository();
            var categorias = categoriarepository.TodasCategorias();

            foreach (var item in categorias)
            {
                lista.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descricao });
            }

            return lista;
        }

        #endregion

    }
}
