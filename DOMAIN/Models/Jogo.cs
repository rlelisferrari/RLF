using System;
using System.Collections.Generic;

namespace DOMAIN.Models
{
    public class Jogo : Base.Base
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public int Numero { get; set; }
        public string Adversario { get; set; }

        public ICollection<ScoutGeral> ScoutGeral { get; set; }
    }
}
