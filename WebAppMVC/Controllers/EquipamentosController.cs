using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ApiCatalogo.Repository;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class EquipamentosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork uof;
        private CreateEquipamentoVM createEquipamentoVM;

        public EquipamentosController(AppDbContext context, IUnitOfWork uof)
        {
            this._context = context;
            this.uof = uof;
            createEquipamentoVM = new CreateEquipamentoVM(this._context);
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

            return View(await this.uof.EquipamentoRepository.GetAllAsyn());
        }

        // GET: Equipamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = this.uof.EquipamentoRepository.GetAllIncluding(eq => eq.EquipamentoTipoEquipamento)
                .FirstOrDefault(eqp => eqp.Id.Equals((int)id));
            
            createEquipamentoVM.Equipamento = equipamento;

            //Alterar para código LINQ
            var tipos = new List<TipoEquipamento>();
            foreach (var equipamentoTipoEquipamento in equipamento.EquipamentoTipoEquipamento)
            {
                tipos.Add(this._context.TipoEquipamento.FirstOrDefault(te => te.Id.Equals(equipamentoTipoEquipamento.TipoEquipamentoId)));
            }

            this.createEquipamentoVM.TiposEquipamento = tipos;

            if (equipamento == null)
                return NotFound();

            return View(createEquipamentoVM);
        }

        // GET: Equipamentoes/Create
        public IActionResult Create()
        {
            return View(this.createEquipamentoVM);
        }

        // POST: Equipamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] Equipamento equipamento, List<int> tipoEquipamento)
        {
            this.createEquipamentoVM.Equipamento = equipamento;
            
            if (ModelState.IsValid)
            {
                var newEq = this.uof.EquipamentoRepository.Add(equipamento);

                foreach (var tipoEq in tipoEquipamento)
                {
                    var ete = new EquipamentoTipoEquipamento();
                    ete.EquipamentoId = newEq.Id;
                    ete.TipoEquipamentoId = tipoEq;
                    this._context.EquipamentoTipoEquipamento.Add(ete);
                    this._context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            

            return View(this.createEquipamentoVM);
        }

        // GET: Equipamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = this.uof.EquipamentoRepository.GetAllIncluding(eq => eq.EquipamentoTipoEquipamento)
                .FirstOrDefault(equip => equip.Id.Equals((int) id));

            this.createEquipamentoVM.Equipamento = equipamento;

            if (equipamento == null)
                return NotFound();
            return View(this.createEquipamentoVM);
        }

        // POST: Equipamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] Equipamento equipamento, List<int> tipoEquipamento)
        {
            if (id != equipamento.Id)
                return NotFound();

            equipamento.EquipamentoTipoEquipamento = new List<EquipamentoTipoEquipamento>();
            foreach (var tipoEq in tipoEquipamento)
            {
                var ete = new EquipamentoTipoEquipamento();
                ete.EquipamentoId = equipamento.Id;
                ete.TipoEquipamentoId = tipoEq;
                equipamento.EquipamentoTipoEquipamento.Add(ete);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.uof.EquipamentoRepository.UpdateAsyn(equipamento, equipamento.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoExists(equipamento.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(this.createEquipamentoVM);
        }

        // GET: Equipamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = await this.uof.EquipamentoRepository.GetAsync((int) id);
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
            var equipamento = await this.uof.EquipamentoRepository.GetAsync(id);
            this.uof.EquipamentoRepository.Delete(equipamento);
            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoExists(int id)
        {
            return this.uof.EquipamentoRepository.GetAsync(id) != null;
        }
    }
}