using System.Collections.Generic;
using DOMAIN.Models;

namespace WebAppMVC.ViewModels
{
    public class CreateOrdemVM
    {
        public IEnumerable<Ordem> Orders { get; set; }
        public IEnumerable<TipoOrdem> TiposOrdem { get; set; }
    }
}