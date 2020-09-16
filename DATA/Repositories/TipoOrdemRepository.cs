using System.Linq;
using System.Threading.Tasks;
using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace DATA.Repositories
{
    public class TipoOrdemRepository : GenericRepository<TipoOrdem>, ITipoOrdemRepository
    {
        public TipoOrdemRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<int> DeleteAsyn(TipoOrdem tipoOrdem)
        {
            var exist = await this._context.TipoOrdens.FindAsync(tipoOrdem.Id);
            if (exist == null)
                return 0;

            var ordens = await this._context.Ordens.Include(or => or.TipoOrdem)
                .Where(ord => ord.TipoOrdem.Id == tipoOrdem.Id)
                .ToListAsync();

            foreach (var item in ordens)
                this._context.Set<Ordem>().Remove(item);

            this._context.Set<TipoOrdem>().Remove(tipoOrdem);

            return await this._context.SaveChangesAsync();
        }
    }
}