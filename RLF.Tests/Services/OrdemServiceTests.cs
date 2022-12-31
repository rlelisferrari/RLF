using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using DOMAIN.Services;
using Moq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xunit;

namespace RLF.Tests.Services
{
    public class OrdemServiceTests
    {
        private readonly Mock<IOrdemRepository> _ordemRepository;

        private List<Ordem> _ordemList;

        public OrdemServiceTests()
        {
            _ordemRepository = new Mock<IOrdemRepository>();
            var tipoOrdem1 = new TipoOrdem() { Id = 1, Nome = "tipoOrdem1" };
            var ordem1 = new Ordem() { Id = 1, TipoOrdem = tipoOrdem1, Nome = "Ordem1" };
            var ordem2 = new Ordem() { Id = 2, TipoOrdem = tipoOrdem1, Nome = "Ordem2" };
            var ordem3 = new Ordem() { Id = 3, TipoOrdem = tipoOrdem1, Nome = "Ordem3" };
            var ordem4 = new Ordem() { Id = 4, TipoOrdem = tipoOrdem1, Nome = "Ordem4" };
            var ordem5 = new Ordem() { Id = 5, TipoOrdem = tipoOrdem1, Nome = "Ordem5" };

            _ordemList = new List<Ordem> { ordem1, ordem2, ordem3,ordem4, ordem5 };
        }

        [Fact]
        public void GetAllOrdem_WithOrdemRepository_ResultCorrectNumberOfOrdems()
        {
            _ordemRepository.Setup(it => it.GetAllAsyn()).ReturnsAsync(_ordemList);
            var OrdemService = new OrdemService(_ordemRepository.Object);

            var list = OrdemService.GetAll();

            Assert.IsType<List<Ordem>>(list.Result);
            Assert.Equal(_ordemList.Count, list.Result.Count());
        }

        [Fact]
        public void GetOrdem_ById_ReturnCorrectOrdem()
        {
            var ordemId = 1;
            _ordemRepository.Setup(it => it.Get(It.IsAny<int>())).Returns((int i) =>_ordemList.Where(it => it.Id == i).FirstOrDefault());
            var OrdemService = new OrdemService(_ordemRepository.Object);

            var ordem = OrdemService.Get(ordemId);

            Assert.IsType<Ordem>(ordem.Result);
            Assert.Equal(_ordemList.FirstOrDefault(it => it.Id == ordemId), ordem.Result);
        }

        [Fact]
        public void AddOrdem_InListLength5_ReturnCorrectOrdemInListLenght6()
        {
            var ordem6 = new Ordem() { Id = 6, TipoOrdem = new TipoOrdem() { Id = 1, Nome = "tipoOrdem1" }, Nome = "Ordem6" };
            _ordemRepository.Setup(it => it.GetAllAsyn()).ReturnsAsync(_ordemList);
            _ordemRepository.Setup(it => it.AddAsyn(It.IsAny<Ordem>())).ReturnsAsync((Ordem ordem) => ordem);
            var OrdemService = new OrdemService(_ordemRepository.Object);

            var lenghtListBefore = OrdemService.GetAll().Result.Count();
            var ordem = OrdemService.Add(ordem6);
            _ordemList.Add(ordem6);
            var lenghtListLater = OrdemService.GetAll().Result.Count();

            Assert.IsType<Ordem>(ordem.Result);
            Assert.Equal(ordem.Result.Id, ordem6.Id);
            Assert.Equal(ordem.Result.Nome, ordem6.Nome);
            Assert.Equal(ordem.Result.TipoOrdem.Nome, ordem6.TipoOrdem.Nome);
            Assert.Equal(lenghtListLater, lenghtListBefore + 1);
        }

    }
}
