using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DOMAIN.Services
{
    public class EquipamentoService
    {
        private readonly IEquipamentoRepository equipamentoRepository;

        public EquipamentoService(IEquipamentoRepository equipamentoRepository)
        {
            this.equipamentoRepository = equipamentoRepository;
        }

        public async Task<IEnumerable<Equipamento>> GetAll()
        {
            return await this.equipamentoRepository.GetAllAsyn();
        }

        public async Task<Equipamento> Get(int id)
        {
            return this.equipamentoRepository.Get(id);
        }

        public async Task<Equipamento> Add(Equipamento equipamento)
        {
            await this.equipamentoRepository.AddAsyn(equipamento);
            return equipamento;
        }

        public async Task<Equipamento> Update(Equipamento equipamento, int id)
        {
            var updated = await this.equipamentoRepository.UpdateAsyn(equipamento, id);
            return updated;
        }

        public async Task<int> Delete(int id)
        {
            var rmTotal = await this.equipamentoRepository.DeleteAsyn(await this.equipamentoRepository.GetAsync(id));
            return rmTotal;
        }

        public void Dispose()
        {
            this.equipamentoRepository.Dispose();
        }
    }
}