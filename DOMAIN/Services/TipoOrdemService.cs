using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DOMAIN.Services
{
    public class TipoOrdemService
    {
        private readonly ITipoOrdemRepository tipoOrdemRepository;

        public TipoOrdemService(ITipoOrdemRepository tipoOrdemRepository)
        {
            this.tipoOrdemRepository = tipoOrdemRepository;
        }

        public async Task<IEnumerable<TipoOrdem>> GetAll()
        {
            return await this.tipoOrdemRepository.GetAllAsyn();
        }

        public async Task<TipoOrdem> Get(int id)
        {
            return this.tipoOrdemRepository.Get(id);
        }

        public async Task<TipoOrdem> Add(TipoOrdem TipoOrdem)
        {
            await this.tipoOrdemRepository.AddAsyn(TipoOrdem);
            return TipoOrdem;
        }

        public async Task<TipoOrdem> Update(TipoOrdem TipoOrdem, int id)
        {
            var updated = await this.tipoOrdemRepository.UpdateAsyn(TipoOrdem, id);
            return updated;
        }

        public async Task<int> Delete(int id)
        {
            var rmTotal = await this.tipoOrdemRepository.DeleteAsyn(await this.tipoOrdemRepository.GetAsync(id));
            return rmTotal;
        }

        public void Dispose()
        {
            this.tipoOrdemRepository.Dispose();
        }
    }
}