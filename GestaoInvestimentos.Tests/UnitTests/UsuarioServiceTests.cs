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

        [Fact]
        public async Task CadastrarUsuario_DeveRetornarSucesso()
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
