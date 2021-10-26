using DATA.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Controllers
{
    public class VeiculosOlxController : Controller
    {
        private readonly AppDbContext _context;

        public VeiculosOlxController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TipoEquipamento
        public async Task<IActionResult> Index()
        {
            var veics = await _context.VeiculosOlx.ToListAsync();
            return View(veics);
        }

        // GET: TipoEquipamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculosOls = await _context.VeiculosOlx
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculosOls == null)
            {
                return NotFound();
            }

            return View(veiculosOls);
        }
    }
}
