using GestaoInvestimentos.Application.DTOs.Usuario.Request;
using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Application.Services;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;
using GestaoInvestimentos.Tests.Fixture;
using Moq;

namespace GestaoInvestimentos.Tests.UnitTests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repositoryMock = new();
        private readonly Mock<IJwtToken> _jwtMock = new();

        private IUsuarioService CriarService()
        {
            return new UsuarioService(_repositoryMock.Object, _jwtMock.Object);
        }

        [Fact(DisplayName = "Autenticar usuario deve autenticar com sucesso")]
        public async Task AutenticarUsuario_DeveAutenticarComSucesso()
        {
            //Arrange
            var service = CriarService();
            var usuarioFaker = new UsuarioFaker().Generate();

            string jwtMock = $"eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTcyNDY4MjYwMCwiaWF0IjoxNzI0NjgyNjAwfQ.DGghqaE0FUAsxSemapNMFjQi4cslLWGUXxANVeE0zQE";

            _repositoryMock.Setup(x => x.AutenticarUsuario(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(usuarioFaker);
            _jwtMock.Setup(x => x.GenerateToken(usuarioFaker)).ReturnsAsync(jwtMock);
            //Act
            var response = await service.AutenticarUsuario(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(response);

        }

        [Fact(DisplayName = "Autenticar usuario deve lancar exception")]
        public async Task AutenticarUsuario_DeveLancarException()
        {
            //Arrange
            var service = CriarService();
            var usuarioFaker = new UsuarioFaker().Generate();

            _repositoryMock.Setup(x => x.AutenticarUsuario(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((Usuario)null);

            //Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await service.AutenticarUsuario(It.IsAny<string>(), It.IsAny<string>());
            });

        }


        [Fact(DisplayName = "Cadastrar usuario deve cadastrar com sucesso")]
        public async Task CadastrarUsuario_DeveCadastrarComSucesso()
        {
            //Arrange
            var service = CriarService();
            var usuarioFaker = new UsuarioFaker().Generate();
            var usuarioRequestFaker = new UsuarioRequest()
            {
                Email = usuarioFaker.Email,
                Nome = usuarioFaker.Nome,
                Senha = usuarioFaker.Senha
            };

            _repositoryMock.Setup(x => x.CadastrarUsuario(usuarioFaker));

            //Act
            await service.CadastrarUsuario(usuarioRequestFaker);

            //Assert

            Mock.Get(_repositoryMock.Object).Verify(x => x.CadastrarUsuario(It.IsAny<Usuario>()), Times.Exactly(1));
        }

    }
}
