using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Equipamento> UpdateAsyn(Equipamento equipamento, object key)
        {
            if (equipamento == null)
                return null;
            var exist = await this._context.Set<Equipamento>().FindAsync(key);
            if (exist != null)
            {
                this._context.Entry(exist).CurrentValues.SetValues(equipamento);

                var eteList =
                    this._context.EquipamentoTipoEquipamento.Where(ete => ete.EquipamentoId.Equals(equipamento.Id));
                foreach (var equipamentoTipoEquipamento in eteList)
                    exist.EquipamentoTipoEquipamento.Remove(equipamentoTipoEquipamento);

                exist.EquipamentoTipoEquipamento = new List<EquipamentoTipoEquipamento>();

                foreach (var equipamentoTipoEquipamento in equipamento.EquipamentoTipoEquipamento)
                    exist.EquipamentoTipoEquipamento.Add(equipamentoTipoEquipamento);

                await this._context.SaveChangesAsync();
            }

            return exist;
        }
    }
}