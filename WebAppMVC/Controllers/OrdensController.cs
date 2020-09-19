﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class OrdensController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IOrdemRepository ordemRepository;
        private readonly ITipoOrdemRepository tipoOrdemRepository;
        private readonly OrdemVM ordemVM;


        public OrdensController(AppDbContext context, IOrdemRepository ordemRepository)
        {
            this._context = context;
            this.ordemRepository = ordemRepository;
            this.ordemVM = new OrdemVM(context);
        }

        // GET: Ordens
        public async Task<IActionResult> Index(string tipoOrdem)
        {
            ViewBag.TipoOrdem = new SelectList(this._context.TipoOrdens, "Id", "Nome");
            var ordens = this.ordemRepository.GetAllIncluding(ordem => ordem.TipoOrdem);

            var id = Convert.ToInt32(tipoOrdem);
            if (id > 0)
                return View(ordens.Where(ord => ord.TipoOrdem.Id.Equals(id)));
            else
                return View(ordens);
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var ordem = this.ordemRepository.GetAllIncluding(ordem => ordem.TipoOrdem)
                .FirstOrDefault(ord => ord.Id.Equals(id));

            if (ordem == null)
                return NotFound();

            ViewBag.TipoOrdem = new SelectList(this._context.TipoOrdens, "Id", "Nome", ordem.TipoOrdem.Id);

            return View(ordem);
        }

        // GET: Ordens/Create
        public IActionResult Create()
        {
            ViewBag.TipoOrdem = new SelectList(this._context.TipoOrdens, "Id", "Nome");
            return View();
        }

        public IActionResult CreateByVM()
        {
            return View(this.ordemVM);
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] Ordem ordem, string tipoOrdem)
        {
            var tipoOrdemId = Convert.ToInt32(tipoOrdem);
            if (tipoOrdemId <= 0)
            {
                ViewBag.TipoOrdem = new SelectList(this._context.TipoOrdens, "Id", "Nome");
                return View();
            }

            ordem.TipoOrdem = this._context.TipoOrdens.FirstOrDefault(to => to.Id.Equals(tipoOrdemId));
            if (ModelState.IsValid)
            {
                this.ordemRepository.Add(ordem);
                return RedirectToAction(nameof(Index));
            }

            return View(ordem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByVM(Ordem ordem, string tipoOrdem)
        {
            var tipoOrdemId = Convert.ToInt32(tipoOrdem);
            if (tipoOrdemId <= 0)
                return View(this.ordemVM);

            ordem.TipoOrdem = this._context.TipoOrdens.FirstOrDefault(to => to.Id.Equals(tipoOrdemId));
            if (ModelState.IsValid)
            {
                this.ordemRepository.Add(ordem);
                return RedirectToAction(nameof(Index));
            }

            return View(this.ordemVM);
        }

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ordem = this.ordemRepository.GetAllIncluding(ordem => ordem.TipoOrdem)
                .FirstOrDefault(ord => ord.Id.Equals(id));
            if (ordem == null)
                return NotFound();

            ViewBag.TipoOrdem = new SelectList(this._context.TipoOrdens, "Id", "Nome", ordem.TipoOrdem.Id);

            return View(ordem);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] Ordem ordem)
        {
            if (id != ordem.Id)
                return NotFound();

            var tipoOrdemId = Convert.ToInt32(Request.Form["TipoOrdem"]);
            ordem.TipoOrdem = this._context.TipoOrdens.FirstOrDefault(to => to.Id.Equals(tipoOrdemId));

            if (ModelState.IsValid)
            {
                try
                {
                    await this.ordemRepository.UpdateAsyn(ordem, ordem.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdemExists(ordem.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ordem);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var ordem = await this.ordemRepository.GetAsync((int) id);
            if (ordem == null)
                return NotFound();

            return View(ordem);
        }

        // POST: Ordens/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordem = await this.ordemRepository.GetAsync(id);
            this.ordemRepository.Delete(ordem);
            return RedirectToAction(nameof(Index));
        }

        private bool OrdemExists(int id)
        {
            return this.ordemRepository.GetAsync(id) != null;
        }
    }
}