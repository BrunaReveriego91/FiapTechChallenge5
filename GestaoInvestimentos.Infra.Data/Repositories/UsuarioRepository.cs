﻿using GestaoInvestimentos.Domain.Entitites;
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

        public async Task<Usuario> AutenticarUsuario(string email, string senha)
        {
            try
            {
                var usuario = await _context.Usuarios.Find(u => u.Email == email).FirstOrDefaultAsync();

                if (usuario == null)
                {
                    throw new Exception("Usuário ou senha inválidos.");
                }

                bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

                if (!senhaValida)
                {
                    throw new Exception("Usuário ou senha inválidos.");
                }

                usuario.Senha = null;
                return usuario;
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
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

        public async Task<Usuario> BuscarUsuarioPorId(Guid id)
        {
            try
            {
                return await _context.Usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();
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

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            try
            {
                var projection = Builders<Usuario>.Projection.Exclude(u => u.Senha);
    
                var usuarios = await _context.Usuarios
                    .Find(e => true)
                    .Project<Usuario>(projection)
                    .ToListAsync();

                return usuarios;
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public async Task RemoverUsuarioPorId(Guid id)
        {
            try
            {
                await _context.Usuarios.FindOneAndDeleteAsync(e => e.Id == id);
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }
    }
}
