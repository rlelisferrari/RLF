using System;
using System.Collections.Generic;
using System.Text;

namespace DOMAIN.Models
{
    public class VeiculoOlx: Base.Base
    {
        public string Titulo { get; set; }
        public double Valor { get; set; }
        public int Km { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Potencia { get; set; }
        public string Cambio { get; set; }
        public string Direcao { get; set; }
        public string Link { get; set; }
    }
}
