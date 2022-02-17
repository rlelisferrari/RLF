using ApiCatalogo.Repository;
using DATA.Contexts;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Controllers
{
    public class ScoutsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork uof;

        public ScoutsController(AppDbContext context, IUnitOfWork uof)
        {
            this._context = context;
            this.uof = uof;
        }

        public async Task<IActionResult> Index()
        {
            //var scouts = await this.uof.ScoutGeralRepository.GetAllAsyn();
            var scouts = uof.ScoutGeralRepository.GetAllIncluding(scouts => scouts.atleta, scouts => scouts.jogo).OrderBy(s => s.jogo.Numero).ThenBy(s => s.atleta.Nome).ToList();
            return View(scouts);
        }

        public IActionResult Create()
        {
            ViewBag.Atletas = new SelectList(this._context.Atletas.OrderBy(it => it.Nome).ToList(), "Id", "Nome");
            ViewBag.Jogos = new SelectList(this._context.Jogos.OrderBy(it => it.Numero).ToList(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScoutGeral scout, string atletas, int id)
        {
            var idAtleta = Convert.ToInt32(atletas);
            var idJogo = Convert.ToInt32(id);
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
            var jogos = from jogo in _context.Jogos
                        join scout in _context.ScoutsGerais on jogo.Id equals scout.idJogo
                        where scout.idAtleta == idAtleta
                        select jogo.Id;

            var ids = jogos.ToList();

            return Json(new {data= _context.Jogos.Where(it => !jogos.ToList().Contains(it.Id)) });
        }
    }
}
