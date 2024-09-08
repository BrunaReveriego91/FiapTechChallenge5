using GestaoInvestimentos.API;
using GestaoInvestimentos.Application.DTOs.Usuario.Request;
using GestaoInvestimentos.Domain.Entitites;
using Microsoft.AspNetCore.Mvc.Testing;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GestaoInvestimentos.Tests.IntegrationTests
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly HttpClient _httpClient;
        protected readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Log para verificar os claims
            foreach (var jwtClaim in jwtToken.Claims)
            {
                Console.WriteLine($"Claim Type: {jwtClaim.Type}, Claim Value: {jwtClaim.Value}");
            }

            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            return claim?.Value;
        }

        protected async Task CriarUsuarioAsync(Usuario usuarioDTO)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Usuario/cadastrar", usuarioDTO);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.Content.ToString());

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        protected async Task<string?> ObterTokenAutenticacaoAsync(
            string email = "admin@teste.com.br",
            string password = "adminTester"
        )
        {
            var usuarioDTO = new Usuario
            {
                Nome = "Admin",
                Email = email,
                Senha = password,
                Role = Domain.Enums.EnumRoles.Admin
            };

            await CriarUsuarioAsync(usuarioDTO);

            var autenticarUsuario = new AutenticarUsuarioRequest { Email = email, Senha = password };
            var response = await _httpClient.PostAsJsonAsync("/Usuario/Autenticar", autenticarUsuario);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        protected void DefinirAutenticacaoHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task DeletarUsuarioAsync(string id = null, string token = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token de autorização é obrigatório");
            }
            DefinirAutenticacaoHeader(token);

            if (!string.IsNullOrEmpty(id))
            {
                var response = await _httpClient.DeleteAsync($"/Usuario/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var deleteError = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao deletar usuário {id}: {deleteError}");
                }
            }
            else
            {
                throw new Exception("ID do usuário é obrigatório");
            }
        }
        protected async Task DeletarAdminAsync(string token = null)
        {
            var idLoggedUser = GetUserIdFromToken(token);
            if (idLoggedUser == null)
            {
                throw new Exception("ID do usuário não encontrado no token");
            }

            await DeletarUsuarioAsync(idLoggedUser, token);

        }
    }
}
