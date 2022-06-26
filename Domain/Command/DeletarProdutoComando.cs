using MediatR;

namespace Mediator_CQRS.Domain.Command
{
    public class DeletarProdutoComando : IRequest<string>
    {
        public int Id { get; private set; }

        public DeletarProdutoComando(int id)
        {
            Id = id;
        }
    }
}