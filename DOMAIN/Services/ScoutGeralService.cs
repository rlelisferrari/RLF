using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOMAIN.Services
{
    public class ScoutGeralService
    {
        private readonly IScoutGeralRepository ScoutGeralRepository;

        public ScoutGeralService(IScoutGeralRepository ScoutGeralRepository)
        {
            this.ScoutGeralRepository = ScoutGeralRepository;
        }

        public async Task<IEnumerable<ScoutGeral>> GetAll()
        {
            return await this.ScoutGeralRepository.GetAllAsyn();
        }

        public async Task<ScoutGeral> Get(int id)
        {
            return this.ScoutGeralRepository.Get(id);
        }

        public async Task<ScoutGeral> Add(ScoutGeral scoutGeral)
        {
            await this.ScoutGeralRepository.AddAsyn(scoutGeral);
            return scoutGeral;
        }

        public async Task<ScoutGeral> Update(ScoutGeral scoutGeral, int id)
        {
            var updated = await this.ScoutGeralRepository.UpdateAsyn(scoutGeral, id);
            return updated;
        }

        public async Task<int> Delete(int id)
        {
            var rmTotal = await this.ScoutGeralRepository.DeleteAsyn(await this.ScoutGeralRepository.GetAsync(id));
            return rmTotal;
        }

        public void Dispose()
        {
            this.ScoutGeralRepository.Dispose();
        }
    }
}
