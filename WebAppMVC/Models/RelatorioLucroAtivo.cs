using DOMAIN.Models;
using System;
using System.Collections.Generic;

namespace WebAppMVC.Models
{
    public class RelatorioLucroAtivo
    {
        public string NomeAtivo;
        public float Desagio;
        public DateTime DataInicial;
        public DateTime DataFinal;
        public DateTime HoraInicial;
        public DateTime HoraFinal;
        public List<CotacaoIntraDay> cotacoesIntraDay;

        public RelatorioLucroAtivo(string ativo, float desagio, DateTime dataInicial, DateTime dataFinal, DateTime horaInicial, DateTime horaFinal)
        {
            NomeAtivo = ativo;
            Desagio = desagio;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
        }
    }
}
