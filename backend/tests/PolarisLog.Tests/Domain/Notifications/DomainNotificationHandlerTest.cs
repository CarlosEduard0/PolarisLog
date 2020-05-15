using System.Linq;
using System.Threading;
using FluentAssertions;
using PolarisLog.Domain.Notifications;
using Xunit;

namespace PolarisLog.Tests.Domain.Notifications
{
    public class DomainNotificationHandlerTest
    {
        [Fact]
        public void Handler_DeveAdicionarNotificacao()
        {
            var chave = "nome";
            var valor = "Nome deve possuir conteúdo";
            var notification = new DomainNotification(chave, valor);
            var notificationHandler = new DomainNotificationHandler();

            notificationHandler.Handle(notification, CancellationToken.None);

            notificationHandler.TemNotificacao().Should().Be(true);
            notificationHandler.ObterNotificacoes().Should().HaveCount(1);
            var notificacaoLancada = notificationHandler.ObterNotificacoes().First();
            notificacaoLancada.Key.Should().Be(chave);
            notificacaoLancada.Value.Should().Be(valor);
        }
    }
}