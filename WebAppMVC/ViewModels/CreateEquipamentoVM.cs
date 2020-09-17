using System.Collections.Generic;
using DATA.Contexts;
using DOMAIN.Models;

namespace WebAppMVC.ViewModels
{
    public class CreateEquipamentoVM
    {
        public Equipamento Equipamento { get; set; }
        public IEnumerable<Equipamento> Equipamentos { get; set; }
        public IEnumerable<TipoEquipamento> TiposEquipamento { get; set; }

        private readonly AppDbContext context;

        public CreateEquipamentoVM(AppDbContext context)
        {
            this.context = context;
            Equipamentos = this.context.Equipamentos;
            TiposEquipamento = this.context.TipoEquipamento;
        }
    }
}