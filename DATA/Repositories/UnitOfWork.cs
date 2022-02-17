using DATA.Contexts;
using DATA.Repositories;
using DOMAIN.Interfaces.Repositories;

namespace ApiCatalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private EquipamentoRepository equipamentoRepository;
        private OrdemRepository ordemRepository;
        private TipoOrdemRepository tipoOrdemRepository;
        private AtletaRepository atletaRepository;
        private JogoRepository jogoRepository;
        public AppDbContext context;

        public UnitOfWork(AppDbContext contexto)
        {
            this.context = contexto;
        }

        public IEquipamentoRepository EquipamentoRepository
        {
            get
            {
                return this.equipamentoRepository =
                    this.equipamentoRepository ?? new EquipamentoRepository(this.context);
            }
        }

        public IOrdemRepository OrdemRepository
        {
            get { return this.ordemRepository = this.ordemRepository ?? new OrdemRepository(this.context); }
        }

        public ITipoOrdemRepository TipoOrdemRepository
        {
            get
            {
                return this.tipoOrdemRepository =
                    this.tipoOrdemRepository ?? new TipoOrdemRepository(this.context);
            }
        }

        public IAtletaRepository AtletaRepository
        {
            get
            {
                return this.atletaRepository =
                    this.atletaRepository ?? new AtletaRepository(this.context);
            }
        }

        public IJogoRepository JogoRepository
        {
            get
            {
                return this.jogoRepository =
                    this.jogoRepository ?? new JogoRepository(this.context);
            }
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}