using MediatR;

namespace Mediator_CQRS.Domain.Command
{
    public class AtualizarProdutoComando : IRequest<string>
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
    }
}