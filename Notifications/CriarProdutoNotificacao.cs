using MediatR;

namespace Mediator_CQRS.Notifications
{
    public class CriarProdutoNotificacao : INotification
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}