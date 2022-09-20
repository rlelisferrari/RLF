using EOD;
using EOD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EOD.API;

namespace WebAppMVC.Models
{
    public class B3ApiService
    {
        public API _api;

        public B3ApiService(string apiToken)
        {
            _api = new API(apiToken);
        }

        public virtual async Task<ICollection<HistoricalStockPrice>> ApiConsumer(string ticker, DateTime dataInicio, DateTime dataFim, int periodo)
        {
            try
            {
                List<HistoricalStockPrice> response = await _api.GetEndOfDayHistoricalStockPriceAsync(ticker, dataInicio, dataFim, (HistoricalPeriod)periodo);
                return response;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
