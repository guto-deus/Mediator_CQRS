using Mediator_CQRS.Domain.Entity;
using Mediator_CQRS.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator_CQRS.Configuration
{
    public class DependencyInjectConfig
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<IRepository<Produto>, ProdutoRepository>();
        }
    }
}