using DATA.Contexts;
using EOD.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMVC.Auxiliar;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class HistoricalStockPricesController : Controller
    {
        private readonly AppDbContext _context;
        private B3ApiService _b3ApiService;
        private readonly ILogger<B3ApiService> _loggerApi;

        public HistoricalStockPricesController(AppDbContext context, ILogger<B3ApiService> _loggerApi)
        {
            _context = context;
            _b3ApiService = new B3ApiService("6328875c73c412.21853345", _loggerApi);
        }

        // GET: TipoEquipamento
        public async Task<IActionResult> Index(string NomeAcao, DateTime dataInicio, DateTime dataFim)
        {
            var acoes = new Parametros().Ativos();
            ViewBag.NomeAcao = new SelectList(acoes,"Nome");
            ViewBag.Inicio = dataInicio < new DateTime(1800,1,1) ? DateTime.Now.AddMonths(-1): dataInicio;
            ViewBag.Fim = dataFim < new DateTime(1800,1,1) ? DateTime.Now: dataFim;
            if (!string.IsNullOrEmpty(NomeAcao))
            {
                var cotacoes = await _b3ApiService.GetIntraday(NomeAcao, dataInicio, dataFim, 1);
                return View(cotacoes.OrderByDescending(it => it.DateTime));
            }

            return View();
        }
    }
}
