using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATA.Contexts;
using DOMAIN.Models;

namespace WebAppMVC.Controllers
{
    public class TipoEquipamentosController : Controller
    {
        private readonly AppDbContext _context;

        public TipoEquipamentosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TipoEquipamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoEquipamento.ToListAsync());
        }

        // GET: TipoEquipamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEquipamento = await _context.TipoEquipamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEquipamento == null)
            {
                return NotFound();
            }

            return View(tipoEquipamento);
        }

        // GET: TipoEquipamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEquipamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] TipoEquipamento tipoEquipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEquipamento);
        }

        // GET: TipoEquipamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEquipamento = await _context.TipoEquipamento.FindAsync(id);
            if (tipoEquipamento == null)
            {
                return NotFound();
            }
            return View(tipoEquipamento);
        }

        // POST: TipoEquipamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] TipoEquipamento tipoEquipamento)
        {
            if (id != tipoEquipamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEquipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEquipamentoExists(tipoEquipamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEquipamento);
        }

        // GET: TipoEquipamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEquipamento = await _context.TipoEquipamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEquipamento == null)
            {
                return NotFound();
            }

            return View(tipoEquipamento);
        }

        // POST: TipoEquipamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoEquipamento = await _context.TipoEquipamento.FindAsync(id);
            _context.TipoEquipamento.Remove(tipoEquipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEquipamentoExists(int id)
        {
            return _context.TipoEquipamento.Any(e => e.Id == id);
        }
    }
}
