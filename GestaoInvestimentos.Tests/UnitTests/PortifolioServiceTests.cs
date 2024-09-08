using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Application.Services;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;
using GestaoInvestimentos.Tests.Fixture;
using Moq;

namespace GestaoInvestimentos.Tests.UnitTests
{
    public class PortifolioServiceTests
    {
        private readonly Mock<IPortifolioRepository> _repositoryMock = new();
        private IPortifolioService CriarServico()
        {
            return new PortifolioService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Listar todos os portfólios")]
        public async Task ListarPortifolios_DeveRetornarListaDePortifolios()
        {
            // Arrange
            var listaPortifolios = new PortfolioFaker().Generate(2);

            _repositoryMock.Setup(repo => repo.ListarPortifolios())
                .ReturnsAsync(listaPortifolios);

            var service = CriarServico();
            // Act
            var result = await service.ListarPortifolios();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact(DisplayName = "Cadastrar portfólio com sucesso")]
        public async Task CadastrarPortifolio_DeveCadastrarPortifolioComSucesso()
        {
            // Arrange
            var portifolio = new PortfolioFaker().Generate();
            var service = CriarServico();

            // Act
            await service.CadastrarPortifolio(portifolio);

            // Assert
            _repositoryMock.Verify(repo => repo.CadastrarPortifolio(portifolio), Times.Once);
        }

        [Fact(DisplayName = "Remover portfólio existente")]
        public async Task RemoverPortifolio_DeveRemoverPortifolioComSucesso()
        {
            // Arrange
            var portifolio = new PortfolioFaker().Generate();

            _repositoryMock.Setup(repo => repo.BuscarPortifolio(portifolio.Id))
                .ReturnsAsync(portifolio);

            var servico = CriarServico();

            // Act
            await servico.RemoverPortifolio(portifolio.Id);

            // Assert
            _repositoryMock.Verify(repo => repo.BuscarPortifolio(portifolio.Id), Times.Once);
            _repositoryMock.Verify(repo => repo.RemoverPortifolio(portifolio.Id), Times.Once);
        }

        [Fact(DisplayName = "Remover portfólio inexistente deve lançar exceção")]
        public async Task RemoverPortifolio_PortifolioInexistente_DeveLancarExcecao()
        {
            // Arrange
            var portifolioId = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.BuscarPortifolio(portifolioId))
                .ReturnsAsync((Portifolio)null);

            var servico = CriarServico();
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await servico.RemoverPortifolio(portifolioId));

            Assert.Equal("Cadastro portifolio não localizado.", exception.Message);
        }


        [Fact(DisplayName = "Buscar portfólio por ID")]
        public async Task BuscarPortifolio_DeveRetornarPortifolioPorId()
        {
            // Arrange
            var portifolio = new PortfolioFaker().Generate();

            _repositoryMock.Setup(repo => repo.BuscarPortifolio(portifolio.Id))
                .ReturnsAsync(portifolio);

            var servico = CriarServico();
            // Act
            var result = await servico.BuscarPortifolio(portifolio.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(portifolio.Id, result.Id);
        }

    }
}
