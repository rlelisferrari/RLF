using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVC.Controllers
{
    public class EquipamentosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEquipamentoRepository equipamentoRepository;

        public EquipamentosController(AppDbContext context, IEquipamentoRepository equipamentoRepository)
        {
            this._context = context;
            this.equipamentoRepository = equipamentoRepository;
        }

        // GET: Equipamentoes
        public async Task<IActionResult> Index(string tipoEquipamento)
        {
            ViewBag.TipoEquipamento = new SelectList(this._context.TipoEquipamento, "Id", "Nome");

            var id = Convert.ToInt32(tipoEquipamento);
            
            if (id > 0)
            {
                var equipamentosFiltrados = this._context.Equipamentos
                    .Join(
                        this._context.EquipamentoTipoEquipamento,
                        eq => eq.Id,
                        ete => ete.EquipamentoId,
                        (eq, ete) => new {eq, ete})
                    .Join(
                        this._context.TipoEquipamento,
                        t => t.ete.TipoEquipamentoId,
                        te => te.Id,
                        (t, te) => new {t, te})
                    .Where(t => t.te.Id == id)
                    .Select(eq => eq.t)
                    .Select(eq => eq.eq);

                return View(equipamentosFiltrados);
            }
            else
                return View(await this.equipamentoRepository.GetAllAsyn());
        }

        // GET: Equipamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = await this.equipamentoRepository.GetAsync((int) id);
                
            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        // GET: Equipamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                this.equipamentoRepository.Add(equipamento);
                return RedirectToAction(nameof(Index));
            }

            return View(equipamento);
        }

        // GET: Equipamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = await this.equipamentoRepository.GetAsync((int)id);

            if (equipamento == null)
                return NotFound();
            return View(equipamento);
        }

        // POST: Equipamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] Equipamento equipamento)
        {
            if (id != equipamento.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    this.equipamentoRepository.Update(equipamento, equipamento.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoExists(equipamento.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(equipamento);
        }

        // GET: Equipamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = await this.equipamentoRepository.GetAsync((int)id);
            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        // POST: Equipamentoes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipamento = await this.equipamentoRepository.GetAsync(id);
            this.equipamentoRepository.Delete(equipamento);
            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoExists(int id)
        {
            return this.equipamentoRepository.GetAsync(id) != null;
        }
    }
}