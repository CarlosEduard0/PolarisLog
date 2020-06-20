using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Usuario;
using PolarisLog.Domain.QuerySide.QueryHandlers;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.QuerySide.Queries
{
    public class UsuarioQueryHandlerTest
    {
        private readonly Context _context;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioQueryHandlerTest()
        {
            _context = ContextFactory.Create();
            _usuarioRepository = new UsuarioRepository(_context);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarUsuarioQuandoForEncontradoNoBanco()
        {
            var usuario = UsuarioFactory.GerarUsuario();
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            var query = new ObterUsuarioPorIdQuery(usuario.Id);
            var queryHandler = new UsuarioQueryHandler(_usuarioRepository);

            var usuarioBanco = await queryHandler.Handle(query, CancellationToken.None);

            usuarioBanco.Should().BeEquivalentTo(usuario);
        }

        [Fact]
        public async Task ObterPorId_DeveInvalidarQueryQuandoIdForVazio()
        {
            var query = new ObterUsuarioPorIdQuery(Guid.Empty);
            var queryHandler = new UsuarioQueryHandler(_usuarioRepository);

            await queryHandler.Handle(query, CancellationToken.None);

            (await query.EhValido()).Should().Be(false);
            query.ValidationResult.Errors
                .Should()
                .Contain(error => error.ErrorMessage == "Id deve possuir conteúdo");
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNullQuandoNaoForEncontradoNoBanco()
        {
            var query = new ObterUsuarioPorIdQuery(Guid.NewGuid());
            var queryHandler = new UsuarioQueryHandler(_usuarioRepository);

            var usuarioBanco = await queryHandler.Handle(query, CancellationToken.None);

            usuarioBanco.Should().BeNull();
        }
    }
}