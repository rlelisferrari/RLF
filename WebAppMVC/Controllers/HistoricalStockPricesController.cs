using DATA.Contexts;
using EOD.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class HistoricalStockPricesController : Controller
    {
        private readonly AppDbContext _context;
        private B3ApiService _b3ApiService;

        public HistoricalStockPricesController(AppDbContext context)
        {
            _context = context;
            _b3ApiService = new B3ApiService("6328875c73c412.21853345");
        }

        // GET: TipoEquipamento
        public async Task<IActionResult> Index(string NomeAcao, DateTime dataInicio, DateTime dataFim)
        {
            var acoes = new List<string>() { "VALE3.SA", "PETR4.SA" };
            ViewBag.NomeAcao = new SelectList(acoes,"Nome");
            ViewBag.Inicio = dataInicio < new DateTime(1800,1,1) ? DateTime.Now.AddMonths(-1): dataInicio;
            ViewBag.Fim = dataFim < new DateTime(1800,1,1) ? DateTime.Now: dataFim;
            if (!string.IsNullOrEmpty(NomeAcao))
            {
                var cotacoes = await _b3ApiService.ApiConsumer(NomeAcao, dataInicio, dataFim, 0);
                return View(cotacoes.OrderByDescending(it => it.Date));
            }

            return View();
        }

        // GET: TipoEquipamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            return View();
        }
    }
}
