using MediatR;

namespace Mediator_CQRS.Notifications
{
    public class DeletarProdutoNotificacao : INotification
    {
        public int Id { get; set; }
        public bool IsConcluido { get; set; }
    }
}