using Mediator_CQRS.Domain.Command;
using Mediator_CQRS.Domain.Entity;
using Mediator_CQRS.Notifications;
using Mediator_CQRS.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator_CQRS.Domain.Handler
{
    public class AtualizarProdutoComandoHandler : IRequestHandler<AtualizarProdutoComando, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _produtoRepository;

        public AtualizarProdutoComandoHandler(IMediator mediator, IRepository<Produto> produtoRepository)
        {
            _mediator = mediator;
            _produtoRepository = produtoRepository;
        }

        public async Task<string> Handle(AtualizarProdutoComando request, CancellationToken cancellationToken)
        {
            var produto = new Produto
            {
                Id = request.Id,
                Nome = request.Nome,
                Preco = request.Preco
            };

            try
            {
                await _produtoRepository.Edit(produto);
                await _mediator.Publish(new AtualizarProdutoNotificacao
                {
                    Id = request.Id,
                    Nome = request.Nome,
                    Preco = request.Preco
                });
                return await Task.FromResult("Produto editado com sucesso.");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new AtualizarProdutoNotificacao
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco
                });

                await _mediator.Publish(new ErroNotification
                {
                    Erro = ex.Message,
                    PilhaErro = ex.StackTrace
                });
                return await Task.FromResult("Ocorreu um erro no momento da alteração");
            }
        }
    }
}