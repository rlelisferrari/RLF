using ApiCatalogo.Repository;
using DATA.Contexts;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAppMVC.Controllers
{
    public class JogosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork uof;

        public JogosController(AppDbContext context, IUnitOfWork uof)
        {
            this._context = context;
            this.uof = uof;
        }

        public async Task<IActionResult> Index()
        {
            var jogos = await this.uof.JogoRepository.GetAllAsyn();
            return View(jogos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                var newEq = this.uof.JogoRepository.Add(jogo);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
