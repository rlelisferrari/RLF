using System.Collections.Generic;

namespace WebAppMVC.ViewModels
{
    public class RelatorioScoutVM
    {
        public RelatorioScoutVM(List<RankingScoutVM> ranking)
        {
            Ranking = ranking;
        }

        public List<RankingScoutVM> Ranking {get; set;}
    }
}
