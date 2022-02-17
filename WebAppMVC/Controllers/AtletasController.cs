using ApiCatalogo.Repository;
using DATA.Contexts;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAppMVC.Controllers
{
    public class AtletasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork uof;

        public AtletasController(AppDbContext context, IUnitOfWork uof)
        {
            this._context = context;
            this.uof = uof;
        }

        public async Task<IActionResult> Index()
        {
            var atletas = await this.uof.AtletaRepository.GetAllAsyn();
            return View(atletas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Atleta atleta)
        {
            if (ModelState.IsValid)
            {
                var newEq = this.uof.AtletaRepository.Add(atleta);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
