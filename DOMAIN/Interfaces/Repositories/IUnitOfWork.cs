using DOMAIN.Interfaces.Repositories;

namespace ApiCatalogo.Repository
{
    public interface IUnitOfWork
    {
        IEquipamentoRepository EquipamentoRepository { get; }
        IOrdemRepository OrdemRepository { get; }
        ITipoOrdemRepository TipoOrdemRepository { get; }
        void Commit();
    }
}
