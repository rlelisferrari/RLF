namespace DOMAIN.Models
{
    public class EquipamentoTipoEquipamento
    {
        public int EquipamentoId { get; set; }
        public Equipamento Equipamento { get; set; }
        public int TipoEquipamentoId { get; set; }
        public TipoEquipamento TipoEquipamento { get; set; }
    }
}