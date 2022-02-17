using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOMAIN.Services
{
    public class AtletaService
    {
        private readonly IAtletaRepository atletaRepository;

        public AtletaService(IAtletaRepository atletaRepository)
        {
            this.atletaRepository = atletaRepository;
        }

        public async Task<IEnumerable<Atleta>> GetAll()
        {
            return await this.atletaRepository.GetAllAsyn();
        }

        public async Task<Atleta> Get(int id)
        {
            return this.atletaRepository.Get(id);
        }

        public async Task<Atleta> Add(Atleta atleta)
        {
            await this.atletaRepository.AddAsyn(atleta);
            return atleta;
        }

        public async Task<Atleta> Update(Atleta atleta, int id)
        {
            var updated = await this.atletaRepository.UpdateAsyn(atleta, id);
            return updated;
        }

        public async Task<int> Delete(int id)
        {
            var rmTotal = await this.atletaRepository.DeleteAsyn(await this.atletaRepository.GetAsync(id));
            return rmTotal;
        }

        public void Dispose()
        {
            this.atletaRepository.Dispose();
        }
    }
}
