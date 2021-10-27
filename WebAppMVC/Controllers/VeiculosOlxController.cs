using CrawlerOlx.OLX;
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
            CapturaOlx();
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

        public void CapturaOlx()
        {
            var CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            var filtros = System.IO.File.ReadAllText(CurrentDirectory + "\\Entradas\\filtros.txt").Split("|");
            var filtroKm = Convert.ToInt32(filtros[0]);
            var filtroValor = Convert.ToInt32(filtros[1]);
            var anoInicial = Convert.ToInt32(filtros[2]);
            var anoFinal = Convert.ToInt32(filtros[3]);
            var numeroPaginas = Convert.ToInt32(filtros[4]);
            var filtroHoras = Convert.ToInt32(filtros[5]);
            var olx = new Olx(filtroKm, filtroValor, anoInicial, anoFinal, numeroPaginas, filtroHoras);
            var hrefsOlx = olx.BuscaLinksOlx();
            //TODO: não gravar anuncios com mesmo código
            var anuncios = olx.CapturaVeiculosOlx(hrefsOlx);

            foreach (var item in anuncios)
            {
                this._context.VeiculosOlx.Add(item);
                this._context.SaveChanges();
            }
        }
    }
}
