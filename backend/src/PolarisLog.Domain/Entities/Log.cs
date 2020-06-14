using System;
using PolarisLog.Domain.Exceptions;

namespace PolarisLog.Domain.Entities
{
    public class Log : Entity
    {
        public Guid UsuarioId { get; private set; }
        public Guid AmbienteId { get; private set; }
        public Guid NivelId { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Origem { get; private set; }
        public DateTime? ArquivadoEm { get; private set; }
        public DateTime CadastradoEm { get; private set; }
        
        public virtual Usuario Usuario { get; }
        public virtual Ambiente Ambiente { get; }
        public virtual Nivel Nivel { get; }

        public Log(Guid usuarioId, Guid ambienteId, Guid nivelId, string titulo, string descricao, string origem)
        {
            ValidarUsuarioId(usuarioId);
            ValidarAmbienteId(ambienteId);
            ValidarNivelId(nivelId);
            ValidarTitulo(titulo);
            ValidarDescricao(descricao);
            ValidarOrigem(origem);

            UsuarioId = usuarioId;
            AmbienteId = ambienteId;
            NivelId = nivelId;
            Titulo = titulo;
            Descricao = descricao;
            Origem = origem;
            CadastradoEm = DateTime.UtcNow;
        }
        
        public void Arquivar()
        {
            if (ArquivadoEm != null)
            {
                throw new DomainException("Log já foi arquivado");
            }
            ArquivadoEm = DateTime.UtcNow;
        }

        private void ValidarUsuarioId(Guid usuarioId)
        {
            if (usuarioId == Guid.Empty)
            {
                throw new DomainException("Id Usuário deve possuir conteúdo");
            }
        }

        private void ValidarAmbienteId(Guid ambienteId)
        {
            if (ambienteId == Guid.Empty)
            {
                throw new DomainException("Id Ambiente deve possuir conteúdo");
            }
        }

        private void ValidarNivelId(Guid nivelId)
        {
            if (nivelId == Guid.Empty)
            {
                throw new DomainException("Id Nível deve possuir conteúdo");
            }
        }

        private void ValidarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new DomainException("Título deve possuir conteúdo");
            }
        }

        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new DomainException("Descrição deve possuir conteúdo");
            }
        }

        private void ValidarOrigem(string origem)
        {
            if (string.IsNullOrWhiteSpace(origem))
            {
                throw new DomainException("Origem deve possuir conteúdo");
            }
        }
    }
}