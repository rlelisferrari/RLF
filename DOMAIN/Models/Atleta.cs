using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOMAIN.Models
{
    public class Atleta: Base.Base
    {
        public string Nome { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }
        public int Numero { get; set; }

        public ICollection<ScoutGeral> ScoutGeral { get; set; }
    }
}
