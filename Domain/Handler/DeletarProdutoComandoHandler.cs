using Mediator_CQRS.Domain.Command;
using Mediator_CQRS.Domain.Entity;
using Mediator_CQRS.Notifications;
using Mediator_CQRS.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator_CQRS.Domain.Handler
{
    public class DeletarProdutoComandoHandler : IRequestHandler<DeletarProdutoComando, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _produtoRepository;

        public DeletarProdutoComandoHandler(IMediator mediator, IRepository<Produto> produtoRepository)
        {
            _mediator = mediator;
            _produtoRepository = produtoRepository;
        }
        public async Task<string> Handle(DeletarProdutoComando request, CancellationToken cancellationToken)
        {
            try
            {
                await _produtoRepository.Delete(request.Id);
                await _mediator.Publish(new DeletarProdutoNotificacao
                { Id = request.Id, IsConcluido = true });
                return await Task.FromResult("Produto excluido com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new DeletarProdutoNotificacao
                {
                    Id = request.Id,
                    IsConcluido = false
                });
                await _mediator.Publish(new ErroNotification
                {
                    Erro = ex.Message,
                    PilhaErro = ex.StackTrace
                });
                return await Task.FromResult("Ocorreu um erro no momento da exclusão");
            }
        }
    }
}