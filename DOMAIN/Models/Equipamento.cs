﻿using System.Collections.Generic;

namespace DOMAIN.Models
{
    public class Equipamento : Base.Base
    {
        public string Nome { get; set; }

        public ICollection<EquipamentoTipoEquipamento> EquipamentoTipoEquipamento { get; set; }
    }
}