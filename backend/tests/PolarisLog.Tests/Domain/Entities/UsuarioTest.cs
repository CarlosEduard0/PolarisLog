using System;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Exceptions;
using Xunit;

namespace PolarisLog.Tests.Domain.Entities
{
    public class UsuarioTest
    {
        [Fact]
        public void Usuario_DeveAdicionarNomeEmailESenha()
        {
            var nome = "nome";
            var email = "email";
            var senha = "senha";
            var passwordHasher = new PasswordHasher<Usuario>();
            
            var usuario = new Usuario(nome, email, senha);

            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
            passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha)
                .Should().Be(PasswordVerificationResult.Success);
        }

        [Fact]
        public void Usuario_DeveLancarExcecaoQuandoNomeForNullOuVazio()
        {
            Action nomeNull = () => new Usuario(null, "email", "senha");
            Action nomeVazio = () => new Usuario("", "email", "senha");

            nomeNull.Should().Throw<DomainException>().WithMessage("Nome deve possuir conteúdo");
            nomeVazio.Should().Throw<DomainException>().WithMessage("Nome deve possuir conteúdo");
        }

        [Fact]
        public void Usuario_DeveLancarExcecaoQuandoEmailForNullOuVazio()
        {
            Action emailNull = () => new Usuario("nome", null, "senha");
            Action emailVazio = () => new Usuario("nome", "", "senha");

            emailNull.Should().Throw<DomainException>().WithMessage("E-mail deve possuir conteúdo");
            emailVazio.Should().Throw<DomainException>().WithMessage("E-mail deve possuir conteúdo");
        }

        [Fact]
        public void Usuario_DeveLancarExcecaoQuandoSenhaForNullOuVazia()
        {
            Action senhaNull = () => new Usuario("nome", "email", null);
            Action senhaVazio = () => new Usuario("nome", "email", "");

            senhaNull.Should().Throw<DomainException>().WithMessage("Senha deve possuir conteúdo");
            senhaVazio.Should().Throw<DomainException>().WithMessage("Senha deve possuir conteúdo");
        }

        [Fact]
        public void Usuario_DeveLancarExcecaoQuandoNomeForMaiorQue50Caracteres()
        {
            var nome = "Nome com mais de 50 caracteres Nome com mais de 50 ";
            
            Action acao = () => new Usuario(nome, "email", "senha");

            acao.Should().Throw<DomainException>().WithMessage("Nome deve ter no máximo 50 caracteres");
        }

        [Fact]
        public void Usuario_DeveLancarExcecaoQuandoEmailForMaiorQue50Caracteres()
        {
            var email = "emailcommaisde50caracteres@emailcommaisde50cara.ter";
            
            Action acao = () => new Usuario("nome", email, "senha");

            acao.Should().Throw<DomainException>().WithMessage("E-mail deve ter no máximo 50 caracteres");
        }
    }
}