using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Application.Services;
using GestaoInvestimentos.Domain.Entitites;
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


        [Fact(DisplayName = "Listar todos ativos")]
        public async Task ListarAtivos_DeveRetornarListaAtivosComSucesso()
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


        [Fact(DisplayName = "Listar ativos deve lançar exceção")]
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

        [Fact(DisplayName = "Cadastrar ativo com sucesso")]
        public async Task CadastrarAtivo_DeveCadastrarAtivoComSucesso()
        {
            // Arrange
            var ativosFaker = new AtivoFaker().Generate();

            var service = CriarServico();
            // Act
            await service.CadastrarAtivo(ativosFaker);

            // Assert
            _repositoryMock.Verify(repo => repo.CadastrarAtivo(ativosFaker), Times.Once);
        }

        [Fact(DisplayName = "Alterar ativo com sucesso")]
        public async Task AlterarAtivo_DeveAlterarAtivoComSucesso()
        {
            // Arrange
            var ativoFaker = new AtivoFaker().Generate();
            var service = CriarServico();
            // Act

            await service.AlterarAtivo(ativoFaker);

            // Assert
            _repositoryMock.Verify(repo => repo.AlterarAtivo(ativoFaker), Times.Once);
        }

        [Fact(DisplayName = "Remover ativo existente")]
        public async Task RemoverAtivo_DeveRemoverAtivoComSucesso()
        {
            // Arrange
            var ativoFaker = new AtivoFaker().Generate();
            var service = CriarServico();

            _repositoryMock.Setup(repo => repo.BuscarAtivo(ativoFaker.Id))
                .ReturnsAsync(ativoFaker);

            // Act
            await service.RemoverAtivo(ativoFaker.Id);

            // Assert
            _repositoryMock.Verify(repo => repo.BuscarAtivo(ativoFaker.Id), Times.Once);
            _repositoryMock.Verify(repo => repo.RemoverAtivo(ativoFaker.Id), Times.Once);
        }

        [Fact(DisplayName = "Remover ativo inexistente deve lançar exceção")]
        public async Task RemoverAtivo_AtivoInexistente_DeveLancarExcecao()
        {
            // Arrange
            var ativoId = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.BuscarAtivo(ativoId))
                .ReturnsAsync((Ativo)null);

            var service = CriarServico();
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await service.RemoverAtivo(ativoId));

            Assert.Equal("Cadastro ativo não localizado.", exception.Message);
        }

        [Fact(DisplayName = "Buscar ativo por ID")]
        public async Task BuscarAtivo_DeveRetornarAtivoPorId()
        {
            // Arrange
            var ativoFaker = new AtivoFaker().Generate();

            _repositoryMock.Setup(repo => repo.BuscarAtivo(ativoFaker.Id))
                .ReturnsAsync(ativoFaker);

            var service = CriarServico();
            // Act
            var result = await service.BuscarAtivo(ativoFaker.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ativoFaker.Id, result.Id);
        }

    }
}
