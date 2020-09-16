namespace DOMAIN.Models
{
    public class Ordem : Base.Base
    {
        public string Nome { get; set; }

        public TipoOrdem TipoOrdem { get; set; }
    }
}