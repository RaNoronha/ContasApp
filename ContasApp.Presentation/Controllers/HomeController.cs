using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        #region Métodos index
        [Authorize]
        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            model.mes = DateTime.Now.Month;
            model.ano = DateTime.Now.Year;

            try
            {
                ViewBag.Meses = ObterMeses();
                ViewBag.Anos = ObterAnos();
                ViewBag.ReceitasEDespesas = ObterReceitasEDespesas(model.mes.Value, model.ano.Value);
                ViewBag.TotalCategorias = ObterTotalCategorias(model.mes.Value, model.ano.Value);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }


            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DashboardViewModel model)
        {           
            try
            {
                ViewBag.Meses = ObterMeses();
                ViewBag.Anos = ObterAnos();
                ViewBag.ReceitasEDespesas = ObterReceitasEDespesas(model.mes.Value, model.ano.Value);
                ViewBag.TotalCategorias = ObterTotalCategorias(model.mes.Value, model.ano.Value);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }
        #endregion

        #region Métodos do filtro de mês e ano
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
        #endregion

        #region Métodos dos gráficos
        private List<object> ObterReceitasEDespesas(int mes, int ano)
        {
            var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);
            var contarepository = new ContaRepository();

            var qtdDiasMes = DateTime.DaysInMonth(ano, mes);
            var dataInicio = new DateTime(ano, mes, 1);
            var dataFim = new DateTime(ano, mes, qtdDiasMes);

            var contas = contarepository.PesquisarDataUsuario(dataInicio, dataFim, usuario.Id);

            var lista = new List<object>();

            lista.Add(new { Nome = "Total de Contas a Receber", Valor = contas.Where(c => c.Tipo == 1).Sum(c => c.Valor) });
            lista.Add(new { Nome = "Total de Contas a Pagar", Valor = contas.Where(c => c.Tipo == 2).Sum(c => c.Valor) });

            return lista;
        }

        public List<object> ObterTotalCategorias(int mes, int ano)
        {
            var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);
            var contarepository = new ContaRepository();

            var qtdDiasMes = DateTime.DaysInMonth(ano, mes);
            var dataInicio = new DateTime(ano, mes, 1);
            var dataFim = new DateTime(ano, mes, qtdDiasMes);

            var contas = contarepository.PesquisarDataUsuario(dataInicio, dataFim, usuario.Id);

            var lista = contas.GroupBy(conta => conta.Categoria.Descricao).Select(grupo => new { Categoria = grupo.Key, Total = grupo.Sum(conta => conta.Valor) }).ToList();

            return lista.Cast<object>().ToList();

        }

        #endregion
    }
}
