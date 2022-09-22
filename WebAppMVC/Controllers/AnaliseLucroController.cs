using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAppMVC.Auxiliar;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class AnaliseLucroController : Controller
    {
        private B3ApiService _b3ApiService;
        private readonly ILogger<AnaliseLucroController> _logger;
        private Dictionary<string, RelatorioLucroAtivo> _consolidacaoAtivos;
        private readonly ILogger<B3ApiService> _loggerApi;

        public AnaliseLucroController(ILogger<AnaliseLucroController> logger, ILogger<B3ApiService> _loggerApi)
        {
            _logger = logger;
            _consolidacaoAtivos = new Dictionary<string, RelatorioLucroAtivo>();
            _b3ApiService = new B3ApiService("6328875c73c412.21853345", _loggerApi);
        }

        // GET: TipoEquipamento
        public async Task<IActionResult> Index(string NomeAcao,string Desagio, DateTime dataInicio, DateTime dataFim, DateTime horaInicio, DateTime horaFim)
        {
            InicializaFiltros(NomeAcao, Desagio, dataInicio, dataFim, horaInicio, horaFim);

            if (!string.IsNullOrEmpty(NomeAcao))
            {
                var cotacoes = await _b3ApiService.GetIntraday(NomeAcao, dataInicio, dataFim, 1);
                var relatorioAtivo = _b3ApiService.AnaliseLucroPorAtivo(cotacoes, NomeAcao, ConvertStringToFloat(Desagio), dataInicio,dataFim, horaInicio, horaFim);
                if (cotacoes == null || relatorioAtivo == null || relatorioAtivo.cotacoesIntraDay == null)
                {
                    ViewBag.Error = "Error"; 
                    return View();
                }
                return View(relatorioAtivo.cotacoesIntraDay);
            }

            return View();
        }

        // GET: TipoEquipamento
        public async Task<IActionResult> LucroResumido(string NomeAcao, string Desagio, DateTime dataInicio, DateTime dataFim, DateTime horaInicio, DateTime horaFim)
        {
            InicializaFiltros(NomeAcao, Desagio, dataInicio, dataFim, horaInicio, horaFim);

            if (!string.IsNullOrEmpty(NomeAcao))
            {
                var cotacoes = await _b3ApiService.GetIntraday(NomeAcao, dataInicio, dataFim, 1);
                var relatorioAtivo = _b3ApiService.AnaliseLucroPorAtivoResumo(cotacoes, NomeAcao, ConvertStringToFloat(Desagio), dataInicio, dataFim, horaInicio, horaFim);
                if (cotacoes == null || relatorioAtivo == null || relatorioAtivo.cotacoesIntraDay == null)
                {
                    ViewBag.Error = "Error";
                    return View();
                }
                return View(relatorioAtivo);
            }

            return View();
        }

        public async Task<IActionResult> ConsolidacaoAtivos(string Desagio, DateTime dataInicio, DateTime dataFim, DateTime horaInicio, DateTime horaFim)
        {        
            InicializaFiltros("", Desagio, dataInicio, dataFim, horaInicio, horaFim);

            if(!string.IsNullOrEmpty(Desagio))
            {
                try
                {
                    _consolidacaoAtivos = new Dictionary<string, RelatorioLucroAtivo>();

                    foreach (var item in new Parametros().Ativos())
                    {
                        string ativo = item;
                        await GeraConsilidacao(ativo, dataInicio, dataFim, Desagio, horaInicio, horaFim);
                    }
                    
                    //ativo = "ABCB4.SA";
                    //await GeraConsilidacao(ativo, dataInicio, dataFim, Desagio, horaInicio, horaFim);

                    //ativo = "AGRO3.SA";
                    //await GeraConsilidacao(ativo, dataInicio, dataFim, Desagio, horaInicio, horaFim);

                    return View(_consolidacaoAtivos);
                }
                catch (Exception)
                {
                    ViewBag.Error = "Error";
                    return View();
                    throw;
                }
            }

            return View();
        }

        public void InicializaFiltros(string NomeAcao, string Desagio, DateTime dataInicio, DateTime dataFim, DateTime horaInicio, DateTime horaFim)
        {
            var acoes = new Parametros().Ativos();
            var desagios = new Parametros().Desagios();
            var date = DateTime.Now;
            ViewBag.NomeAcao = new SelectList(acoes, "Nome");
            ViewBag.Desagio = new SelectList(desagios, "Desagio");
            ViewBag.Inicio = dataInicio < new DateTime(1800, 1, 1) ? DateTime.Now.AddDays(-1) : dataInicio;
            ViewBag.Fim = dataFim < new DateTime(1800, 1, 1) ? DateTime.Now : dataFim;
            ViewBag.HoraInicio = horaInicio < new DateTime(1800, 1, 1) ? new DateTime(date.Year, date.Month,date.Day,13,0,0) : horaInicio;
            ViewBag.HoraFim = horaFim < new DateTime(1800, 1, 1) ? new DateTime(date.Year, date.Month, date.Day, 20, 0, 0) : horaFim;
            ViewBag.Error = "";
        }

        public float ConvertStringToFloat(string desagio)
        {
            var stringOK = float.TryParse(desagio, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out float result);
            return stringOK ? result : 0.2f;
        }

        private async Task GeraConsilidacao(string ativo, DateTime dataInicio, DateTime dataFim, string desagio, DateTime horaInicio, DateTime horaFim)
        {
            var cotacoes = await _b3ApiService.GetIntraday(ativo, dataInicio, dataFim, 1);
            if (cotacoes == null || cotacoes.Count == 0)
                this._logger.LogInformation($"API não retorna ativo: {ativo}");
            else
            {
                var relatorioAtivo = _b3ApiService.AnaliseLucroPorAtivoResumo(cotacoes, ativo, ConvertStringToFloat(desagio), dataInicio, dataFim, horaInicio, horaFim);
                _consolidacaoAtivos[ativo] = relatorioAtivo;
            }
        }
    }
}
