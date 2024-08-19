using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Context;
using GestaoInvestimentos.Infra.Data.Interfaces;
using MongoDB.Driver;

namespace GestaoInvestimentos.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoContext _context;

        public UsuarioRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            try
            {
                return await _context.Usuarios.Find(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public async Task CadastrarUsuario(Usuario usuario)
        {
            try
            {
                await _context.Usuarios.InsertOneAsync(usuario);
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public Task DesabilitarUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
