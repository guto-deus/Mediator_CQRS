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
    public class CriarProdutoComandoHandler : IRequestHandler<CriarProdutoComando, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _produtoRepository;

        public CriarProdutoComandoHandler(IMediator mediator, IRepository<Produto> produtoRepository)
        {
            _mediator = mediator;
            _produtoRepository = produtoRepository;
        }

        public async Task<string> Handle(CriarProdutoComando request, CancellationToken cancellationToken)
        {
            var produto = new Produto { Nome = request.Nome, Preco = request.Preco};

            try
            {
                await _produtoRepository.Add(produto);
                await _mediator.Publish(new CriarProdutoNotificacao
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco
                }, cancellationToken);

                return await Task.FromResult("Produto criado com sucesso.");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new CriarProdutoNotificacao { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco });
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}
