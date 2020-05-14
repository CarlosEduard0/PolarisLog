using System;

namespace PolarisLog.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; }
        public DateTime DataCadastro { get; }

        public Entity()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }
    }
}