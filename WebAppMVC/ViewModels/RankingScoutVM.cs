using DOMAIN.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebAppMVC.ViewModels
{
    public class RankingScoutVM
    {
        public Atleta Atleta { get; set; }
        public int nGols { get; set; }
        public int nAssistencias { get; set; }
        public int nAmarelos { get; set; }
        public int nVermelhos { get; set; }

        public RankingScoutVM() {}        
    }
}
