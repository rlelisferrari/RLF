using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using DOMAIN.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RLF.DOMAIN.Tests.Services
{
    public class OrdemServiceTests
    {
        private readonly Mock<IOrdemRepository> _ordemRepository;


        public OrdemServiceTests()
        {
            _ordemRepository = new Mock<IOrdemRepository>();
        }

        [Fact]
        public void Test1()
        {
            var OrdemService = new OrdemService(_ordemRepository.Object);
        }

        [Fact]
        public void Test2()
        {
            var listOrdem = new List<Ordem>();
            _ordemRepository.Setup(it => it.GetAllAsyn()).ReturnsAsync(listOrdem);
            var OrdemService = new OrdemService(_ordemRepository.Object);

            var list = OrdemService.GetAll();

            Assert.IsType<List<Ordem>>(list);
            Assert.Equal(list.Result.Count(), 0);
        }
    }
}
