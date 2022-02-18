using ApiCatalogo.Repository;
using DATA.Contexts;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class ScoutsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork uof;
        private List<ScoutGeral> scouts;
        private List<RankingScoutVM> ranking;

        public ScoutsController(AppDbContext context, IUnitOfWork uof)
        {
            this._context = context;
            this.uof = uof;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            scouts = GetAllScouts();
            return View(scouts);
        }

        public IActionResult Create()
        {
            ViewBag.Atletas = new SelectList(this._context.Atletas.OrderBy(it => it.Nome).ToList(), "Id", "Nome");
            ViewBag.Jogos = new SelectList(this._context.Jogos.OrderBy(it => it.Numero).ToList(), "Id", "Nome");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Ranking()
        {
            this.ranking = GetRanking();            
            var relatorio = new RelatorioScoutVM(this.ranking);
            return View(relatorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScoutGeral scout, string atletas, string dropDownJogo)
        {
            var idAtleta = Convert.ToInt32(atletas);
            var idJogo = Convert.ToInt32(dropDownJogo);
            if(idAtleta <= 0 || idJogo <= 0)
            {
                ViewBag.Atletas = new SelectList(this._context.Atletas, "Id", "Nome");
                ViewBag.Jogos = new SelectList(this._context.Jogos, "Id", "Nome");
                return View();
            }


            if (ModelState.IsValid)
            {
                scout.idAtleta = idAtleta;
                scout.idJogo = idJogo;
                var newEq = this.uof.ScoutGeralRepository.Add(scout);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public JsonResult GetJogosByAtletaId(int idAtleta)
        {
            var todosJogos = _context.Jogos;
            var ids = (from jogo in todosJogos
                        join scout in _context.ScoutsGerais on jogo.Id equals scout.idJogo
                        where scout.idAtleta == idAtleta
                        select jogo.Id).ToList();


            return Json(new {data= todosJogos.Where(it => !ids.ToList().Contains(it.Id)) });
        }

        private List<ScoutGeral> GetAllScouts()
        {
            return uof.ScoutGeralRepository.GetAllIncluding(scouts => scouts.atleta, scouts => scouts.jogo).OrderBy(s => s.jogo.Numero).ThenBy(s => s.atleta.Nome).ToList(); ;
        }

        private List<RankingScoutVM> GetRanking()
        {
            this.scouts = GetAllScouts();
            return (from sc in scouts
                           group sc by sc.atleta.Nome into g
                           select new RankingScoutVM
                           {
                               Atleta = g.First().atleta,
                               nGols = g.Sum(pc => pc.gol),
                               nAssistencias = g.Sum(pc => pc.assistencia),
                               nAmarelos = g.Sum(pc => pc.cartaAmarelo),
                               nVermelhos = g.Sum(pc => pc.cartaVermelho),
                           }).ToList();
        }
    }
}
