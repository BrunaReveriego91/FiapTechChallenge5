using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Application.Services;
using GestaoInvestimentos.Infra.Data.Interfaces;
using GestaoInvestimentos.Tests.Fixture;
using MongoDB.Driver;
using Moq;

namespace GestaoInvestimentos.Tests.UnitTests
{
    public class AtivoServiceTests
    {
        private readonly Mock<IAtivoRepository> _repositoryMock = new();

        private IAtivoService CriarServico()
        {
            return new AtivoService(_repositoryMock.Object);
        }


        [Fact]
        public async Task ListarAtivos_DeveRetornarSucesso()
        {
            //Arrange
            var ativosFaker = new AtivoFaker().Generate(3);

            _repositoryMock.Setup(x => x.ListarAtivos())
                .ReturnsAsync(ativosFaker);

            var service = CriarServico();
            //Act
            var result = await service.ListarAtivos();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.ToList().Count);
            Mock.Get(_repositoryMock.Object).Verify(x => x.ListarAtivos(), Times.Exactly(1));

        }

        [Fact]
        public async Task ListarAtivos_DeveLancarMongoException()
        {
            //Arrange
            var exception = new MongoException("Exceção forçada.");

            _repositoryMock.Setup(x => x.ListarAtivos())
                .ThrowsAsync(exception);

            var service = CriarServico();
            //Act && Assert
            await Assert.ThrowsAsync<MongoException>(async () =>
            {
                await service.ListarAtivos();
            });
        }
    }
}
