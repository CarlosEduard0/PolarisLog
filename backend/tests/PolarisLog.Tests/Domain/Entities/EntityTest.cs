using System;
using FluentAssertions;
using PolarisLog.Domain.Entities;
using Xunit;

namespace PolarisLog.Tests.Domain.Entities
{
    public class EntityTest
    {
        [Fact]
        public void Entity_DeveAdicionarIdEDataCadastro()
        {
            var entity = new Entity();
            entity.Id.Should().NotBeEmpty();
            entity.CadastradoEm.Should().BeCloseTo(DateTime.Now.ToUniversalTime(), 1000);
        }
    }
}