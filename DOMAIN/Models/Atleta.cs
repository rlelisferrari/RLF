using System;
using System.Collections.Generic;

namespace DOMAIN.Models
{
    public class Atleta: Base.Base
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; }
        public int Numero { get; set; }

        public ICollection<ScoutGeral> ScoutGeral { get; set; }
    }
}
