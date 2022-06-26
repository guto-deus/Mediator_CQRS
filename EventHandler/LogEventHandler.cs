using Mediator_CQRS.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator_CQRS.EventHandler
{
    public class LogEventHandler :
                            INotificationHandler<CriarProdutoNotificacao>,
                            INotificationHandler<AtualizarProdutoNotificacao>,
                            INotificationHandler<DeletarProdutoNotificacao>,
                            INotificationHandler<ErroNotification>
    {
        public Task Handle(CriarProdutoNotificacao notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CRIACAO: '{notification.Id} " +
                    $"- {notification.Nome} - {notification.Preco}'");
            });
        }

        public Task Handle(AtualizarProdutoNotificacao notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ALTERACAO: '{notification.Id} - {notification.Nome} " +
                    $"- {notification.Preco} - {notification.IsConcluido}'");
            });
        }
        public Task Handle(DeletarProdutoNotificacao notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"EXCLUSAO: '{notification.Id} " +
                    $"- {notification.IsConcluido}'");
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Erro} \n {notification.PilhaErro}'");
            });
        }

    }
}