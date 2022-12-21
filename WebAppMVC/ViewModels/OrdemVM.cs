using System.Collections.Generic;
using ApiCatalogo.Repository;
using DATA.Contexts;
using DOMAIN.Models;

namespace WebAppMVC.ViewModels
{
    public class OrdemVM
    {
        public Ordem Ordem { get; set; }
        public IEnumerable<Ordem> Ordens { get; set; }
        public IEnumerable<TipoOrdem> TodosTiposOrdem { get; set; }

        //private readonly AppDbContext context;

        //public OrdemVM(AppDbContext context)
        //{
        //    this.context = context;
        //    Ordens = this.context.Ordens;
        //    TodosTiposOrdem = this.context.TipoOrdens;
        //}

        public OrdemVM(IUnitOfWork uof)
        {
            Ordens = uof.OrdemRepository.GetAll();
            TodosTiposOrdem = uof.TipoOrdemRepository.GetAll();
        }
    }
}