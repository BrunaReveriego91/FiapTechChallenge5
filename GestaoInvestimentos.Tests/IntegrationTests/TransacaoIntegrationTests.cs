using GestaoInvestimentos.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GestaoInvestimentos.Tests.IntegrationTests
{
    public class TransacaoIntegrationTests : IntegrationTestBase
    {
        public TransacaoIntegrationTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }


        [Theory]
        [InlineData("/Transacao/listar")]
        public async Task ListarTransacoesAsync(string url)
        {
            var token = await ObterTokenAutenticacaoAsync();
            DefinirAutenticacaoHeader(token);
            var response = await _httpClient.GetAsync(url);
            Assert.True(response.IsSuccessStatusCode);

            await DeletarAdminAsync(token);
        }

    }
}
