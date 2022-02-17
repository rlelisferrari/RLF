using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Services
{
    public class JogoService
    {
        private readonly IJogoRepository jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            this.jogoRepository = jogoRepository;
        }

        public async Task<IEnumerable<Jogo>> GetAll()
        {
            return await this.jogoRepository.GetAllAsyn();
        }

        public async Task<Jogo> Get(int id)
        {
            return this.jogoRepository.Get(id);
        }

        public async Task<Jogo> Add(Jogo jogo)
        {
            await this.jogoRepository.AddAsyn(jogo);
            return jogo;
        }

        public async Task<Jogo> Update(Jogo jogo, int id)
        {
            var updated = await this.jogoRepository.UpdateAsyn(jogo, id);
            return updated;
        }

        public async Task<int> Delete(int id)
        {
            var rmTotal = await this.jogoRepository.DeleteAsyn(await this.jogoRepository.GetAsync(id));
            return rmTotal;
        }

        public void Dispose()
        {
            this.jogoRepository.Dispose();
        }
    }
}
