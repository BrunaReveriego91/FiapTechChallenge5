﻿using GestaoInvestimentos.Application.DTOs.Usuario.Request;
using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;

namespace GestaoInvestimentos.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtToken _jwtToken;

        public UsuarioService(IUsuarioRepository usuarioRepository, IJwtToken jwtToken)
        {
            _usuarioRepository = usuarioRepository;
            _jwtToken = jwtToken;
        }

        public async Task<string> AutenticarUsuario(string email, string senha)
        {
            var usuario = await _usuarioRepository.AutenticarUsuario(email, senha);

            if (usuario == null)
                throw new Exception("Falha ao autenticar usuário.");

            return await _jwtToken.GenerateToken(usuario);
        }

        public async Task CadastrarUsuario(UsuarioRequest usuario)
        {
            var usuarioExistente = await _usuarioRepository.BuscarUsuarioPorEmail(usuario.Email);

            if (usuarioExistente != null)
                throw new Exception("E-mail já está em uso.");

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            var usuarioHash = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = senhaHash
            };

            await _usuarioRepository.CadastrarUsuario(usuarioHash);
        }
    }
}
