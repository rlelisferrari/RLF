using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DATA.Repositories
{
    public class JogoRepository: GenericRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
