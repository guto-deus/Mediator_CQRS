using MediatR;

namespace Mediator_CQRS.Notifications
{
    public class AtualizarProdutoNotificacao : INotification
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool IsConcluido { get; set; }
    }
}