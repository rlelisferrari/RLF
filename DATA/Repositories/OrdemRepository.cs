using System.Threading.Tasks;
using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace DATA.Repositories
{
    public class OrdemRepository : GenericRepository<Ordem>, IOrdemRepository
    {
        public OrdemRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Ordem> UpdateAsyn(Ordem ordem, object key)
        {
            if (ordem == null)
                return null;
            var exist = await _context.Set<Ordem>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(ordem);
                exist.TipoOrdem = await _context.TipoOrdens.SingleOrDefaultAsync(ot => ot.Id == ordem.TipoOrdem.Id);
                await _context.SaveChangesAsync();
            }
            return exist;
        }
    }
}