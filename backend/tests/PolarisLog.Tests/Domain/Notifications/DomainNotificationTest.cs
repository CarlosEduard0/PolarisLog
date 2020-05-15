using FluentAssertions;
using PolarisLog.Domain.Notifications;
using Xunit;

namespace PolarisLog.Tests.Domain.Notifications
{
    public class DomainNotificationTest
    {
        [Fact]
        public void DomainNotification_DeveAdicionarChaveEValor()
        {
            var chave = "nome";
            var valor = "Nome deve possuir conteúdo";
            
            var domainNotification = new DomainNotification(chave, valor);

            domainNotification.Key.Should().Be(chave);
            domainNotification.Value.Should().Be(valor);
        }
    }
}