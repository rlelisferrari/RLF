using System.Threading.Tasks;
using ApiCatalogo.Repository;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVC.Controllers
{
    public class TipoOrdemController : Controller
    {
        private readonly IUnitOfWork uof;

        public TipoOrdemController(IUnitOfWork uof)
        {
            this.uof = uof;
        }

        // GET: TipoOrdem
        public async Task<IActionResult> Index()
        {
            return View(await this.uof.TipoOrdemRepository.GetAllAsyn());
        }

        // GET: TipoOrdem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var tipoOrdem = await this.uof.TipoOrdemRepository.GetAsync((int) id);
            if (tipoOrdem == null)
                return NotFound();

            return View(tipoOrdem);
        }

        // GET: TipoOrdem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoOrdem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] TipoOrdem tipoOrdem)
        {
            if (ModelState.IsValid)
            {
                this.uof.TipoOrdemRepository.Add(tipoOrdem);
                return RedirectToAction(nameof(Index));
            }

            return View(tipoOrdem);
        }

        // GET: TipoOrdem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tipoOrdem = await this.uof.TipoOrdemRepository.GetAsync((int) id);
            if (tipoOrdem == null)
                return NotFound();
            return View(tipoOrdem);
        }

        // POST: TipoOrdem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] TipoOrdem tipoOrdem)
        {
            if (id != tipoOrdem.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    this.uof.TipoOrdemRepository.Update(tipoOrdem, tipoOrdem.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoOrdemExists(tipoOrdem.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(tipoOrdem);
        }

        // GET: TipoOrdem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tipoOrdem = await this.uof.TipoOrdemRepository.GetAsync((int) id);
            if (tipoOrdem == null)
                return NotFound();

            return View(tipoOrdem);
        }

        // POST: TipoOrdem/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoOrdem = await this.uof.TipoOrdemRepository.GetAsync(id);
            await this.uof.TipoOrdemRepository.DeleteAsyn(tipoOrdem);
            return RedirectToAction(nameof(Index));
        }

        private bool TipoOrdemExists(int id)
        {
            return this.uof.TipoOrdemRepository.Get(id) != null;
        }
    }
}