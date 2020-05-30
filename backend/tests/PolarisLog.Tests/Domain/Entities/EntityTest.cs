using System;
using System.Globalization;
using FluentAssertions;
using PolarisLog.Domain.Entities;
using Xunit;

namespace PolarisLog.Tests.Domain.Entities
{
    public class EntityTest
    {
        [Fact]
        public void Entity_DeveAdicionarId()
        {
            var entity = new Entity();
            entity.Id.Should().NotBeEmpty();
        }
    }
}