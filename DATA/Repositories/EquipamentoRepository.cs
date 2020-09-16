using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DATA.Repositories
{
    public class EquipamentoRepository : GenericRepository<Equipamento>, IEquipamentoRepository
    {
        public EquipamentoRepository(AppDbContext context) : base(context)
        {
        }
    }
}