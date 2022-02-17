using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DATA.Repositories
{
    public class AtletaRepository : GenericRepository<Atleta>, IAtletaRepository
    {
        public AtletaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
