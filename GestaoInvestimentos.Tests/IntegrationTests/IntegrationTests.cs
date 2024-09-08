using GestaoInvestimentos.API;
using GestaoInvestimentos.Tests.Fixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace GestaoInvestimentos.Tests.IntegrationTests
{
    public class IntegrationTests : IntegrationTestBase
    {
        public IntegrationTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }


        [Theory(DisplayName = "Cadastrar/Remover Usuario")]
        [InlineData("/Usuario/cadastrar")]
        public async Task CadastrarRemoverUsuarioAsync(string url)
        {
            // Arrange

            var usuario = new UsuarioRequestFaker().Generate();

            var content = new StringContent(
                JsonConvert.SerializeObject(usuario),
                Encoding.UTF8,
                "application/json"
            );

            var token = await ObterTokenAutenticacaoAsync();
            DefinirAutenticacaoHeader(token);

            // Act
            var response = await _httpClient.PostAsync(url, content);
            var usuarioId = await response.Content.ReadAsStringAsync();

            var responseDeleteUsuario = await _httpClient.DeleteAsync($"/Usuario/{usuarioId.Replace("\"", "")}");

            // Assert
            response.EnsureSuccessStatusCode();
            responseDeleteUsuario.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteUsuario.StatusCode);

            await DeletarAdminAsync(token);
        }


        [Theory(DisplayName = "Listar Transacoes")]
        [InlineData("/Transacao/listar")]
        public async Task ListarTransacoesAsync(string url)
        {
            var token = await ObterTokenAutenticacaoAsync();
            DefinirAutenticacaoHeader(token);
            var response = await _httpClient.GetAsync(url);
            Assert.True(response.IsSuccessStatusCode);

            await DeletarAdminAsync(token);
        }


        [Theory(DisplayName = "Listar Portifolios")]
        [InlineData("/Portifolio/listar")]
        public async Task ListarPortifoliosAsync(string url)
        {
            var token = await ObterTokenAutenticacaoAsync();
            DefinirAutenticacaoHeader(token);
            var response = await _httpClient.GetAsync(url);
            Assert.True(response.IsSuccessStatusCode);

            await DeletarAdminAsync(token);
        }



        [Theory(DisplayName = "Buscar Transacao Por Id")]
        [InlineData("/Transacao/{id}")]
        public async Task BuscarTransacaoAsync(string url)
        {

            var urlTratada = url.Replace("{id}", "2db67408-d862-4700-89a5-861e3c137f92");
            var token = await ObterTokenAutenticacaoAsync();
            DefinirAutenticacaoHeader(token);
            var response = await _httpClient.GetAsync(urlTratada);
            Assert.True(response.IsSuccessStatusCode);

            await DeletarAdminAsync(token);
        }



        [Theory(DisplayName = "Buscar Portifolio Por Id")]
        [InlineData("/Portifolio/{id}")]
        public async Task BuscarPortifolioAsync(string url)
        {

            var urlTratada = url.Replace("{id}", "2db67408-d862-4700-89a5-861e3c137f92");
            var token = await ObterTokenAutenticacaoAsync();
            DefinirAutenticacaoHeader(token);
            var response = await _httpClient.GetAsync(urlTratada);
            Assert.True(response.IsSuccessStatusCode);

            await DeletarAdminAsync(token);
        }

        [Theory(DisplayName = "Cadastrar/Remover Ativo, Portfolio e Transacao")]
        [InlineData("/Transacao/cadastrar")]
        public async Task CadastrarRemoverAtivoPortifolioTransacaoAsync(string url)
        {
            // Arrange

            var ativo = new AtivoFaker().Generate();

            var contentAtivo = new StringContent(
                JsonConvert.SerializeObject(ativo),
                Encoding.UTF8,
                "application/json"
            );

            var portfolio = new PortfolioFaker().Generate();

            var contentPortfolio = new StringContent(
                JsonConvert.SerializeObject(portfolio),
                Encoding.UTF8,
                "application/json"
            );

            var transacao = new TransacaoFaker().Generate();

            transacao.AtivoId = ativo.Id;
            transacao.PortfolioId = portfolio.Id;

            var content = new StringContent(
                JsonConvert.SerializeObject(transacao),
                Encoding.UTF8,
                "application/json"
            );
            var token = await ObterTokenAutenticacaoAsync();
            DefinirAutenticacaoHeader(token);

            // Act
            var responseAtivo = await _httpClient.PostAsync($"/Ativo/cadastrar", contentAtivo);
            var responsePortfolio = await _httpClient.PostAsync($"/Portifolio/cadastrar", contentPortfolio);

            var responseTransacao = await _httpClient.PostAsync(url, content);

            var responseDeleteAtivo = await _httpClient.DeleteAsync($"/Ativo/{ativo.Id}");
            var responseDeletePortfolio = await _httpClient.DeleteAsync($"/Portifolio/{portfolio.Id}");
            var responseDeleteTransacao = await _httpClient.DeleteAsync($"/Transacao/{transacao.Id}");

            // Assert
            responseAtivo.EnsureSuccessStatusCode();
            responsePortfolio.EnsureSuccessStatusCode();
            responseTransacao.EnsureSuccessStatusCode();

            responseDeleteAtivo.EnsureSuccessStatusCode();
            responseDeletePortfolio.EnsureSuccessStatusCode();
            responseDeleteTransacao.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, responseAtivo.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteAtivo.StatusCode);

            Assert.Equal(HttpStatusCode.OK, responsePortfolio.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeletePortfolio.StatusCode);

            Assert.Equal(HttpStatusCode.OK, responseTransacao.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteTransacao.StatusCode);


            await DeletarAdminAsync(token);
        }


    }
}
