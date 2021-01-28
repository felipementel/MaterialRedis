using Aplicacao.Domain.Aggregate.Product.Model;
using Aplicacao.Domain.Interfaces.Services;

namespace Aplicacao.Domain.Aggregate.Product.Interfaces.Services
{
    public interface IProductService : IService<Model.Product, int> { }
}