using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Models;
using DATA.Repositories;
using DOMAIN.Interfaces.Repositories;

namespace DATA.Repositories
{
    public class VeiculoOlxRepository : GenericRepository<VeiculoOlx>, IVeiculoOlxRepository
    {
        public VeiculoOlxRepository(AppDbContext context) : base(context)
        {
        }
    }
}
