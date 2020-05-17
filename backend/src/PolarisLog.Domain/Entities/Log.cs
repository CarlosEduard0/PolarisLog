using System;
using PolarisLog.Domain.Exceptions;

namespace PolarisLog.Domain.Entities
{
    public class Log : Entity
    {
        public Guid UsuarioId { get; private set; }
        public Level? Level { get; private set; }
        public string Descricao { get; private set; }
        public string Origem { get; private set; }
        public DateTime? ArquivadoEm { get; private set; }
        
        public Usuario Usuario { get; private set; }

        public Log(Guid usuarioId, Level? level, string descricao, string origem)
        {
            ValidarDescricao(descricao);
            ValidarOrigem(origem);

            UsuarioId = usuarioId;
            Level = level;
            Descricao = descricao;
            Origem = origem;
        }
        
        public void Arquivar()
        {
            if (ArquivadoEm != null)
            {
                throw new DomainException("Log já foi arquivado");
            }
            ArquivadoEm = DateTime.UtcNow;
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