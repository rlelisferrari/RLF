using System.Collections.Generic;

namespace DOMAIN.Models
{
    public class TipoEquipamento: Base.Base
    {
        public string Nome { get; set; }

        public ICollection<EquipamentoTipoEquipamento> EquipamentoTipoEquipamento { get; set; }
    }
}