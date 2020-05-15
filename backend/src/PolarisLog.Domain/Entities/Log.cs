using System;
using PolarisLog.Domain.Exceptions;

namespace PolarisLog.Domain.Entities
{
    public class Log : Entity
    {
        public Level Level { get; private set; }
        public string Descricao { get; private set; }
        public string Origem { get; private set; }
        public DateTime? ArquivadoEm { get; private set; }
        public DateTime? DeletadoEm { get; private set; }

        public Log(Level level, string descricao, string origem)
        {
            ValidarDescricao(descricao);
            ValidarOrigem(origem);
            
            Level = level;
            Descricao = descricao;
            Origem = origem;
        }
        
        public void Arquivar()
        {
            ArquivadoEm = DateTime.Now;
        }

        public void Deletar()
        {
            DeletadoEm = DateTime.Now;
        }

        public void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new DomainException("Descrição deve possuir conteúdo");
            }
        }

        public void ValidarOrigem(string origem)
        {
            if (string.IsNullOrWhiteSpace(origem))
            {
                throw new DomainException("Origem deve possuir conteúdo");
            }
        }
    }
}