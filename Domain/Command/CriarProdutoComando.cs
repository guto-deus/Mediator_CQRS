using MediatR;

namespace Mediator_CQRS.Domain.Command
{
    public class CriarProdutoComando : IRequest<string>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}