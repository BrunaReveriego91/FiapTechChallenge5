using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Application.Services;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;
using GestaoInvestimentos.Tests.Fixture;
using MongoDB.Driver;
using Moq;

namespace GestaoInvestimentos.Tests.UnitTests
{
    public class TransacaoServiceTests
    {
        private readonly Mock<ITransacaoRepository> _transacaoRepository = new();
        private readonly Mock<IAtivoService> _ativoService = new();
        private readonly Mock<IPortifolioService> _portifolioService = new();

        private ITransacaoService CriarServico()
        {
            return new TransacaoService(_transacaoRepository.Object, _ativoService.Object, _portifolioService.Object);
        }

        [Fact(DisplayName = "Listar todas transacoes")]
        public async Task ListarTransacoes_DeveRetornarListaTransacoes()
        {
            //Arrange
            var transacoesFaker = new TransacaoFaker().Generate(3);
            _transacaoRepository.Setup(x => x.ListarTransacoes())
                .ReturnsAsync(transacoesFaker);

            var service = CriarServico();
            //Act
            var result = await service.ListarTransacoes();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.ToList().Count);
            Mock.Get(_transacaoRepository.Object).Verify(x => x.ListarTransacoes(), Times.Exactly(1));

        }

        [Fact(DisplayName = "Listar todas transacoes deve lancar exception")]
        public async Task ListarTransacoes_DeveLancarMongoException()
        {
            //Arrange
            var exception = new MongoException("Exceção forçada.");

            _transacaoRepository.Setup(x => x.ListarTransacoes())
                .ThrowsAsync(exception);

            var service = CriarServico();
            //Act && Assert
            await Assert.ThrowsAsync<MongoException>(async () =>
            {
                await service.ListarTransacoes();
            });
        }

        [Fact(DisplayName = "Cadastrar transacao deve cadastrar com sucesso")]
        public async Task CadastrarTransacao_DeveCadastrarComSucesso()
        {
            //Arrange
            var ativoFaker = new AtivoFaker().Generate();
            var portfolioFaker = new PortfolioFaker().Generate();
            var transacaoFaker = new TransacaoFaker().Generate();

            _ativoService.Setup(x => x.BuscarAtivo(transacaoFaker.AtivoId))
                .ReturnsAsync(ativoFaker);

            _portifolioService.Setup(x => x.BuscarPortifolio(transacaoFaker.PortfolioId))
                .ReturnsAsync(portfolioFaker);

            _transacaoRepository.Setup(x => x.CadastrarTransacao(transacaoFaker));

            var service = CriarServico();
            //Act
            await service.CadastrarTransacao(transacaoFaker);

            //Assert
            Mock.Get(_transacaoRepository.Object).Verify(x => x.CadastrarTransacao(It.IsAny<Transacao>()), Times.Exactly(1));

        }

        [Fact(DisplayName = "Cadastrar transacao deve lancar exception")]
        public async Task CadastrarTransacao_DeveLancarMongoException()
        {
            //Arrange

            var exception = new MongoException("Exceção forçada.");
            var ativoFaker = new AtivoFaker().Generate();
            var portfolioFaker = new PortfolioFaker().Generate();
            var transacaoFaker = new TransacaoFaker().Generate();

            _ativoService.Setup(x => x.BuscarAtivo(transacaoFaker.AtivoId))
                .ReturnsAsync(ativoFaker);

            _portifolioService.Setup(x => x.BuscarPortifolio(transacaoFaker.PortfolioId))
                .ReturnsAsync(portfolioFaker);

            _transacaoRepository.Setup(x => x.CadastrarTransacao(transacaoFaker))
                .ThrowsAsync(exception);

            var service = CriarServico();
            //Act && Assert
            await Assert.ThrowsAsync<MongoException>(async () =>
            {
                await service.CadastrarTransacao(transacaoFaker);
            });
        }



        [Fact(DisplayName = "Remover transacao deve remover com sucesso")]
        public async Task RemoverTransacao_DeveRemoverComSucesso()
        {
            //Arrange

            var transacoesFaker = new TransacaoFaker().Generate();

            _transacaoRepository.Setup(x => x.BuscarTransacao(It.IsAny<Guid>()))
                .ReturnsAsync(transacoesFaker);

            _transacaoRepository.Setup(x => x.RemoverTransacao(It.IsAny<Guid>()));


            var service = CriarServico();

            //Act
            await service.RemoverTransacao(It.IsAny<Guid>());

            //Assert
            Mock.Get(_transacaoRepository.Object).Verify(x => x.BuscarTransacao(It.IsAny<Guid>()), Times.Exactly(1));
            Mock.Get(_transacaoRepository.Object).Verify(x => x.RemoverTransacao(It.IsAny<Guid>()), Times.Exactly(1));

        }


        [Fact(DisplayName = "Remover transacao deve lancar exception")]
        public async Task RemoverTransacao_DeveLancarException()
        {
            //Arrange
            _transacaoRepository.Setup(x => x.BuscarTransacao(It.IsAny<Guid>()))
                .ReturnsAsync((Transacao)null);

            var service = CriarServico();

            //Act && Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await service.RemoverTransacao(It.IsAny<Guid>());
            });

        }



        [Fact(DisplayName = "Buscar transacao por ID deve buscar com sucesso")]
        public async Task BuscarTransacaoPorId_DeveRetornarTransacaoComSucesso()
        {
            //Arrange
            var transacoesFaker = new TransacaoFaker().Generate();

            _transacaoRepository.Setup(x => x.BuscarTransacao(It.IsAny<Guid>()))
                .ReturnsAsync(transacoesFaker);

            var service = CriarServico();
            //Act
            var result = await service.BuscarTransacao(It.IsAny<Guid>());

            //Assert
            Assert.NotNull(result);
            Mock.Get(_transacaoRepository.Object).Verify(x => x.BuscarTransacao(It.IsAny<Guid>()), Times.Exactly(1));

        }

    }
}
