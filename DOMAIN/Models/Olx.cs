using DOMAIN.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerOlx.OLX
{    
    public class Olx
    {
        //?me=30000&o=3&pe=60000&re=40&rs=37
        public string siteOlx = $"https://es.olx.com.br/autos-e-pecas/carros-vans-e-utilitarios?";
        
        public int filtroKm;
        public int filtroValor;
        public int filtroAnoInicial;
        public int filtroAnoFinal;
        public int filtroNumeroPaginas;
        public DateTime filtroHorasRecente;

        public Olx(int filtroKm, int filtroValor, int anoInicial, int anoFinal, int paginas, int horas)
        {
            this.filtroKm = filtroKm;
            this.siteOlx = siteOlx + $"me={this.filtroKm}&";
            this.filtroValor = filtroValor;
            this.filtroAnoInicial = AnoToIndiceOlx(anoInicial);
            this.filtroAnoFinal = AnoToIndiceOlx(anoFinal);
            this.filtroNumeroPaginas = paginas;
            this.filtroHorasRecente = DateTime.Now.AddHours(-1*horas);
        }

        public List<string> BuscaLinksOlx()
        {
            var hrefs = new List<string>();
            HtmlWeb web = new HtmlWeb();

            for (int i = 1; i <= filtroNumeroPaginas; i++)
            {
                //2019 - 2022; valor <= 60k
                //HtmlDocument doc = web.Load(siteOlx + "o=" + i + "&pe=60000&re=40&rs=37");
                //2008 - 2012; valor <= 40k
                var url = siteOlx + $"o=" + i + $"&pe={this.filtroValor}&re={this.filtroAnoFinal}&rs={this.filtroAnoInicial}";
                HtmlDocument doc = web.Load(url);
                var listaAnuncios = doc.GetElementbyId("ad-list");
                if (listaAnuncios == null)
                    continue;
                var count = 0;
                foreach (var anuncio in listaAnuncios.ChildNodes)
                {
                    var item = anuncio.Descendants().FirstOrDefault();
                    if (item != null && item.Attributes.Contains("href"))
                    {
                        hrefs.Add(item.Attributes["href"].Value);
                        count++;
                    }
                }
                Console.WriteLine("Url: " + url);
                Console.WriteLine($"PAGINA {i} Anuncios: {count} Total:{hrefs.Count}");
            }
            
            return hrefs;
        }

        public List<VeiculoOlx> CapturaVeiculosOlx(List<string> hrefs)
        {
            var anuncios = new List<VeiculoOlx>();
            HtmlWeb web = new HtmlWeb();
            foreach (var link in hrefs)
            {
                try
                {
                    HtmlDocument doc = web.Load(link);
                    var titulo = doc.DocumentNode.SelectNodes("//h1[@class='sc-1q2spfr-0 lcTcEs sc-ifAKCX cmFKIN']").FirstOrDefault();
                    var valor = doc.DocumentNode.SelectNodes("//h2[@class='sc-1leoitd-0 cIfjkh sc-ifAKCX cmFKIN']").FirstOrDefault();
                    var data = doc.DocumentNode.SelectNodes("//span[@class='sc-1oq8jzc-0 jvuXUB sc-ifAKCX fizSrB']").FirstOrDefault();
                    var modelo = doc.DocumentNode.SelectNodes("//div[@class='h3us20-3 iRwHp']").FirstOrDefault();
                    var descricao = doc.DocumentNode.SelectNodes("//span[@class='sc-1sj3nln-1 eOSweo sc-ifAKCX cmFKIN']").FirstOrDefault();
                    var km = doc.DocumentNode.SelectNodes("//div[@class='sc-hmzhuo eNZSNe sc-jTzLTM iwtnNi']");

                    var veiculo = new VeiculoOlx();
                    veiculo.DataPublicacao = DataPub(data.InnerText);

                    //Filtrar apenas anuncios do dia
                    if (veiculo.DataPublicacao < filtroHorasRecente)
                    {
                        Console.WriteLine($"Anúncio anterior a data {filtroHorasRecente}");
                        break;
                    }

                    veiculo.Link = link;
                    veiculo.Titulo = titulo.InnerText;
                    veiculo.Valor = Convert.ToDouble(valor.InnerText.Replace("R$", "").Replace(" ",""));                    
                    veiculo.Descricao = descricao.InnerText;
                    var it = modelo.Descendants().FirstOrDefault();
                    veiculo.Modelo = it != null ? it.InnerText : modelo.InnerText;
                    Console.WriteLine(veiculo.Titulo);
                    foreach (var item in km)
                    {
                        if (item.InnerText.Contains("Quilometragem"))
                            veiculo.Km = Convert.ToInt32(item.InnerText.Replace("Quilometragem", ""));
                        if (item.InnerText.Contains("Ano"))
                            veiculo.Ano = Convert.ToInt32(item.InnerText.Replace("Ano", ""));
                        if (item.InnerText.Contains("Potência do motor"))
                            veiculo.Potencia = item.InnerText.Replace("Potência do motor", "");
                        if (item.InnerText.Contains("Câmbio"))
                            veiculo.Cambio = item.InnerText.Replace("Câmbio", "");
                        if (item.InnerText.Contains("Direção"))
                            veiculo.Direcao = item.InnerText.Replace("Direção", "");
                    }
                    anuncios.Add(veiculo);
                    
                }
                catch(Exception ex) {
                    Console.WriteLine(ex);
                }
                
            }

            return anuncios;
        }        

        public DateTime DataPub(string text)
        {
            var dataAll = text.Replace("Publicado em", "").Replace("às", "|").Replace(" ", "").Split('|');
            var dataDiaMes = dataAll[0].Split('/');
            var dataHoraMin = dataAll[1].Split(':');
            var dia = Convert.ToInt32(dataDiaMes[0]);
            var mes = Convert.ToInt32(dataDiaMes[1]);
            var hora = Convert.ToInt32(dataHoraMin[0]);
            var min = Convert.ToInt32(dataHoraMin[1]);
            return new DateTime(DateTime.Now.Year, mes, dia, hora, min,0);
        }

        public int AnoToIndiceOlx(int ano)
        {
            var indice2022 = 40;
            var dif = 2022 - ano;
            return indice2022 - dif;
        }
    }
}
