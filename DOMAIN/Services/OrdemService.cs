using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DOMAIN.Services
{
    public class OrdemService
    {
        private readonly IOrdemRepository ordemRepository;

        public OrdemService(IOrdemRepository ordemRepository)
        {
            this.ordemRepository = ordemRepository;
        }

        public async Task<IEnumerable<Ordem>> GetAll()
        {
            return await this.ordemRepository.GetAllAsyn();
        }

        public async Task<Ordem> Get(int id)
        {
            return this.ordemRepository.Get(id);
        }

        public async Task<Ordem> Add(Ordem ordem)
        {
            await this.ordemRepository.AddAsyn(ordem);
            return ordem;
        }

        public async Task<Ordem> Update(Ordem ordem, int id)
        {
            var updated = await this.ordemRepository.UpdateAsyn(ordem, id);
            return updated;
        }

        public async Task<int> Delete(int id)
        {
            var rmTotal = await this.ordemRepository.DeleteAsyn(await this.ordemRepository.GetAsync(id));
            return rmTotal;
        }

        public void Dispose()
        {
            this.ordemRepository.Dispose();
        }
    }
}