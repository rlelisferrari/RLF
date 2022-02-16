namespace DOMAIN.Models
{
    public class ScoutGeral
    {
        public int idAtleta { get; set; }
        public int idJogo { get; set; }
        public int gol { get; set; }
        public int assistencia { get; set; }
        public int cartaAmarelo { get; set; }
        public int cartaVermelho { get; set; }

        public Atleta atleta { get; set; }
        public Jogo jogo { get; set; }
    }
}
