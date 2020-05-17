using Microsoft.AspNetCore.Identity;
using PolarisLog.Domain.Exceptions;

namespace PolarisLog.Domain.Entities
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }

        protected Usuario()
        {
        }

        public Usuario(string nome, string email, string senha)
        {
            ValidarNome(nome);
            ValidarEmail(email);
            ValidarSenha(senha);

            Nome = nome;
            Email = email;
            Senha = senha;
            Senha = new PasswordHasher<Usuario>().HashPassword(this, senha);
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome deve possuir conteúdo");
            }

            if (nome.Length > 50)
            {
                throw new DomainException("Nome deve ter no máximo 50 caracteres");
            }
        }

        private void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("E-mail deve possuir conteúdo");
            }

            if (email.Length > 50)
            {
                throw new DomainException("E-mail deve ter no máximo 50 caracteres");
            }
        }

        private void ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha deve possuir conteúdo");
            }
        }
    }
}