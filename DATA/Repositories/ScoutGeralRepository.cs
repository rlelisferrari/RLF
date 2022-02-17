using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DATA.Repositories
{
    public class ScoutGeralRepository : GenericRepository<ScoutGeral>, IScoutGeralRepository
    {
        public ScoutGeralRepository(AppDbContext context) : base(context)
        {
        }
    }
}
